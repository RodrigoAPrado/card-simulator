using System;
using Ygo.Core.Abstract;
using Ygo.Core.Phases.Abstract;
using Ygo.Core.Response;

namespace Ygo.Core.Phases
{
    public class MainPhase1 : BaseGamePhase
    {
        public override string Name => "Main Phase 1";

        private MainGameState _currentGameState;
        private TurnContext _context;
        
        public MainPhase1(IGamePhase nextPhase, Action advancePhase) 
            : base(nextPhase, advancePhase)
        {
        }

        public override void Init(TurnContext context)
        {
            _currentGameState = MainGameState.Open;
            _context = context;
        }

        public override ClickedOnCardHandResponse ClickedOnCardInHand(ICardInstance card)
        {
            if (_context.CurrentTurnPlayer.NormalSummonFlag || _currentGameState != MainGameState.Open)
            {
                return new ClickedOnCardHandResponse(true);
            }

            if (!card.IsValidMonster)
                throw new InvalidOperationException($"Card is not a valid monster");
            
            var response = new ClickedOnCardHandResponse(false);
            
            
            response.NormalSummon = true;
            response.NormalSet = true;
            return response;
            
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