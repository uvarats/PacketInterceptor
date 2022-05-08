namespace WinformsExample
{
    partial class CaptureForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CaptureForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.startStopToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveButton = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statisticsLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ppmLabel = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.packetInfoTextbox = new System.Windows.Forms.RichTextBox();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.averageLengthLabel = new System.Windows.Forms.Label();
            this.multicastCountLabel = new System.Windows.Forms.Label();
            this.notTCPLabel = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startStopToolStripButton,
            this.saveButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(988, 27);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // startStopToolStripButton
            // 
            this.startStopToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.startStopToolStripButton.Image = PacketInterceptor.Properties.Resources.stop_icon_disabled;
            this.startStopToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.startStopToolStripButton.Name = "startStopToolStripButton";
            this.startStopToolStripButton.Size = new System.Drawing.Size(29, 24);
            this.startStopToolStripButton.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // saveButton
            // 
            this.saveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveButton.Image = ((System.Drawing.Image)(resources.GetObject("saveButton.Image")));
            this.saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(29, 24);
            this.saveButton.Text = "save";
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statisticsLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 618);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(988, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statisticsLabel
            // 
            this.statisticsLabel.Name = "statisticsLabel";
            this.statisticsLabel.Size = new System.Drawing.Size(0, 16);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(16, 34);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.notTCPLabel);
            this.splitContainer1.Panel1.Controls.Add(this.multicastCountLabel);
            this.splitContainer1.Panel1.Controls.Add(this.averageLengthLabel);
            this.splitContainer1.Panel1.Controls.Add(this.ppmLabel);
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.packetInfoTextbox);
            this.splitContainer1.Size = new System.Drawing.Size(956, 575);
            this.splitContainer1.SplitterDistance = 322;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 3;
            // 
            // ppmLabel
            // 
            this.ppmLabel.AutoSize = true;
            this.ppmLabel.Location = new System.Drawing.Point(677, 29);
            this.ppmLabel.Name = "ppmLabel";
            this.ppmLabel.Size = new System.Drawing.Size(138, 16);
            this.ppmLabel.TabIndex = 1;
            this.ppmLabel.Text = "Пакетов в минуту: 0";
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersWidth = 51;
            this.dataGridView.Size = new System.Drawing.Size(656, 322);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.SelectionChanged += new System.EventHandler(this.dataGridView_SelectionChanged);
            // 
            // packetInfoTextbox
            // 
            this.packetInfoTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.packetInfoTextbox.Location = new System.Drawing.Point(0, 0);
            this.packetInfoTextbox.Margin = new System.Windows.Forms.Padding(4);
            this.packetInfoTextbox.Name = "packetInfoTextbox";
            this.packetInfoTextbox.Size = new System.Drawing.Size(956, 248);
            this.packetInfoTextbox.TabIndex = 1;
            this.packetInfoTextbox.Text = "";
            // 
            // averageLengthLabel
            // 
            this.averageLengthLabel.AutoSize = true;
            this.averageLengthLabel.Location = new System.Drawing.Point(677, 57);
            this.averageLengthLabel.Name = "averageLengthLabel";
            this.averageLengthLabel.Size = new System.Drawing.Size(167, 16);
            this.averageLengthLabel.TabIndex = 2;
            this.averageLengthLabel.Text = "Средняя длина пакета: 0";
            // 
            // multicastCountLabel
            // 
            this.multicastCountLabel.AutoSize = true;
            this.multicastCountLabel.Location = new System.Drawing.Point(677, 87);
            this.multicastCountLabel.Name = "multicastCountLabel";
            this.multicastCountLabel.Size = new System.Drawing.Size(130, 16);
            this.multicastCountLabel.TabIndex = 3;
            this.multicastCountLabel.Text = "Multicast-пакетов: 0";
            // 
            // notTCPLabel
            // 
            this.notTCPLabel.AutoSize = true;
            this.notTCPLabel.Location = new System.Drawing.Point(677, 115);
            this.notTCPLabel.Name = "notTCPLabel";
            this.notTCPLabel.Size = new System.Drawing.Size(84, 16);
            this.notTCPLabel.TabIndex = 4;
            this.notTCPLabel.Text = "Не TCP/IP: 0";
            // 
            // CaptureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(988, 640);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CaptureForm";
            this.Text = "Перехват пакетов";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CaptureForm_FormClosing);
            this.Load += new System.EventHandler(this.CaptureForm_Load);
            this.Shown += new System.EventHandler(this.CaptureForm_Shown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton startStopToolStripButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statisticsLabel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.RichTextBox packetInfoTextbox;
        private System.Windows.Forms.ToolStripButton saveButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Label ppmLabel;
        private System.Windows.Forms.Label averageLengthLabel;
        private System.Windows.Forms.Label notTCPLabel;
        private System.Windows.Forms.Label multicastCountLabel;
    }
}