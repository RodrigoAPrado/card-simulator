namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.System.Enum
{
    public enum SystemVictoryReason : uint
    {
        // --- Razões de Sistema (0x0 - 0x4) ---
        Surrendered = 0x0,
        LpReachedZero = 0x1,
        DeckOut = 0x2, // Cards can't be drawn
        TimeLimitUp = 0x3,
        LostConnection = 0x4,

        // --- Efeitos de Vitória Automática (0x10 - 0x1F) ---
        ExodiaTheForbiddenOne = 0x10,
        FinalCountdown = 0x11,
        VennominagaTheDeityOfPoisonousSnakes = 0x12,
        HolactieTheCreatorOfLight = 0x13,
        ExodiusTheUltimateForbiddenLord = 0x14,
        DestinyBoard = 0x15,
        LastTurn = 0x16,
        Number88GimmickPuppetOfLeo = 0x17,
        NumberC88GimmickPuppetDisasterLeo = 0x18,
        Jackpot7 = 0x19,
        RelaySoul = 0x1a,
        GhostrickAngelOfMischief = 0x1b,
        PhantasmSpiralAssault = 0x1c,
        FaWinners = 0x1d,
        FlyingElephant = 0x1e,
        ExodiaTheLegendaryDefender = 0x1f,

        // --- Efeitos Especiais e Match (0x20 - 0x30) ---
        MatchVictoryByEffect = 0x20, // Victory by the effect of 「%ls」
        TrueExodia = 0x21,
        CreatorOfMiracles = 0x30,

        // --- Efeitos de Anime/Eventos/Outros (0x51 - 0x58) ---
        Evil1 = 0x51,
        NumberIC1000NumerouniusNumerounia = 0x52,
        ZeroGateOfTheVoid = 0x53,
        Deuce = 0x54,
        ActionFieldRules = 0x55,
        DeckMastersRules = 0x56,
        DrawOfFate = 0x57,
        MusicalSumoDiceGames = 0x58
    }
}