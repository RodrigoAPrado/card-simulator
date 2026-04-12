using System;
using System.Collections.Generic;
using Ygo.Core.Abstract;
using Ygo.Core.Actions;
using Ygo.Core.Board.Abstract;
using Ygo.Core.Commands;
using Ygo.Core.Interaction.Abstract;

namespace Ygo.Core.Interaction
{
    public class NormalSetZoneSelectState : ZoneSelectionState
    {
        private readonly ICardInstance _cardInstance;

        public NormalSetZoneSelectState(
            Guid playerId,
            GameState gameState,
            IList<IBoardZone> availableZones,
            ICardInstance cardInstance
        )
            : base(playerId, gameState, availableZones)
        {
            _cardInstance = cardInstance;
        }

        protected override void InternalHandle(ZoneClickCommand zoneClickCommand)
        {
            var gameAction = new DelegatedGameAction("Do Normal Set",
                () =>
                {
                    _gameState.DoNormalSet(_playerId, _cardInstance, zoneClickCommand.Zone);
                    _gameState.ClearInteractionState(_playerId);
                });
            _gameState.ExecuteAction(gameAction);
        }
    }
}