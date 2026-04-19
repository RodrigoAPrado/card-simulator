using System;
using System.Collections.Generic;
using Ygo.Core.Abstract;
using Ygo.Core.Actions;
using Ygo.Core.Actions.Abstract;
using Ygo.Core.Commands;
using Ygo.Core.Interaction.Abstract;

namespace Ygo.Core.Interaction
{
    public class TributeSelectingState : MonsterCardSelectionState
    {
        private readonly ICardInstance _card;
        private readonly List<ICardInstance> _selectedCards = new List<ICardInstance>();
        private bool _isSet;
        private bool CanProceed => _selectedCards.Count == _card.TributeCost;
        
        public TributeSelectingState(
            Guid playerId, 
            GameState gameState, 
            List<ICardInstance> availableCards,
            ICardInstance card,
            bool isSet) 
            : base(playerId, gameState, availableCards)
        {
            _card = card;
            _isSet = isSet;
        }

        protected override void InternalHandle(CardOnFieldClickCommand zoneClickCommand)
        {
            _selectedCards.Add(zoneClickCommand.Card);
            _availableCards.Remove(zoneClickCommand.Card);
            if (!CanProceed)
            {
                _gameState.EnqueueActions(new List<IGameAction>()
                {
                    new DelegatedGameAction(() =>
                    {
                        _gameState.SetInteractionState(_playerId, this);
                    })
                });
                return;
            }

            var actions = new List<IGameAction>()
            {
                new DelegatedGameAction(() => _gameState.ClearInteractionState(_playerId))
            };

            foreach (var card in _selectedCards)
            {
                actions.Add(new DelegatedGameAction(() => _gameState.TributeMonster(_playerId, card)));
            }

            actions.Add(_isSet
                ? new DelegatedGameAction(() => _gameState.CheckNormalSet(_playerId, _card, true))
                : new DelegatedGameAction(() => _gameState.CheckNormalSummon(_playerId, _card, true)));

            _gameState.EnqueueActions(actions);
        }
    }
}