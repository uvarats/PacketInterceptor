using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SharpPcap;
using PacketDotNet;
using SharpPcap.LibPcap;
using System.Diagnostics;

// ����������� ������� ������� � ����


namespace WinformsExample
{
    public partial class CaptureForm : Form
    {
        /// <summary>
        /// ����� true - ��������� ����� ������������������
        /// </summary>
        /// <param name="args">
        /// A <see cref="string"/>
        /// </param>
        private bool BackgroundThreadStop;

        /// <summary>
        /// ������ ��������, ������� ������������� ������������� ������ �
        /// PacketQueue ���� � ����� �������.
        /// </summary>
        /// <param name="args">
        /// A <see cref="string"/>
        /// </param>
        private object QueueLock = new object();

        /// <summary>
        /// �������, � ������� ����� ��������� ������ �������� ������. ������� �����
        /// �������� ������, ����� �������� QueueLock ������.
        /// </summary>
        private List<RawCapture> PacketQueue = new List<RawCapture>();

        /// <summary>
        /// ��������� ����� PcapDevice.Statistics() �� ������� ������� ����������.
        /// ��������� ������������ ���������� ����������.
        /// </summary>
        /// <param name="args">
        /// A <see cref="string"/>
        /// </param>
        private DateTime LastStatisticsOutput;

        /// <summary>
        /// �������� ����� ������� ���������� ����� PcapDevice.Statistics()
        /// </summary>
        /// <param name="args">
        /// A <see cref="string"/>
        /// </param>
        private TimeSpan LastStatisticsInterval = new TimeSpan(0, 0, 2); // �������� ����� ����������� ���������� 2 �������.

        private System.Threading.Thread backgroundThread;

        private DeviceListForm deviceListForm;
        private ICaptureDevice device;

        private PacketArrivalEventHandler arrivalEventHandler;
        private CaptureStoppedEventHandler captureStoppedEventHandler;

        private Queue<PacketWrapper> packetStrings;

        private int packetCount;
        private BindingSource bs;
        private ICaptureStatistics captureStatistics;
        private bool statisticsUiNeedsUpdate = false;

        private Timer additionalStatistics;
        private uint PacketsPerMinute
        {
            set
            {
                ppmLabel.Text = $"������� � ������: {value}";
            }
        }
        private uint lastPacketsReceived = 0;
        private long AveragePacketLength
        {
            set
            {
                averageLengthLabel.BeginInvoke(new Action(() =>
                {
                    averageLengthLabel.Text = $"������� ����� ������: {value}";
                }));
            }
        }
        private uint notTCPPackets = 0;
        private uint NotTCPPackets
        {
            get
            {
                return notTCPPackets;
            }
            set
            {
                notTCPPackets = value;
                notTCPLabel.BeginInvoke(new Action(() =>
                {
                    notTCPLabel.Text = $"�� TCP/IP: {value}";
                }));
            }
        }
        private uint multicastPackets = 0;
        public uint MulticastPackets
        {
            get
            {
                return multicastPackets;
            }
            set
            {
                multicastPackets = value;
                multicastCountLabel.BeginInvoke(new Action(() =>
                {
                    multicastCountLabel.Text = $"Multicast-�������: {value}";
                }));
            }
        }
        private ICaptureDevice notTCPDevice;
        private ICaptureDevice multicastDevice;

        public CaptureForm()
        {
            InitializeComponent();
        }

        private void CaptureForm_Load(object sender, EventArgs e)
        {
            deviceListForm = new DeviceListForm();
            deviceListForm.OnItemSelected += new DeviceListForm.OnItemSelectedDelegate(deviceListForm_OnItemSelected);
            deviceListForm.OnCancel += new DeviceListForm.OnCancelDelegate(deviceListForm_OnCancel);
        }

        void deviceListForm_OnItemSelected(int itemIndex)
        {
            // ������ ����� ������ ����������
            deviceListForm.Hide();

            // ������ ������� ������� � ���������� ����������.
            StartCapture(itemIndex);
        }

