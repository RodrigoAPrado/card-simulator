namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.System.Enum
{
    public enum SystemStrings
    {
        // --- Summons e Ações Básicas (1-7) ---
        NormalSummon = 0x1, // 1
        SpecialSummon = 0x2, // 2
        FlipSummon = 0x3, // 3
        NormalSummoned = 0x4, // 4
        SpecialSummoned = 0x5, // 5
        FlipSummoned = 0x6, // 6
        Activate = 0x7, // 7

        // --- Custos e Ações de Campo (10-12) ---
        RemoveCounter = 0xA, // 10
        PayLP = 0xB, // 11
        RemoveMaterial = 0xC, // 12

        // --- Fases e Etapas do Turno (20-32) ---
        DrawPhase = 0x14, // 20
        StandbyPhase = 0x15, // 21
        MainPhase = 0x16, // 22
        EndOfMainPhase = 0x17, // 23
        BattlePhase = 0x18, // 24
        EndOfBattlePhase = 0x19, // 25
        EndPhase = 0x1A, // 26
        BeforeNormalDraw = 0x1B, // 27
        StartStepBattlePhase = 0x1C, // 28
        EndOfBattlePhase2 = 0x1D, // 29
        ReplayContinueAttack = 0x1E, // 30
        AttackDirectly = 0x1F, // 31
        ConductAnotherBattlePhase = 0x20, // 32

        // --- Damage Step (40-44) ---
        StartOfDamageStep = 0x28, // 40
        BeforeDamageCalculation = 0x29, // 41
        DuringDamageCalculation = 0x2A, // 42
        AfterDamageCalculation = 0x2B, // 43
        AtTheEndOfDamageStep = 0x2C, // 44

        // --- Efeitos Diversos (60-67) ---
        CoinHeads = 0x3C, // 60
        CoinTails = 0x3D, // 61
        HeadsEffect = 0x3E, // 62
        TailsEffect = 0x3F, // 63
        GeminiStatus = 0x40, // 64
        UseEffect = 0x41, // 65
        KeepRevealed = 0x42, // 66
        OwnedByOpponent = 0x43, // 67

        // --- Tipos Genéricos (70-72) ---
        MonsterCards = 0x46, // 70
        SpellCards = 0x47, // 71
        TrapCards = 0x48, // 72

        // --- Transições (80-81) ---
        EnteringBattlePhase = 0x50, // 80
        EnteringEndPhase = 0x51, // 81

        // --- Perguntas de Invocação e Ativação (90-98) ---
        SummonWithoutTribute = 0x5A, // 90
        UseAdditionalSummon = 0x5B, // 91
        UseOpponentMonster = 0x5C, // 92
        ContinueChoosingMaterials = 0x5D, // 93
        ActivateEffectNow = 0x5E, // 94
        UseEffectOfName = 0x5F, // 95
        UseEffectToAvoidDestruction = 0x60, // 96
        PlaceInSpellTrapZone = 0x61, // 97
        ApplyCostReplacing = 0x62, // 98

        // --- Turnos e UI Básica (100-103) ---
        GoFirst = 0x64, // 100
        GoSecond = 0x65, // 101
        Your = 0x66, // 102
        YourOpponents = 0x67, // 103

        // --- Sistema de Mensagens de Duelo (200-224) ---
        UseEffectFromLocation = 0xC8, // 200
        NoCardCanActivate = 0xC9, // 201
        CheckField = 0xCA, // 202
        ActivateCardOrEffect = 0xCB, // 203
        RemoveCountOfName = 0xCC, // 204
        SelectSortOrder = 0xCD, // 205
        SelectChainLinkSequence = 0xCE, // 206
        RevealingCards = 0xCF, // 207
        ConfirmCards = 0xD0, // 208
        CardMetRequisites = 0xD1, // 209
        ContinueSelectingCards = 0xD2, // 210
        TurnsPassed = 0xD3, // 211
        DeclaredCard = 0xD4, // 212
        DeclaredTypeMsg = 0xD5, // 213
        DeclaredAttributeMsg = 0xD6, // 214
        DeclaredNumberMsg = 0xD7, // 215
        ActivatedInChainLink = 0xD8, // 216
        TargetOfChainLink = 0xD9, // 217
        PayLPInstead = 0xDA, // 218
        DetachInstead = 0xDB, // 219
        RemoveCounterInstead = 0xDC, // 220
        ActivateTriggerEffect = 0xDD, // 221
        ActivateTriggerEffectGeneric = 0xDE, // 222
        SpecialSummonedProperly = 0xE0, // 224
        AddedToHandByEffect = 0xE1, //225

        // --- Comandos de Seleção (500-533) ---
        SelectToTribute = 0x1F4, // 500
        SelectToDiscard = 0x1F5, // 501
        SelectToDestroy = 0x1F6, // 502
        SelectToBanish = 0x1F7, // 503
        SelectToSendToGY = 0x1F8, // 504
        SelectToReturnToHand = 0x1F9, // 505
        SelectToAddToHand = 0x1FA, // 506
        SelectToReturnToDeck = 0x1FB, // 507
        SelectToNormalSummon = 0x1FC, // 508
        SelectToSpecialSummon = 0x1FD, // 509
        SelectToSet = 0x1FE, // 510
        SelectToUseAsFusion = 0x1FF, // 511
        SelectToUseAsSynchro = 0x200, // 512
        SelectToUseAsXyz = 0x201, // 513
        SelectFaceUp = 0x202, // 514
        SelectFaceDown = 0x203, // 515
        SelectAttackPos = 0x204, // 516
        SelectDefensePos = 0x205, // 517
        SelectToEquip = 0x206, // 518
        SelectXyzToDetach = 0x207, // 519
        SelectToChangeControl = 0x208, // 520
        SelectToReplace = 0x209, // 521
        SelectFaceUpAttack = 0x20A, // 522
        SelectFaceUpDefense = 0x20B, // 523
        SelectFaceDownAttack = 0x20C, // 524
        SelectFaceDownDefense = 0x20D, // 525
        SelectToReveal = 0x20E, // 526
        SelectToPlaceOnField = 0x20F, // 527
        SelectToChangePosition = 0x210, // 528
        SelectYourCard = 0x211, // 529
        SelectOpponentCard = 0x212, // 530
        SelectTributeSummon = 0x213, // 531
        SelectDetachXyzFrom = 0x214, // 532
        SelectToUseAsLink = 0x215, // 533

        // --- Comandos de Seleção e Declaração (549-578) ---
        SelectAttackTarget = 0x225, // 549
        SelectEffectToActivate = 0x226, // 550
        SelectTargetOfEffect = 0x227, // 551
        SelectHeadsOrTails = 0x228, // 552
        SelectDiceResults = 0x229, // 553
        DeclareOneCardType = 0x22A, // 554
        SelectAnOption = 0x22B, // 555
        SelectEffectToApply = 0x22C, // 556
        SelectGeneric = 0x230, // 560
        SelectBattlePosition = 0x231, // 561
        DeclareAttribute = 0x232, // 562
        DeclareType = 0x233, // 563
        DeclareCardName = 0x234, // 564
        DeclareNumber = 0x235, // 565
        SelectEffectToActivate2 = 0x236, // 566
        DeclareLevelRank = 0x237, // 567
        SelectCardToResolve = 0x238, // 568
        SelectZoneToPlace = 0x239, // 569
        SelectZoneUnusable = 0x23A, // 570
        SelectZoneToMove = 0x23B, // 571
        SelectToPlaceCounter = 0x23C, // 572
        AddCardsToHand = 0x23D, // 573
        SendCardsToGY = 0x23E, // 574
        SelectToNegate = 0x23F, // 575
        SelectToChangeAtkDef = 0x240, // 576
        SelectToApplyEffect = 0x241, // 577
        SelectToAttachAsMaterial = 0x242, // 578

        // --- Locais/Zonas (1000-1009) ---
        LocationDeck = 0x3E8, // 1000
        LocationHand = 0x3E9, // 1001
        LocationMZone = 0x3EA, // 1002
        LocationSZone = 0x3EB, // 1003
        LocationGY = 0x3EC, // 1004
        LocationRemoved = 0x3ED, // 1005
        LocationExtra = 0x3EE, // 1006
        LocationXyzMaterial = 0x3EF, // 1007
        LocationFieldZone = 0x3F0, // 1008
        LocationPendulumZone = 0x3F1, // 1009

        // --- Atributos (1010-1016) ---
        AttrEarth = 0x3F2, // 1010
        AttrWater = 0x3F3, // 1011
        AttrFire = 0x3F4, // 1012
        AttrWind = 0x3F5, // 1013
        AttrLight = 0x3F6, // 1014
        AttrDark = 0x3F7, // 1015
        AttrDivine = 0x3F8, // 1016

        // --- Raças (1020-1049) ---
        RaceWarrior = 0x3FC, // 1020
        RaceSpellcaster = 0x3FD, // 1021
        RaceFairy = 0x3FE, // 1022
        RaceFiend = 0x3FF, // 1023
        RaceZombie = 0x400, // 1024
        RaceMachine = 0x401, // 1025
        RaceAqua = 0x402, // 1026
        RacePyro = 0x403, // 1027
        RaceRock = 0x404, // 1028
        RaceWingedBeast = 0x405, // 1029
        RacePlant = 0x406, // 1030
        RaceInsect = 0x407, // 1031
        RaceThunder = 0x408, // 1032
        RaceDragon = 0x409, // 1033
        RaceBeast = 0x40A, // 1034
        RaceBeastWarrior = 0x40B, // 1035
        RaceDinosaur = 0x40C, // 1036
        RaceFish = 0x40D, // 1037
        RaceSeaSerpent = 0x40E, // 1038
        RaceReptile = 0x40F, // 1039
        RacePsychic = 0x410, // 1040
        RaceDivineBeast = 0x411, // 1041
        RaceCreatorGod = 0x412, // 1042
        RaceWyrm = 0x413, // 1043
        RaceCyberse = 0x414, // 1044
        RaceCyborg = 0x415, // 1045
        RaceMagicalKnight = 0x416, // 1046
        RaceHighDragon = 0x417, // 1047
        RaceOmegaPsychic = 0x418, // 1048
        RaceCelestialWarrior = 0x419, // 1049

        // --- Tipos de Carta (1050-1076) ---
        TypeMonster = 0x41A, // 1050
        TypeSpell = 0x41B, // 1051
        TypeTrap = 0x41C, // 1052
        TypeNormal = 0x41E, // 1054
        TypeEffect = 0x41F, // 1055
        TypeFusion = 0x420, // 1056
        TypeRitual = 0x421, // 1057
        TypeTrapMonster = 0x422, // 1058
        TypeSpirit = 0x423, // 1059
        TypeUnion = 0x424, // 1060
        TypeGemini = 0x425, // 1061
        TypeTuner = 0x426, // 1062
        TypeSynchro = 0x427, // 1063
        TypeToken = 0x428, // 1064
        TypeMaximum = 0x429, // 1065
        TypeQuickPlay = 0x42A, // 1066
        TypeContinuous = 0x42B, // 1067
        TypeEquip = 0x42C, // 1068
        TypeField = 0x42D, // 1069
        TypeCounter = 0x42E, // 1070
        TypeFlip = 0x42F, // 1071
        TypeToon = 0x430, // 1072
        TypeXyz = 0x431, // 1073
        TypePendulum = 0x432, // 1074
        TypeSpecialSummon = 0x433, // 1075
        TypeLink = 0x434, // 1076

        // --- Outros (1081) ---
        ExtraMonsterZone = 0x439, // 1081
        ActivateAction = 0x47E, // 1150
        NormalSummonAction = 0x47F, // 1151
        SpecialSummonAction = 0x480, // 1152
        Set = 0x481, // 1153
        FlipSummonAction = 0x482, // 1154
        ToDefense = 0x483, // 1155
        ToAttack = 0x484, // 1156
        Attack = 0x485, // 1157
        View = 0x486, // 1158
        StSet = 0x487, // 1159
        ActivatePendulum = 0x488, // 1160
        ResolveEffect = 0x489, // 1161
        ResetEffect = 0x48A, // 1162
        PendulumSummon = 0x48B, // 1163

        // --- Tipos de Invocação Especial (1170 - 1174) ---
        FusionSummon = 0x492, // 1170
        RitualSummon = 0x493, // 1171
        SynchroSummon = 0x494, // 1172
        XyzSummon = 0x495, // 1173
        LinkSummon = 0x496, // 1174

        // --- Proteção: Geral (3000-3013) ---
        CannotBeDestroyedByBattle = 3000,
        CannotBeDestroyedByCardEffects = 3001,
        CannotBeTargetedByCardEffects = 3002,
        CannotBeTargetedByMonsterEffects = 3003,
        CannotBeTargetedBySpellEffects = 3004,
        CannotBeTargetedByTrapEffects = 3005,
        CannotBeTargetedBySpellTrapEffects = 3006,
        CannotBeTargetedForAnAttack = 3007,
        CannotBeDestroyedByBattleOrCardEffects = 3008,
        CannotBeTargetedOrDestroyedByCardEffects = 3009,
        CannotBeDestroyedByMonsterEffects = 3010,
        CannotBeDestroyedBySpellEffects = 3011,
        CannotBeDestroyedByTrapEffects = 3012,
        CannotBeDestroyedBySpellTrapEffects = 3013,

        // --- Proteção: Própria (3030-3037) ---
        CannotBeDestroyedByYourCardEffects = 3030,
        CannotBeTargetedByYourCardEffects = 3031,
        CannotBeTargetedByYourMonsterEffects = 3032,
        CannotBeTargetedByYourSpellEffects = 3033,
        CannotBeTargetedByYourTrapEffects = 3034,
        CannotBeTargetedByYourSpellTrapEffects = 3035,
        CannotBeDestroyedByBattleOrYourCardEffects = 3036,
        CannotBeTargetedOrDestroyedByYourCardEffects = 3037,

        // --- Proteção: Oponente (3060-3067) ---
        CannotBeDestroyedByOpponentCardEffects = 3060,
        CannotBeTargetedByOpponentCardEffects = 3061,
        CannotBeTargetedByOpponentMonsterEffects = 3062,
        CannotBeTargetedByOpponentSpellEffects = 3063,
        CannotBeTargetedByOpponentTrapEffects = 3064,
        CannotBeTargetedByOpponentSpellTrapEffects = 3065,
        CannotBeDestroyedByBattleOrOpponentCardEffects = 3066,
        CannotBeTargetedOrDestroyedByOpponentCardEffects = 3067,

        // --- Não Afetado: Geral (3100-3104) ---
        UnaffectedByOtherCardEffects = 3100,
        UnaffectedByMonsterEffects = 3101,
        UnaffectedBySpellEffects = 3102,
        UnaffectedByTrapEffects = 3103,
        UnaffectedBySpellTrapEffects = 3104,

        // --- Não Afetado: Própria (3105-3109) ---
        UnaffectedByYourCardEffects = 3105,
        UnaffectedByYourMonsterEffects = 3106,
        UnaffectedByYourSpellEffects = 3107,
        UnaffectedByYourTrapEffects = 3108,
        UnaffectedByYourSpellTrapEffects = 3109,

        // --- Não Afetado: Oponente (3110-3114) ---
        UnaffectedByOpponentEffects = 3110,
        UnaffectedByOpponentMonsterEffects = 3111,
        UnaffectedByOpponentSpellEffects = 3112,
        UnaffectedByOpponentTrapEffects = 3113,
        UnaffectedByOpponentSpellTrapEffects = 3114,

        // --- Relacionado a Batalha (3200-3214) ---
        MustAttackIfAble = 3200,
        CanMakeASecondAttack = 3201,
        CanMakeASecondAttackOnMonsters = 3202,
        CanMakeASecondAttackInARow = 3203,
        CanMakeASecondAttackOnMonstersInARow = 3204,
        CanAttackDirectly = 3205,
        CannotAttack = 3206,
        CannotAttackDirectly = 3207,
        InflictsPiercingBattleDamage = 3208,
        InflictsDoubleBattleDamage = 3209,
        TakeNoBattleDamageInvolvingThisCard = 3210,
        OpponentAlsoTakesBattleDamageInvolvingThisCard = 3211,
        OpponentTakesBattleDamageInvolvingThisCardInstead = 3212,
        NeitherPlayerTakesBattleDamageInvolvingThisCard = 3213,
        OpponentTakesNoBattleDamageInvolvingThisCard = 3214,

        // --- Diversos (3300-3314) ---
        BanishItWhenItLeavesTheField = 3300,
        ReturnItToDeckWhenItLeavesTheField = 3301,
        CannotActivateItsEffects = 3302,
        CannotBeTributed = 3303,
        CannotBeTributedForATributeSummon = 3304,
        CannotBeDiscarded = 3305,
        CannotBeBanishied = 3306,
        CannotBeRespondedTo = 3307,
        CannotBeNegated = 3308,
        CannotBeUsedAsFusionMaterial = 3309,
        CannotBeUsedAsSynchroMaterial = 3310,
        CannotBeUsedAsXyzMaterial = 3311,
        CannotBeUsedAsLinkMaterial = 3312,
        CannotChangeItsBattlePosition = 3313,
        CannotApplyItsEffects = 3314
    }
}