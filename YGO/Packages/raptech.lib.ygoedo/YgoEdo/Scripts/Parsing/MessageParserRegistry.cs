using System.Collections.Generic;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Constant;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser.Abstr;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing
{
    public static class MessageParserRegistry
    {
        public static Dictionary<OCG_GameMessage, IParser> RegisterParsers()
        {
            var unknownParser = new UnknownParser();
            var basicParser = new BasicParser();
            var selectPlaceParser = new SelectPlaceParser();
            var selectYesNoParser = new SelectYesNoParser();
            var sortChainCardParser = new SortChainCardParser();
            var confirmCardParser = new ConfirmCardParser();
            var shuffleCardsParser = new ShuffleCardsParser();
            var summoningParser = new SummoningParser();
            var positionListParser = new PositionListParser();
            var changeCounterParser = new ChangeCounterParser();
            var tossCoinDiceParser = new TossCoinDiceParser();
            var announceRaceAttributeParser = new AnnounceRaceAttributeParser();

            return new Dictionary<OCG_GameMessage, IParser>
            {
                { OCG_GameMessage.Unknown, unknownParser },
                { OCG_GameMessage.Retry, new RetryParser() },
                { OCG_GameMessage.Hint, new HintParser() },
                { OCG_GameMessage.Win, new WinParser() },
                { OCG_GameMessage.SelectBattleCmd, new SelectBattleCmdParser() },
                { OCG_GameMessage.SelectIdleCmd, new SelectIdleCmdParser() },
                { OCG_GameMessage.SelectEffectYn, selectYesNoParser },
                { OCG_GameMessage.SelectYesNo, selectYesNoParser },
                { OCG_GameMessage.SelectOption, new SelectOptionParser() },
                { OCG_GameMessage.SelectCard, new SelectCardParser() },
                { OCG_GameMessage.SelectChain, new SelectChainParser() },
                { OCG_GameMessage.SelectPlace, selectPlaceParser },
                { OCG_GameMessage.SelectPosition, new SelectPositionParser() },
                { OCG_GameMessage.SelectTribute, new SelectTributeParser() },
                { OCG_GameMessage.SortChain, sortChainCardParser },
                { OCG_GameMessage.SelectCounter, new SelectCounterParser() },
                { OCG_GameMessage.SelectSum, new SelectSumParser() },
                { OCG_GameMessage.SelectDisfield, selectPlaceParser },
                { OCG_GameMessage.SortCard, sortChainCardParser },
                { OCG_GameMessage.SelectUnselectCard, new SelectUnselectedCardParser() },
                { OCG_GameMessage.ConfirmDeckTop, confirmCardParser },
                { OCG_GameMessage.ConfirmCards, confirmCardParser },
                { OCG_GameMessage.ShuffleDeck, basicParser },
                { OCG_GameMessage.ShuffleHand, shuffleCardsParser },
                { OCG_GameMessage.SwapGraveDeck, new SwapGraveDeckParser() },
                { OCG_GameMessage.ShuffleSetCard, new ShuffleSetCardParser() },
                { OCG_GameMessage.ReverseDeck, basicParser },
                { OCG_GameMessage.DeckTop, basicParser },
                { OCG_GameMessage.ShuffleExtra, shuffleCardsParser },
                { OCG_GameMessage.NewTurn, basicParser },
                { OCG_GameMessage.NewPhase, basicParser },
                { OCG_GameMessage.ConfirmExtraTop, confirmCardParser },
                { OCG_GameMessage.Move, new MoveParser() },
                { OCG_GameMessage.PosChange, basicParser },
                { OCG_GameMessage.Set, summoningParser },
                { OCG_GameMessage.Swap, basicParser },
                { OCG_GameMessage.FieldDisabled, new FieldDisabledParser() },
                { OCG_GameMessage.Summoning, summoningParser },
                { OCG_GameMessage.Summoned, basicParser },
                { OCG_GameMessage.SpSummoning, summoningParser },
                { OCG_GameMessage.SpSummoned, basicParser },
                { OCG_GameMessage.FlipSummoning, summoningParser },
                { OCG_GameMessage.FlipSummoned, basicParser },
                { OCG_GameMessage.Chaining, new ChainingParser() },
                { OCG_GameMessage.Chained, basicParser },
                { OCG_GameMessage.ChainSolving, basicParser },
                { OCG_GameMessage.ChainSolved, basicParser },
                { OCG_GameMessage.ChainEnd, basicParser },
                { OCG_GameMessage.ChainNegated, basicParser },
                { OCG_GameMessage.ChainDisabled, basicParser },
                { OCG_GameMessage.CardSelected, positionListParser },
                { OCG_GameMessage.RandomSelected, new RandomSelectedParser() },
                { OCG_GameMessage.BecomeTarget, positionListParser },
                { OCG_GameMessage.Draw, new DrawParser() },
                { OCG_GameMessage.Damage, basicParser },
                { OCG_GameMessage.Recover, basicParser },
                { OCG_GameMessage.Equip, basicParser },
                { OCG_GameMessage.LpUpdate, basicParser },
                { OCG_GameMessage.CardTarget, basicParser },
                { OCG_GameMessage.CancelTarget, basicParser },
                { OCG_GameMessage.PayLpCost, basicParser },
                { OCG_GameMessage.AddCounter, changeCounterParser },
                { OCG_GameMessage.RemoveCounter, changeCounterParser },
                { OCG_GameMessage.Attack, new AttackParser() },
                { OCG_GameMessage.Battle, new BattleParser() },
                { OCG_GameMessage.AttackDisabled, basicParser },
                { OCG_GameMessage.DamageStepStart, basicParser },
                { OCG_GameMessage.DamageStepEnd, basicParser },
                { OCG_GameMessage.MissedEffect, basicParser },
                { OCG_GameMessage.TossCoin, tossCoinDiceParser },
                { OCG_GameMessage.TossDice, tossCoinDiceParser },
                { OCG_GameMessage.RockPaperScissors, new RockPaperScissorsParser() },
                { OCG_GameMessage.HandRes, new HandResParser() },
                { OCG_GameMessage.AnnounceRace, announceRaceAttributeParser },
                { OCG_GameMessage.AnnounceAttrib, announceRaceAttributeParser },
                { OCG_GameMessage.AnnounceCard, new AnnounceCardParser() },
                { OCG_GameMessage.AnnounceNumber, new AnnounceNumberParser() },
                { OCG_GameMessage.CardHint, basicParser },
                { OCG_GameMessage.PlayerHint, new PlayerHintParser() },
                { OCG_GameMessage.MatchKill, basicParser },
                { OCG_GameMessage.RemoveCards, new RemoveCardsParser() }
            };
        }
    }
}