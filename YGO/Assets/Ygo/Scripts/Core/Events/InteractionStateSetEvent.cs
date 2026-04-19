using System;
using Ygo.Core.Events.Abstract;
using Ygo.Core.Interaction.Abstract;

namespace Ygo.Core.Events
{
    public class InteractionStateSetEvent : IGameEvent
    {
        public Guid RequesterId { get; }
        public IInteractionState InteractionState { get; }

        public InteractionStateSetEvent(Guid requesterId, IInteractionState interactionState)
        {
            RequesterId = requesterId;
            InteractionState = interactionState;
        }
    }
}