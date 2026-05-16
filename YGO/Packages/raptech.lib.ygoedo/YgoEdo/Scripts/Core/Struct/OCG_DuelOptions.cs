using System;
using System.Runtime.InteropServices;

namespace YgoSoul.RapTech.Lib.YgoEdo.Core.Struct
{
    [StructLayout(LayoutKind.Sequential)]
    public struct OCG_DuelOptions
    {
        public ulong seed0;
        public ulong seed1;
        public ulong seed2;
        public ulong seed3;

        public ulong flags;

        public OCG_Player team1;
        public OCG_Player team2;

        public IntPtr cardReader;
        public IntPtr payload1;

        public IntPtr scriptReader;
        public IntPtr payload2;

        public IntPtr logHandler;
        public IntPtr payload3;

        public IntPtr cardReaderDone;
        public IntPtr payload4;

        public byte enableUnsafeLibraries;
    }
}