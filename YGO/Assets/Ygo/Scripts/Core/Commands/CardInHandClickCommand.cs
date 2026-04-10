using Ygo.Core.Abstract;
using Ygo.Core.Commands.Abstract;

namespace Ygo.Core.Commands
{
    public class CardInHandClickCommand : IGameCommand
    {
        public ICardInstance Card { get; }

        public CardInHandClickCommand(ICardInstance card)
        {
            Card = card;
        }
    }
}