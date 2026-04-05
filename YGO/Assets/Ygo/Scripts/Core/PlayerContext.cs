using System;

namespace Ygo.Core
{
    public class PlayerContext
    {
        public Guid Id { get; }
        public CardsHandler CardsHandler { get; private set; }
        public int CurrentLifePoints { get; private set; }
        public int PreviousLifePoints  { get; private set; }
        
        public bool Lost => CurrentLifePoints <= 0 || _overDeck;
        public bool ShowViewPoint { get; }
        public bool NormalSummonFlag { get; private set; }
        
        private bool _overDeck;

        public PlayerContext(CardsHandler cardsHandler, int startingLifePoints, bool showViewPoint)
        {
            Id = new Guid();
            CardsHandler = cardsHandler;
            CurrentLifePoints = startingLifePoints;
            PreviousLifePoints = CurrentLifePoints;
            ShowViewPoint = showViewPoint;
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

        public void SetTurnPlayer()
        {
            NormalSummonFlag = false;
        }

        public void SetNormalSummoned()
        {
            NormalSummonFlag = true;
        }
    }
}