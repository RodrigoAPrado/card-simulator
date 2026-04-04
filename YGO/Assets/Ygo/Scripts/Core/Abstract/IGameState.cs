using System.Collections.Generic;
using Ygo.Core.Abstract;

namespace Ygo.Core.Abstract
{
    public interface IGameState
    {
        IList<ICardInstance> CardsDrawn { get; }

        void DrawCards(ICardRepository repo, int amount);
    }
}