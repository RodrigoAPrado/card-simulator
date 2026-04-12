using System;
using System.Collections.Generic;
using Ygo.Core.Abstract;
using Ygo.Core.Actions;
using Ygo.Core.Board.Abstract;
using Ygo.Core.Commands;
using Ygo.Core.Interaction.Abstract;

namespace Ygo.Core.Interaction
{
    public class AttackTargetSelectState : MonsterCardSelectionState
    {
        private readonly ICardInstance _attacker;

        public AttackTargetSelectState(
            Guid playerId,
            GameState gameState,
            IList<ICardInstance> availableCards,
            ICardInstance cardInstance
        )
            : base(playerId, gameState, availableCards)
        {
            _attacker = cardInstance;
        }

        protected override void InternalHandle(CardOnFieldClickCommand cardOnFieldClickCommand)
        {
            var gameAction = new DelegatedGameAction("Declare Attack",
                () =>
                {
                    _gameState.DeclareAttack(_playerId, _attacker, cardOnFieldClickCommand.Card);
                    _gameState.ClearInteractionState(_playerId);
                });
            _gameState.ExecuteAction(gameAction);
        }
    }
}