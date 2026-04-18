using System;
using Ygo.Core.Commands.Abstract;

namespace Ygo.Core.Commands
{
    public class PlayerConfirmationCommand : IGameCommand
    {
        public Guid RequesterId { get; }
        public bool Accept { get; }

        public PlayerConfirmationCommand(Guid requesterId, bool accept)
        {
            RequesterId = requesterId;
            Accept = accept;
        }
    }
}