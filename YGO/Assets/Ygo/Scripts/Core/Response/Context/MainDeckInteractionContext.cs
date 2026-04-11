using System;
using Ygo.Core.Response.Context.Abstract;

namespace Ygo.Core.Response.Context
{
    public class MainDeckInteractionContext : IInteractionContext
    {
        public Guid PlayerId { get; }

        public MainDeckInteractionContext(Guid playerId)
        {
            PlayerId = playerId;
        }
    }
}