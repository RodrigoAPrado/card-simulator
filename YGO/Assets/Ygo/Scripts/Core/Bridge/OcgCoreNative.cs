using System;

namespace Ygo.Core.Bridge
{
    using System.Runtime.InteropServices;

    public static class OcgCoreNative
    {
        [DllImport("ocgcore", CallingConvention =  CallingConvention.Cdecl)]
        public static extern IntPtr create_duel(uint seed);

        [DllImport("ocgcore", CallingConvention = CallingConvention.Cdecl)]
        public static extern int set_player_info(IntPtr pduel, int playerid, int lp, int startcount, int drawcount);

        [DllImport("ocgcore", CallingConvention = CallingConvention.Cdecl)]
        public static extern int get_state(System.IntPtr buffer);
    }
}