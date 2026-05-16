using System;

namespace YgoSoul.RapTech.Lib.YgoEdo.Core.Flag
{
    [Flags]
    public enum OCG_DuelFlags : ulong
    {
        // --- Flags Individuais ---
        TestMode = 0x01,
        AttackFirstTurn = 0x02,
        UseTrapsInNewChain = 0x04,
        SixStepBattleStep = 0x08,
        PseudoShuffle = 0x10,
        TriggerWhenPrivateKnowledge = 0x20,
        SimpleAi = 0x40,
        Relay = 0x80,
        OcgObsoleteIgnition = 0x100, // Priority de monstros por efeito de ignição (pré-2011)
        FirstTurnDraw = 0x200,
        OneFaceUpField = 0x400, // Apenas 1 Spell de Campo ativa no campo total (MR1/MR2)
        PZone = 0x800,
        SeparatePZone = 0x1000, // Zonas de Pêndulo separadas das Spell/Trap Zones
        EmZone = 0x2000,
        FsxMmzone = 0x4000, // Fusão/Synchro/Xyz podem ser invocados na Main Monster Zone (MR5)
        TrapMonstersNotUseZone = 0x8000, // Monstros Armadilha não ocupam zona de Spell/Trap
        ReturnToDeckTriggers = 0x10000,
        TriggerOnlyInLocation = 0x20000,
        SpSummonOnceOldNegate = 0x40000,
        CannotSummonOathOld = 0x80000,
        NoStandbyPhase = 0x100000,
        NoMainPhase2 = 0x200000,
        ThreeColumnsField = 0x400000,
        DrawUntil5 = 0x800000,
        NoHandLimit = 0x1000000,
        UnlimitedSummons = 0x2000000,
        InvertedQuickPriority = 0x4000000,
        EquipNotSentIfMissingTarget = 0x8000000,
        ZeroAtkDestroyed = 0x10000000,
        StoreAttackReplays = 0x20000000,
        SingleChainInDamageSubstep = 0x40000000,
        CanReposIfNonSumplayer = 0x80000000,
        TcgSegocNonPublic = 0x100000000UL,
        TcgSegocFirstTrigger = 0x200000000UL,
        TcgFastEffectIgnition = 0x400000000UL,
        ExtraDeckRitual = 0x800000000UL,
        NormalSummonFaceUpDef = 0x1000000000UL,

        // --- Modos Compostos (Regras Oficiais) ---
        SpeedDuel = ThreeColumnsField | NoMainPhase2 | TrapMonstersNotUseZone | TriggerOnlyInLocation,

        RushDuel = ThreeColumnsField | NoMainPhase2 | NoStandbyPhase | FirstTurnDraw | InvertedQuickPriority
                   | DrawUntil5 | NoHandLimit | UnlimitedSummons | TrapMonstersNotUseZone | TriggerOnlyInLocation |
                   ExtraDeckRitual,

        MasterRule1 = OcgObsoleteIgnition | FirstTurnDraw | OneFaceUpField | SpSummonOnceOldNegate |
                      ReturnToDeckTriggers | CannotSummonOathOld,

        MasterRuleGoat = MasterRule1 | TcgFastEffectIgnition | UseTrapsInNewChain | SixStepBattleStep |
                         TriggerWhenPrivateKnowledge
                         | EquipNotSentIfMissingTarget | ZeroAtkDestroyed | StoreAttackReplays |
                         SingleChainInDamageSubstep
                         | CanReposIfNonSumplayer | TcgSegocNonPublic | TcgSegocFirstTrigger,

        MasterRule2 = FirstTurnDraw | OneFaceUpField | SpSummonOnceOldNegate | ReturnToDeckTriggers |
                      CannotSummonOathOld,

        MasterRule3 = PZone | SeparatePZone | SpSummonOnceOldNegate | ReturnToDeckTriggers | CannotSummonOathOld,

        MasterRule4 = PZone | EmZone | SpSummonOnceOldNegate | ReturnToDeckTriggers | CannotSummonOathOld,

        MasterRule5 = PZone | EmZone | FsxMmzone | TrapMonstersNotUseZone | TriggerOnlyInLocation
    }
}