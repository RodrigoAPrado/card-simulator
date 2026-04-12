using System;
using System.Collections.Generic;
using Ygo.Core.Abstract;
using Ygo.Core.Board.Abstract;
using Ygo.Core.Commands;
using Ygo.Core.Commands.Abstract;

namespace Ygo.Core.Interaction.Abstract
{
    public abstract class MonsterCardSelectionState : IInteractionState
    {
        public IList<ICardInstance> AvailableCards { get; }
        protected readonly Guid _playerId;
        protected readonly GameState _gameState;

        protected MonsterCardSelectionState(Guid playerId, GameState gameState, IList<ICardInstance> availableCards)
        {
            _playerId = playerId;
            _gameState = gameState;
            AvailableCards = availableCards;
        }

        public void Handle(IGameCommand command)
        {
            if (_playerId != command.RequesterId)
                return;

            if (command is not CardOnFieldClickCommand cardClickCommand)
            {
                return;
            }

            if (AvailableCards.Contains(cardClickCommand.Card))
            {
                InternalHandle(cardClickCommand);
            }
        }
        protected abstract void InternalHandle(CardOnFieldClickCommand zoneClickCommand);
    }
}