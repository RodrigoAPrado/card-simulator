using System;
using Ygo.Core.Abstract;
using Ygo.Core.Board.Abstract;
using Ygo.Core.Commands.Abstract;

namespace Ygo.Core.Commands
{
    public class ZoneClickCommand : IGameCommand
    {

        public Guid RequesterId { get; }
        public Guid OwnerId { get; }
        public IBoardZone Zone { get; }

        public ZoneClickCommand(Guid requesterId, Guid playerId, IBoardZone zone)
        {
            RequesterId = requesterId;
            OwnerId = playerId;
            Zone = zone;
        }
    }
}