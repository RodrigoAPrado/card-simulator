namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Enum
{
    public enum DuelState
    {
        NotInitiated = 0,
        DuelOptionsSet = 1,
        DuelCreated = 2,
        DecksSet = 3,
        DuelReady = 4,
        WaitingInput = 5,
        DuelFinished = 99,
        Destroyed = 100
    }
}