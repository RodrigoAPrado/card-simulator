using System.Runtime.InteropServices;

namespace YgoSoul.RapTech.Lib.YgoEdo.Core.Struct
{
    [StructLayout(LayoutKind.Sequential)]
    public struct OCG_QueryInfo
    {
        public uint flags;
        public byte con;
        public uint loc;
        public uint seq;
        public uint overlay_seq;
    }
}