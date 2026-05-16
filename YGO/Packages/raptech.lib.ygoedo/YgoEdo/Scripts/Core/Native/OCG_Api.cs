using System;
using System.Runtime.InteropServices;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Struct;

namespace YgoSoul.RapTech.Lib.YgoEdo.Core.Native
{
    public static class OCG_Api
    {
        private const string DllName = "ocgcore";

        public static class Info
        {
            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern void OCG_GetVersion(out int major, out int minor);
        }

        public static class Setup
        {
            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern int OCG_CreateDuel(out IntPtr duel, ref OCG_DuelOptions options);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern void OCG_DestroyDuel(IntPtr duel);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern void OCG_DuelNewCard(IntPtr duel, ref OCG_NewCardInfo info);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern int OCG_LoadScript(IntPtr duel, byte[] buffer, uint length,
                [MarshalAs(UnmanagedType.LPStr)] string name);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern void OCG_StartDuel(IntPtr duel);
        }

        public static class Run
        {
            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern int OCG_DuelProcess(IntPtr duel);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern IntPtr OCG_DuelGetMessage(IntPtr duel, out uint length);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern void OCG_DuelSetResponse(IntPtr duel, byte[] buffer, uint length);
        }

        public static class Query
        {
            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern uint OCG_DuelQueryCount(IntPtr pDuel, byte team, uint loc);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern IntPtr OCG_DuelQuery(IntPtr pDuel, out uint length, ref OCG_QueryInfo info_ptr);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern IntPtr
                OCG_DuelQueryLocation(IntPtr pDuel, out uint length, ref OCG_QueryInfo info_ptr);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern IntPtr OCG_DuelQueryField(IntPtr pDuel, out uint length);
        }
    }
}