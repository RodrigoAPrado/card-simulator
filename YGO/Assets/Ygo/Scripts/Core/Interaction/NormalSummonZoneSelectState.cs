using System;
using System.Collections.Generic;
using Ygo.Core.Abstract;
using Ygo.Core.Actions;
using Ygo.Core.Actions.Abstract;
using Ygo.Core.Board.Abstract;
using Ygo.Core.Commands;
using Ygo.Core.Interaction.Abstract;

namespace Ygo.Core.Interaction
{
    public class NormalSummonZoneSelectState : ZoneSelectionState 
    {
        private readonly ICardInstance _cardInstance;
        private bool _isTribute;
        
        public NormalSummonZoneSelectState(
            Guid playerId,
            GameState gameState, 
            IList<IBoardZone> availableZones, 
            ICardInstance cardInstance,
            bool isTribute
            ) 
            : base(playerId, gameState, availableZones)
        {
            _cardInstance = cardInstance;
            _isTribute = isTribute;
        }

        protected override void InternalHandle(ZoneClickCommand zoneClickCommand)
        {
            _gameState.EnqueueActions(new List<IGameAction>
            {
                new DelegatedGameAction(() =>
                {
                    _gameState.ClearInteractionState(_playerId);
                    _gameState.DoNormalSummon(_playerId, _cardInstance, zoneClickCommand.Zone, _isTribute);
                })
            });
        }
    }
}