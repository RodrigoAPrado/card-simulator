using System;

namespace YgoSoul.RapTech.Lib.YgoEdo.Core.Flag
{
    [Flags]
    public enum OCG_MonsterAttributes : uint
    {
        None = 0x0,
        Earth = 0x01,
        Water = 0x02,
        Fire = 0x04,
        Wind = 0x08,
        Light = 0x10,
        Dark = 0x20,
        Divine = 0x40,

        // Combinação de todos os atributos
        All = Earth | Water | Fire | Wind | Light | Dark | Divine
    }
}