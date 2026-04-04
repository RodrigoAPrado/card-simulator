using System;
using System.Collections.Generic;
using Ygo.Core.Abstract;

namespace Ygo.Core
{
    public class GameState : IGameState
    {
        public IList<ICardInstance> CardsDrawn => _cardsDrawn.AsReadOnly();

        private List<ICardInstance> _cardsDrawn;
        
        public void DrawCards(ICardRepository repo, int amount)
        {
            var availableIds = repo.IdsList;
            var rng = new Random();
            _cardsDrawn = new List<ICardInstance>();
            for (var i = 0; i < amount; i++)
            {    
                _cardsDrawn.Add(new CardInstance(repo.GetCardById(availableIds[rng.Next(0, availableIds.Count)])));
            }
        }
    }
}