using System;
using Ygo.Core.Commands.Abstract;

namespace Ygo.Core.Commands
{
    public class MainDeckClickCommand : IGameCommand
    {

        public Guid RequesterId { get; }
        public Guid OwnerId { get; }
        
        public MainDeckClickCommand(Guid requesterId, Guid ownerId)
        {
            RequesterId = requesterId;
            OwnerId = ownerId;
        }
    }
}