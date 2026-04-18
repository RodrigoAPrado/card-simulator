using System;
using Ygo.Core.Abstract;
using Ygo.Core.Commands.Abstract;
using Ygo.Core.Interaction.Abstract;

namespace Ygo.Core.Interaction
{
    public class TributeSummonConfirmState : ConfirmationState
    {
        public string CardName => _card.Data.Name;
        public int? TributeCount => _card.TributeCost;
        private readonly ICardInstance _card;
        public TributeSummonConfirmState(Guid playerId, GameState gameState, ICardInstance card) 
            : base(playerId, gameState)
        {
            _card = card;
        }

        protected override void HandleAccept()
        {
            throw new NotImplementedException();
        }

        protected override void HandleDecline()
        {
            throw new NotImplementedException();
        }
    }
}