using System.Runtime.InteropServices;

namespace YgoSoul.RapTech.Lib.YgoEdo.Core.Struct
{
    [StructLayout(LayoutKind.Sequential)]
    public struct OCG_NewCardInfo
    {
        public byte team;
        public byte duelist;
        public uint code;
        public byte con;
        public uint loc;
        public uint seq;
        public uint pos;
    }
}