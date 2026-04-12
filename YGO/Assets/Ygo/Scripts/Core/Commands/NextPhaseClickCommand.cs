using System;
using Ygo.Core.Commands.Abstract;

namespace Ygo.Core.Commands
{
    public class NextPhaseClickCommand : IGameCommand
    {
        public Guid RequesterId { get; }

        public NextPhaseClickCommand(Guid requesterId)
        {
            RequesterId = requesterId;
        }
    }
}