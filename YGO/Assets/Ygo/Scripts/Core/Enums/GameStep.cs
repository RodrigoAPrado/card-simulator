namespace Ygo.Core.Enums
{
    public enum GameStep
    {
        None = 0,
        WaitingDraw = 1,
        Open = 2,
        SelectingZoneToSummonMonster = 3,
        OnMonsterSummoned = 4,
        ProceedToNextPhase = 99,
    }
}