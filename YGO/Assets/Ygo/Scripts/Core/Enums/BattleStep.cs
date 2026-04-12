namespace Ygo.Core.Enums
{
    public enum BattleStep
    {
        AttackDeclaration = 0,
        StartOfDamageStep = 1,
        BeforeDamageCalculation = 2,
        DamageCalculation = 3,
        AfterDamageCalculation = 4,
        EndOfDamageStep = 5,
        ReturnToBattlePhase = 6,
    }
}