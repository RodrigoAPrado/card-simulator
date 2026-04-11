using System;
using Ygo.Core.Events.Abstract;
using Ygo.Core.Interaction.Abstract;

namespace Ygo.Core.Events
{
    public class InteractionStateSetEvent : IGameEvent
    {
        public Guid PlayerId { get; }
        public IInteractionState InteractionState { get; }

        public InteractionStateSetEvent(Guid playerId, IInteractionState interactionState)
        {
            PlayerId = playerId;
            InteractionState = interactionState;
        }
    }
}