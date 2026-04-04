using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Ygo.Core.Abstract;
using Ygo.Data;
using Random = System.Random;

namespace Ygo.Core
{
    public class GameState
    {
        public PhasesMachine Phases { get; private set; }
        public CardsHandler CardsHandler { get; private set; }
        
        public void Setup(ICardRepository repo)
        {
            CardsHandler = new CardsHandler();
            CardsHandler.Setup(repo);
            CardsHandler.ShuffleDeck();
            Phases = new PhasesMachine();
            Phases.Setup(CardsHandler);
        }

        public void Init()
        {
            Phases.Init();
        }

        public void ShuffleDeck()
        {
            CardsHandler.ShuffleDeck();
        }

        public void DrawCard(int amount)
        {
            CardsHandler.DrawCards(amount);
        }
    }
}