using System;
using Ygo.Core.Abstract;
using Ygo.Core.Response.Context.Abstract;

namespace Ygo.Core.Response.Context
{
    public class CardInteractionContext : IInteractionContext
    {
        public Guid PlayerId { get; }
        public ICardInstance Card { get; }

        public CardInteractionContext(Guid playerId, ICardInstance card)
        {
            PlayerId = playerId;
            Card = card;
        }
    }
}