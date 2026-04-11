using System;
using Ygo.Core.Response.Context.Abstract;

namespace Ygo.Core.Response.Context
{
    public class DeckInteractionContext : IInteractionContext
    {
        public Guid PlayerId { get; }

        public DeckInteractionContext(Guid playerId)
        {
            PlayerId = playerId;
        }
    }
}