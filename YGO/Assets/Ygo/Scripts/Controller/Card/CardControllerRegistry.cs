using System.Collections.Generic;
using Ygo.Core.Abstract;

namespace Ygo.Controller.Card
{
    public class CardControllerRegistry
    {
        private readonly Dictionary<ICardInstance, CardController> _dic = new();

        public void Register(ICardInstance card, CardController cardController)
        {
            _dic[card] = cardController;
        }

        public CardController Get(ICardInstance card)
        {
            return _dic.GetValueOrDefault(card);
        }
    }
}