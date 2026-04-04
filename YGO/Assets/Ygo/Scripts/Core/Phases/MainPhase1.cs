using System;
using Ygo.Core.Abstract;
using Ygo.Core.Phases.Abstract;
using Ygo.Core.Response;

namespace Ygo.Core.Phases
{
    public class MainPhase1 : BaseGamePhase
    {
        public override string Name => "Main Phase 1";

        private bool _normalSummoned;
        private MainGameState _currentGameState;
        
        public MainPhase1(IGamePhase nextPhase, CardsHandler cardsHandler, Action advancePhase) 
            : base(nextPhase, cardsHandler, advancePhase)
        {
        }

        public override void Init()
        {
            _normalSummoned = false;
            _currentGameState = MainGameState.Open;
        }

        public override ClickedOnCardHandResponse ClickedOnCardInHand(ICardInstance card)
        {
            if (_normalSummoned || _currentGameState != MainGameState.Open)
            {
                return new ClickedOnCardHandResponse(true);
            }

            if (!card.IsValidMonster)
                throw new InvalidOperationException($"Card is not a valid monster");
            
            var response = new ClickedOnCardHandResponse(false);
            
            var level = card.Data.MonsterData.Level;
            
            switch (level)
            {
                case <= 4:
                    response.NormalSummon = true;
                    response.NormalSet = true;
                    break;
                case <= 6:
                    response.TributeAmount = 1;
                    response.TributeSummon = true;
                    response.TributeSet = true;
                    break;
                default:
                    response.TributeAmount = 2;
                    response.TributeSummon = true;
                    response.TributeSet = true;
                    break;
            }

            return response;
        }
    }

    internal enum MainGameState
    {
        Open = 0,
        SelectingZone = 1,
        
    }
}