using System;
using Ygo.Core.Abstract;
using Ygo.Core.Board;

namespace Ygo.Core
{
    public class PlayerContext
    {
        public Guid Id { get; }
        public string PlayerName { get; }
        public CardsHandler CardsHandler { get; private set; }
        public BoardHandler BoardHandler { get; private set; }
        public int CurrentLifePoints { get; private set; }
        public int PreviousLifePoints  { get; private set; }
        
        public bool Lost => CurrentLifePoints <= 0 || _overDeck;
        public bool ShowViewPoint { get; }
        public bool NormalSummonFlag { get; private set; }
        
        private bool _overDeck;

        public PlayerContext(string playerName, int startingLifePoints, bool showViewPoint)
        {
            Id = Guid.NewGuid();
            PlayerName = playerName;
            CurrentLifePoints = startingLifePoints;
            PreviousLifePoints = CurrentLifePoints;
            ShowViewPoint = showViewPoint;
        }

        public void Setup(CardsHandler cardsHandler, BoardHandler boardHandler)
        {
            CardsHandler = cardsHandler;
            BoardHandler = boardHandler;
        }

        public bool DrawFromDeck()
        {
            var drawn = CardsHandler.TryDrawFromDeck();
            if (drawn)
                return true;
            _overDeck = true;
            return false;
        }

        public bool DrawFromDeck(int amount)
        {
            for (var i = 0; i < amount; i++)
            {
                var result = DrawFromDeck();
                if (!result)
                    return false;
            }

            return true;
        }

        public void ChangeLifePoints(int value)
        {
            PreviousLifePoints = CurrentLifePoints;
            CurrentLifePoints += value;
        }

        public void ClearFlags()
        {
            NormalSummonFlag = false;
        }

        public void SetNormalSummoned()
        {
            NormalSummonFlag = true;
        }
    }
}