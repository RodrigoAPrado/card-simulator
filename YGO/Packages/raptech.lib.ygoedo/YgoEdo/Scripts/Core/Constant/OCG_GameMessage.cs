namespace YgoSoul.RapTech.Lib.YgoEdo.Core.Constant
{
    public enum OCG_GameMessage : byte
    {
        Unknown = 0, // Adicionado por mim mesmo.
        Retry = 1,
        Hint = 2, // rever variações

        //Waiting = 3,
        //Start = 4,
        Win = 5,

        //UpdateData = 6,
        //UpdateCard = 7,
        //RequestDeck = 8,
        SelectBattleCmd = 10,
        SelectIdleCmd = 11,
        SelectEffectYn = 12,
        SelectYesNo = 13,
        SelectOption = 14,
        SelectCard = 15,
        SelectChain = 16,
        SelectPlace = 18,
        SelectPosition = 19,
        SelectTribute = 20,
        SortChain = 21,
        SelectCounter = 22,
        SelectSum = 23, // voltar depois
        SelectDisfield = 24,
        SortCard = 25,
        SelectUnselectCard = 26,
        ConfirmDeckTop = 30,
        ConfirmCards = 31,
        ShuffleDeck = 32,
        ShuffleHand = 33,

        //RefreshDeck = 34,
        SwapGraveDeck = 35, // voltar depois
        ShuffleSetCard = 36, // voltar depois
        ReverseDeck = 37,
        DeckTop = 38,
        ShuffleExtra = 39,
        NewTurn = 40,
        NewPhase = 41,
        ConfirmExtraTop = 42,
        Move = 50,
        PosChange = 53,
        Set = 54,
        Swap = 55,
        FieldDisabled = 56,
        Summoning = 60,
        Summoned = 61,
        SpSummoning = 62,
        SpSummoned = 63,
        FlipSummoning = 64,
        FlipSummoned = 65,
        Chaining = 70,
        Chained = 71,
        ChainSolving = 72,
        ChainSolved = 73,
        ChainEnd = 74,
        ChainNegated = 75,
        ChainDisabled = 76,
        CardSelected = 80,
        RandomSelected = 81, // voltar depois
        BecomeTarget = 83,
        Draw = 90,
        Damage = 91,
        Recover = 92,
        Equip = 93,
        LpUpdate = 94,

        //Unequip = 95,
        CardTarget = 96,
        CancelTarget = 97,
        PayLpCost = 100,
        AddCounter = 101,
        RemoveCounter = 102,
        Attack = 110,
        Battle = 111,
        AttackDisabled = 112,
        DamageStepStart = 113,
        DamageStepEnd = 114,
        MissedEffect = 120,

        //BeChainTarget = 121,
        //CreateRelation = 122,
        //ReleaseRelation = 123,
        TossCoin = 130,
        TossDice = 131,
        RockPaperScissors = 132, // não vou usar
        HandRes = 133, // não vou usar
        AnnounceRace = 140,
        AnnounceAttrib = 141,
        AnnounceCard = 142,
        AnnounceNumber = 143,
        CardHint = 160,
        TagSwap = 161, // deixar para depois
        ReloadField = 162, // é igual a query de campo

        //AiName = 163,
        //ShowHint = 164,
        PlayerHint = 165,
        MatchKill = 170,

        //CustomMsg = 180,
        RemoveCards = 190
    }
}