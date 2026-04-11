using System;
using Ygo.Core.Abstract;
using Ygo.Core.Board.Abstract;
using Ygo.Core.Commands.Abstract;

namespace Ygo.Core.Commands
{
    public class ZoneClickCommand : IGameCommand
    {
        public Guid PlayerId { get; }
        public IBoardZone Zone { get; }

        public ZoneClickCommand(Guid playerId, IBoardZone zone)
        {
            PlayerId = playerId;
            Zone = zone;
        }
    }
}