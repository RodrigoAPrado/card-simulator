using System;
using Ygo.Core.Abstract;
using Ygo.Core.Board.Abstract;
using Ygo.Core.Enums;
using Ygo.Core.Phases.Abstract;
using Ygo.Core.Response;
using Ygo.Data.Enums;

namespace Ygo.Core.Phases
{
    public class MainPhase1 : BaseGamePhase
    {

        public override string Name => "Main Phase 1";
        
        public MainPhase1(TurnContext context, Action onGameStepChanged) : base(context, onGameStepChanged)
        {
        }

        public override void Init()
        {
            ChangeStep(GameStep.Open);
        }

        public override ClickedOnCardResponse ClickedOnCardInHand(ICardInstance card)
        {
            if (CurrentStep != GameStep.Open
                || _context.CurrentTurnPlayer.NormalSummonFlag 
                || !_context.CurrentTurnPlayer.BoardHandler.IsAnyFree(ZoneType.MainMonsterZone))
            {
                return new ClickedOnCardResponse(null);
            }

            if (!card.IsValidMonster)
                throw new InvalidOperationException($"Card is not a valid monster");
            
            return new ClickedOnCardResponse(card)
            {
                NormalSummon = true
            };
        }

        public override WhereToSummonMonsterResponse CheckWhereToSummonMonster(ICardInstance card)
        {
            if(card.Data.CardType != CardType.Monster)
                throw new InvalidOperationException("Card is not a monster");

            var availableZones 
                = _context.CurrentTurnPlayer.BoardHandler.AvailableZones(ZoneType.MainMonsterZone);

            if (availableZones is { Count: > 0 })
            {
                ChangeStep(GameStep.SelectingZoneToSummonMonster);
            }
            
            return new WhereToSummonMonsterResponse(availableZones);
        }

        public override void CancelSummoning()
        {
            if(CurrentStep != GameStep.SelectingZoneToSummonMonster)
                throw new InvalidOperationException("Can't cancel the summoning while not selecting zone");
            
            ChangeStep(GameStep.Open);
        }

        public override bool SummonCardOnSelectedZone(ICardInstance card, IBoardZone zone)
        {
            var result = zone.TryPutCard(card);
            if (!result.Ok) 
                return false;
            
            _context.CurrentTurnPlayer.SetNormalSummoned();
            _context.CurrentTurnPlayer.CardsHandler.RemoveCardFromHand(card);
            card.Summon(zone);
            ChangeStep(GameStep.OnMonsterSummoned);
            return true;
        }

        public override void ToOpenGameStep()
        {
            ChangeStep(GameStep.Open);
        }

        public override void GoToNextPhase()
        {
            if (CurrentStep != GameStep.Open)
                return;
            
            ChangeStep(GameStep.ProceedToNextPhase);
        }
    }
}