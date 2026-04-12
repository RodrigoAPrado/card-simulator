using System;
using Ygo.Core.Abstract;
using Ygo.Core.Commands.Abstract;

namespace Ygo.Core.Commands
{
    public class CardInHandClickCommand : IGameCommand
    {

        public Guid RequesterId { get; }
        public Guid OwnerId { get; }
        public ICardInstance Card { get; }

        public CardInHandClickCommand(Guid requesterId, Guid ownerId, ICardInstance card)
        {
            RequesterId = requesterId;
            OwnerId = ownerId;
            Card = card;
        }
    }
}