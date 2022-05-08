using PacketDotNet;
using SharpPcap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinformsExample
{
    public class PacketWrapper
    {
        public RawCapture rawPacket;

        public int Count { get; private set; }
        public DateTime Timeval { get { return rawPacket.Timeval.Date; } }
        public LinkLayers LinkLayerType { get { return rawPacket.LinkLayerType; } }
        public int Length { get { return rawPacket.Data.Length; } }

        public PacketWrapper(int count, RawCapture p)
        {
            this.Count = count;
            this.rawPacket = p;
        }
    }
}