        void deviceListForm_OnCancel()
        {
            Application.Exit();
        }
        private void StartCapture(int itemIndex)
        {
            packetCount = 0;
            // ������������� ��������� ��� ������� �������
            // ������ - ��������, ������ - ��� ������� ������� �� �� ����� TCP/IP
            // ������ - ��� ��������� ����������������� �������
            device = CaptureDeviceList.Instance[itemIndex];
            notTCPDevice = CaptureDeviceList.New()[itemIndex];
            multicastDevice = CaptureDeviceList.New()[itemIndex];

            packetStrings = new Queue<PacketWrapper>();
            bs = new BindingSource();
            dataGridView.DataSource = bs;
            LastStatisticsOutput = DateTime.Now;

            // ������ �������� ������
            BackgroundThreadStop = false;
            backgroundThread = new System.Threading.Thread(BackgroundThread);
            backgroundThread.Start();

            // ��������� �������� ������� �������
            arrivalEventHandler = new PacketArrivalEventHandler(device_OnPacketArrival);
            device.OnPacketArrival += arrivalEventHandler;
            captureStoppedEventHandler = new CaptureStoppedEventHandler(device_OnCaptureStopped);
            device.OnCaptureStopped += captureStoppedEventHandler;

            notTCPDevice.OnPacketArrival += NotTCPDevice_OnPacketArrival;
            multicastDevice.OnPacketArrival += MulticastDevice_OnPacketArrival;

            device.Open();
            notTCPDevice.Open();
            notTCPDevice.Filter = "not tcp and not ip";
            multicastDevice.Open();
            multicastDevice.Filter = "ether multicast";

            // �������������� �������������� ���������� ����������
            captureStatistics = device.Statistics;
            UpdateCaptureStatistics();

            additionalStatistics = new Timer();
            additionalStatistics.Tick += AdditionalStatistics_Tick;
            additionalStatistics.Interval = 15000;

            // ������ �������� ������� �������
            device.StartCapture();
            notTCPDevice.StartCapture();
            multicastDevice.StartCapture();

            additionalStatistics.Start();

            // ����� ������ �� ������ �������
            startStopToolStripButton.Image = PacketInterceptor.Properties.Resources.stop_icon_enabled;
            startStopToolStripButton.ToolTipText = "���������� ������";
        }

        private void MulticastDevice_OnPacketArrival(object sender, PacketCapture e)
        {
            MulticastPackets++;
        }

        private void NotTCPDevice_OnPacketArrival(object sender, PacketCapture e)
        {
            NotTCPPackets++;
        }

        private void AdditionalStatistics_Tick(object sender, EventArgs e)
        {
            uint receivedPackets = captureStatistics.ReceivedPackets;
            PacketsPerMinute = (receivedPackets - lastPacketsReceived) * 4;
            lastPacketsReceived = receivedPackets;
        }

        private void Shutdown()
        {
            if (device != null)
            {
                additionalStatistics.Stop();

                device.StopCapture();
                device.Close();
                device.OnPacketArrival -= arrivalEventHandler;
                device.OnCaptureStopped -= captureStoppedEventHandler;
                device = null;

                notTCPDevice.StopCapture();
                notTCPDevice.Close();
                notTCPDevice = null;

                multicastDevice.StopCapture();
                multicastDevice.Close();
                multicastDevice = null;

                // ������� �������� ������, ��� ���� ������������
                BackgroundThreadStop = true;

                // ������� ���������� �������� ������
                backgroundThread.Join();

                // ����� ������ �� �����
                startStopToolStripButton.Image = PacketInterceptor.Properties.Resources.play_icon_enabled;
                startStopToolStripButton.ToolTipText = "������� ���������� ��� �������";
            }
        }

