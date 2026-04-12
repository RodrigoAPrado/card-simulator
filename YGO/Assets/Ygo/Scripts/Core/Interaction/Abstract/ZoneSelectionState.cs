using System;
using System.Collections.Generic;
using Ygo.Core.Board.Abstract;
using Ygo.Core.Commands;
using Ygo.Core.Commands.Abstract;
using Ygo.Core.Interaction.Abstract;

namespace Ygo.Core.Interaction.Abstract
{
    public abstract class ZoneSelectionState : IInteractionState
    {
        public IList<IBoardZone> AvailableZones { get; }
        protected readonly Guid _playerId;
        protected readonly GameState _gameState;
        
        protected ZoneSelectionState(Guid playerId, GameState gameState, IList<IBoardZone> availableZones)
        {
            _playerId = playerId;
            _gameState = gameState;
            AvailableZones = availableZones;
        }

        public void Handle(IGameCommand command)
        {
            if (_playerId != command.RequesterId)
                return;
            
            if (command is not ZoneClickCommand zoneClickCommand)
            {
                return;
            }
            
            if (AvailableZones.Contains(zoneClickCommand.Zone))
            {
                InternalHandle(zoneClickCommand);
            }
        }

        protected abstract void InternalHandle(ZoneClickCommand zoneClickCommand);
    }
}