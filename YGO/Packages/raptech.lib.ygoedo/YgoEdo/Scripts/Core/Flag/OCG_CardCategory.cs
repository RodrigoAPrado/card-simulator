using System;

namespace YgoSoul.RapTech.Lib.YgoEdo.Core.Flag
{
    [Flags]
    public enum OCG_CardCategory : ulong
    {
        None = 0,
        DestroyMonster = 1UL << 0, // 1100
        DestroyST = 1UL << 1, // 1101
        DestroyDeck = 1UL << 2, // 1102
        DestroyHand = 1UL << 3, // 1103
        SendToGY = 1UL << 4, // 1104
        SendToHand = 1UL << 5, // 1105
        SendToDeck = 1UL << 6, // 1106
        Banish = 1UL << 7, // 1107
        Draw = 1UL << 8, // 1108
        Search = 1UL << 9, // 1109
        ChangeAtkDef = 1UL << 10, // 1110
        ChangeLevelRank = 1UL << 11, // 1111
        ChangePosition = 1UL << 12, // 1112
        Piercing = 1UL << 13, // 1113
        DirectAttack = 1UL << 14, // 1114
        MultiAttack = 1UL << 15, // 1115
        NegateActivation = 1UL << 16, // 1116
        NegateEffect = 1UL << 17, // 1117
        DamageLP = 1UL << 18, // 1118
        RecoverLP = 1UL << 19, // 1119
        SpecialSummon = 1UL << 20, // 1120
        NonEffectRelated = 1UL << 21, // 1121
        TokenRelated = 1UL << 22, // 1122
        FusionRelated = 1UL << 23, // 1123
        RitualRelated = 1UL << 24, // 1124
        SynchroRelated = 1UL << 25, // 1125
        XyzRelated = 1UL << 26, // 1126
        LinkRelated = 1UL << 27, // 1127
        CounterRelated = 1UL << 28, // 1128
        Gamble = 1UL << 29, // 1129
        Control = 1UL << 30, // 1130
        MoveZones = 1UL << 31 // 1131
    }
}