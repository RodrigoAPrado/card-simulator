using System;
using System.Runtime.InteropServices;

namespace YgoSoul.RapTech.Lib.YgoEdo.Core.Native
{
    public class OCG_Delegates
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void DataReader(IntPtr payload, uint code, IntPtr data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void DataReaderDone(IntPtr payload, IntPtr data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void LogHandler(IntPtr payload, [MarshalAs(UnmanagedType.LPStr)] string message, int type);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int ScriptReader(IntPtr payload, IntPtr duel, [MarshalAs(UnmanagedType.LPStr)] string name);
    }
}