using System;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Flag
{
    [Flags]
    public enum Location
    {
        Unknown = 0,
        Deck = 1 << 0,
        Hand = 1 << 1,
        MonsterZone = 1 << 2,
        SpellTrapZone = 1 << 3,
        Grave = 1 << 4,
        Banishment = 1 << 5,
        Extra = 1 << 6,
        Overlay = 1 << 7,

        OnField = MonsterZone | SpellTrapZone
    }
}