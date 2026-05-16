using System;

namespace YgoSoul.RapTech.Lib.YgoEdo.Core.Flag
{
    [Flags]
    public enum OCG_CardLocation : uint
    {
        Unknown = 0x0,
        Deck = 0x1,
        Hand = 0x2,
        MonsterZone = 0x4,
        SpellTrapZone = 0x8,
        Grave = 0x10,
        Banishment = 0x20,
        Extra = 0x40,
        Overlay = 0x80,
        OnField = MonsterZone | SpellTrapZone
    }
}