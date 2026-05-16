namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Enum
{
    public enum DuelPhase
    {
        DrawPhase = 0x1,
        StandbyPhase = 0x2,
        MainPhase1 = 0x4,
        BattlePhase = 0x8,
        BattleStep = 0x10,
        DamageStep = 0x20,
        DamageCalculation = 0x40,
        BattleResult = 0x80,
        MainPhase2 = 0x100,
        EndPhase = 0x200
    }
}