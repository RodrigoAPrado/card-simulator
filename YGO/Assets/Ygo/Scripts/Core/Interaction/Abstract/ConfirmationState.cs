using System;
using Ygo.Core.Commands;
using Ygo.Core.Commands.Abstract;

namespace Ygo.Core.Interaction.Abstract
{
    public abstract class ConfirmationState : IInteractionState
    {
        public abstract string Message { get; }
        protected readonly Guid _playerId;
        protected readonly GameState _gameState;
        
        protected ConfirmationState(Guid playerId, GameState gameState)
        {
            _playerId = playerId;
            _gameState = gameState;
        }

        public void Handle(IGameCommand command)
        {
            if (_playerId != command.RequesterId)
                return;
            
            if (command is not PlayerConfirmationCommand playerConfirmationCommand)
            {
                return;
            }

            if (playerConfirmationCommand.Accept)
            {
                HandleAccept();
                return;
            }
            HandleDecline();
        }

        protected abstract void HandleAccept();
        protected abstract void HandleDecline();
    }
}