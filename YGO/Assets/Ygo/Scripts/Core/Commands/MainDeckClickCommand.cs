using System;
using Ygo.Core.Commands.Abstract;

namespace Ygo.Core.Commands
{
    public class MainDeckClickCommand : IGameCommand
    {
        public Guid PlayerId { get; }
        
        public MainDeckClickCommand(Guid playerId)
        {
            PlayerId = playerId;
        }
    }
}