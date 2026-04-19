using System;
using Ygo.Core.Commands.Abstract;

namespace Ygo.Core.Commands
{
    public class PauseGameStateCommand : IGameCommand
    {
        public Guid RequesterId { get; }

        public PauseGameStateCommand(Guid requesterId)
        {
            RequesterId = requesterId;
        }
    }
}