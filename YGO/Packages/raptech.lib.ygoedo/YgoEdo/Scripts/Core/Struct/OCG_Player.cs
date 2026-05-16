using System.Runtime.InteropServices;

namespace YgoSoul.RapTech.Lib.YgoEdo.Core.Struct
{
    [StructLayout(LayoutKind.Sequential)]
    public struct OCG_Player
    {
        public uint startingLP;
        public uint startingDrawCount;
        public uint drawCountPerTurn;
    }
}