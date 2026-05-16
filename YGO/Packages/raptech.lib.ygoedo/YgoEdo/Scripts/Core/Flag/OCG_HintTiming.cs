using System;

namespace YgoSoul.RapTech.Lib.YgoEdo.Core.Flag
{
    [Flags]
    public enum OCG_HintTiming : uint
    {
        DrawPhase = 0x1,
        StandbyPhase = 0x2,
        MainEnd = 0x4,
        BattleStart = 0x8,
        BattleEnd = 0x10,
        EndPhase = 0x20,
        Summon = 0x40,
        SpecialSummon = 0x80,
        FlipSummon = 0x100,
        MonsterSet = 0x200,
        SpellSet = 0x400,
        PositionChange = 0x800,
        Attack = 0x1000,
        DamageStep = 0x2000,
        DamageCalc = 0x4000,
        ChainEnd = 0x8000,
        Draw = 0x10000,
        Damage = 0x20000,
        Recover = 0x40000,
        Destroy = 0x80000,
        Remove = 0x100000,
        ToHand = 0x200000,
        ToDeck = 0x400000,
        ToGrave = 0x800000,
        BattlePhase = 0x1000000,
        Equip = 0x2000000,
        BattleStepEnd = 0x4000000,
        Battled = 0x8000000
    }
}