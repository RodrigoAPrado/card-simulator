using System;
using Ygo.Core.Abstract;
using Ygo.Core.Commands.Abstract;

namespace Ygo.Core.Commands
{
    public class CardOnFieldClickCommand : IGameCommand
    {

        public Guid RequesterId { get; }
        public Guid OwnerId { get; }
        public ICardInstance Card { get; }

        public CardOnFieldClickCommand(Guid requesterId, Guid ownerId, ICardInstance card)
        {
            RequesterId = requesterId;
            OwnerId = ownerId;
            Card = card;
        }
    }
}