        // �������� ������������ ��������� ������� �������
        void device_OnCaptureStopped(object sender, CaptureStoppedEventStatus status)
        {
            if (status != CaptureStoppedEventStatus.CompletedWithoutError)
            {
                MessageBox.Show("Error stopping capture", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void device_OnPacketArrival(object sender, PacketCapture e)
        {
            // ����� ������������� ����������
            var Now = DateTime.Now;
            var interval = Now - LastStatisticsOutput;

            if (interval > LastStatisticsInterval)
            {
                captureStatistics = e.Device.Statistics;
                statisticsUiNeedsUpdate = true;
                LastStatisticsOutput = Now;
            }

            // ���������� � ������� �������� ��� �������������� �������������� ������� � �������.
            lock (QueueLock)
            {
                PacketQueue.Add(e.GetPacket());
            }
        }

        private void CaptureForm_Shown(object sender, EventArgs e)
        {
            deviceListForm.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (device == null)
            {
                deviceListForm.Show();
            }
            else
            {
                Shutdown();
            }
        }

        /// <summary>
        /// ��������� ������� ������� � �������. ���� ������ ���� - ����������� QueueLock,
        /// ����������� ������� �������, ��������� ����� ������� PacketQueue
        /// � �������������� ��������. ��� ����������� ����� ������,
        /// ����������� ��� ���������� �������.
        ///
        /// ����� ������� ����� ����� ������������ ����������� �� �������,
        /// �� ��������� ��������.
        /// </summary>
        private void BackgroundThread()
        {
            while (!BackgroundThreadStop)
            {
                bool shouldSleep = true;

                lock (QueueLock)
                {
                    if (PacketQueue.Count != 0)
                    {
                        shouldSleep = false;
                    }
                }

                if (shouldSleep)
                {
                    System.Threading.Thread.Sleep(250);
                }
                else // ������� ���������� �������
                {
                    List<RawCapture> ourQueue;
                    lock (QueueLock)
                    {
                        // ������ ������� �������, ������������ ������������ ������� ����� �������
                        ourQueue = PacketQueue;
                        PacketQueue = new List<RawCapture>();
                    }
                    long packetLength = 0;
                    foreach (var packet in ourQueue)
                    {
                        // ����� �� ����� �������� ������������ ���� ������,
                        // �� ���������� ������ �������.
                        //
                        // ���� �������� �������� ������� ��������� ��������
                        // ��������� �������, ��� ������� �������� �� �������� ��������. 
                        // � ���� ������� ������ ������ ���� ���������.
                        packetLength += packet.PacketLength;
                        var packetWrapper = new PacketWrapper(packetCount, packet);
                        this.BeginInvoke(new MethodInvoker(delegate
                        {
                            packetStrings.Enqueue(packetWrapper);
                        }
                        ));

                        packetCount++;

                        var time = packet.Timeval.Date;
                        var len = packet.Data.Length;
                    }
                    AveragePacketLength = packetLength / ourQueue.Count;

                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        bs.DataSource = packetStrings.Reverse();
                    }
                    ));

                    if (statisticsUiNeedsUpdate)
                    {
                        UpdateCaptureStatistics();
                        statisticsUiNeedsUpdate = false;
                    }
                }
            }
        }

        private void UpdateCaptureStatistics()
        {
            uint receivedPackets = captureStatistics.ReceivedPackets;
            uint droppedPackets = captureStatistics.DroppedPackets;
            uint interfaceDroppedPackets = captureStatistics.InterfaceDroppedPackets;
            statisticsLabel.Text = $"�������� �������: {receivedPackets}, �������� �������: {droppedPackets}.";
        }

        private void CaptureForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Shutdown();
        }
        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView.SelectedCells.Count == 0)
                return;

            var packetWrapper = (PacketWrapper) dataGridView.Rows[dataGridView.SelectedCells[0].RowIndex].DataBoundItem;
            var packet = Packet.ParsePacket(packetWrapper.rawPacket.LinkLayerType, packetWrapper.rawPacket.Data);
            packetInfoTextbox.Text = packet.ToString(StringOutputType.VerboseColored);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "����� ������� (*.pcap)|*.pcap|��� ����� (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            string path = saveFileDialog.FileName;
            var captureFileWriter = new CaptureFileWriterDevice(path);
            captureFileWriter.Open();
            foreach (var wrap in packetStrings)
            {
                captureFileWriter.Write(wrap.rawPacket);
            }
        }
    }
}
