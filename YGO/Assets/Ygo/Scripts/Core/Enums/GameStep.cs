using UnityEngine.Assertions.Must;

namespace Ygo.Core.Enums
{
    public enum GameStep
    {
        None = 0,
        WaitingDraw = 1,
        Open = 2,
        SelectingZoneToSummonMonster = 3,
        OnMonsterSummoned = 4,
        Battle = 5,
        SelectingAttackTarget = 6,
        AttackingDeclaration = 7,
        StartOfDamageStep = 8,
        BeforeDamageCalculation = 9,
        DamageCalculation = 10,
        AfterDamageCalculation = 11,
        EndOfDamageStep = 12,
        ProceedToNextPhase = 99,
    }
}