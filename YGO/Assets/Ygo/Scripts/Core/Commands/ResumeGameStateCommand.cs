using System;
using Ygo.Core.Commands.Abstract;

namespace Ygo.Core.Commands
{
    public class ResumeGameStateCommand : IGameCommand
    {
        public Guid RequesterId { get; }

        public ResumeGameStateCommand(Guid requesterId)
        {
            RequesterId = requesterId;
        }
    }
}