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
        
        public override GameStep CurrentStep => _currentGameStep;
        private GameStep _currentGameStep;

        public MainPhase1(IGamePhase nextPhase, Action advancePhase) 
            : base(nextPhase, advancePhase)
        {
        }

        public override void Init(TurnContext context)
        {
            base.Init(context);
            _currentGameStep = GameStep.Open;
        }

        public override ClickedOnCardHandResponse ClickedOnCardInHand(ICardInstance card)
        {
            if (_currentGameStep != GameStep.Open
                || _context.CurrentTurnPlayer.NormalSummonFlag 
                || !_context.CurrentTurnPlayer.BoardHandler.IsAnyFree(ZoneType.MainMonsterZone))
            {
                return new ClickedOnCardHandResponse(null);
            }

            if (!card.IsValidMonster)
                throw new InvalidOperationException($"Card is not a valid monster");
            
            return new ClickedOnCardHandResponse(card)
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
                _currentGameStep = GameStep.SelectingZoneToSummonMonster;
            }
            
            return new WhereToSummonMonsterResponse(availableZones);
        }

        public override void CancelSummoning()
        {
            if(_currentGameStep != GameStep.SelectingZoneToSummonMonster)
                throw new InvalidOperationException("Can't cancel the summoning while not selecting zone");

            _currentGameStep = GameStep.Open;
        }

        public override bool SummonCardOnSelectedZone(ICardInstance card, IBoardZone zone)
        {
            var result = zone.TryPutCard(card);
            if (!result.Ok) 
                return false;
            
            _context.CurrentTurnPlayer.SetNormalSummoned();
            _context.CurrentTurnPlayer.CardsHandler.RemoveCardFromHand(card);
            card.SetLocation(zone.Position.ToMonsterCardLocation());
            _currentGameStep = GameStep.OnMonsterSummoned;
            return true;
        }

        public override void ToOpenGameStep()
        {
            _currentGameStep = GameStep.Open;
        }
    }
}