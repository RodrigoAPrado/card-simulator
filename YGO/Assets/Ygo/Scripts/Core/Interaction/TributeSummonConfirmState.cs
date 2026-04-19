using System;
using System.Collections.Generic;
using Ygo.Core.Abstract;
using Ygo.Core.Actions;
using Ygo.Core.Actions.Abstract;
using Ygo.Core.Commands.Abstract;
using Ygo.Core.Interaction.Abstract;

namespace Ygo.Core.Interaction
{
    public class TributeSummonConfirmState : ConfirmationState
    {
        public override string Message => $"You need to tribute {TributeCost} monster(s) to Tribute {SummonName} \"{CardName}\". Do you want to continue?";
        private string CardName => _card.Data.Name;
        private int? TributeCost => _card.TributeCost;
        private readonly ICardInstance _card;
        private readonly bool _isSet;
        private string SummonName => _isSet ? "Set" : "Summon";
        public TributeSummonConfirmState(Guid playerId, GameState gameState, ICardInstance card, bool isSet) 
            : base(playerId, gameState)
        {
            _card = card;
            _isSet = isSet;
        }

        protected override void HandleAccept()
        {
            _gameState.EnqueueActions(new List<IGameAction>
            {
                new DelegatedGameAction(() =>
                {
                    _gameState.ClearInteractionState(_playerId);
                    _gameState.CheckAvailableTributesForSummonOrSet(_card.OwnerId, _card, _isSet);
                })
            });
        }

        protected override void HandleDecline()
        {
            _gameState.EnqueueActions(new List<IGameAction>
            {
                new DelegatedGameAction(() =>
                {
                    _gameState.ClearInteractionState(_playerId);
                    _gameState.CancelAction();
                })
            });
        }
    }
}