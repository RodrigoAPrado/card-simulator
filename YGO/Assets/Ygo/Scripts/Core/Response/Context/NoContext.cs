using System;
using Ygo.Core.Response.Context.Abstract;

namespace Ygo.Core.Response.Context
{
    public class NoContext : IInteractionContext
    {
        public Guid PlayerId { get; }

        public NoContext(Guid playerId)
        {
            PlayerId = playerId;
        }
    }
}