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
        public IList<ICardInstance> AvailableCards => _availableCards.AsReadOnly();
        protected List<ICardInstance> _availableCards;
        protected readonly Guid _playerId;
        protected readonly GameState _gameState;

        protected MonsterCardSelectionState(Guid playerId, GameState gameState, List<ICardInstance> availableCards)
        {
            _playerId = playerId;
            _gameState = gameState;
            _availableCards = availableCards;
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