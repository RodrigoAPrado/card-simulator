using System;
using Ygo.Core.Abstract;
using Ygo.Core.Board.Abstract;
using Ygo.Core.Phases.Abstract;
using Ygo.Core.Response;

namespace Ygo.Core.Phases
{
    public class MainPhase1 : BaseGamePhase
    {
        public override string Name => "Main Phase 1";

        private MainGameState _currentGameState;
        
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
            if (_currentGameState != MainGameState.Open
                || _context.CurrentTurnPlayer.NormalSummonFlag 
                || !_context.CurrentTurnPlayer.BoardHandler.IsAnyFree(ZoneType.MainMonsterZone))
            {
                return new ClickedOnCardHandResponse(true);
            }

            if (!card.IsValidMonster)
                throw new InvalidOperationException($"Card is not a valid monster");
            
            return new ClickedOnCardHandResponse(false)
            {
                NormalSummon = true,
                NormalSet = true
            };
        }
    }

    internal enum MainGameState
    {
        Open = 0,
        SelectingZone = 1,
        
    }
}