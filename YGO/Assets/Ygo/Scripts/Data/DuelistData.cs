using System;
using System.Collections.Generic;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card;

namespace Ygo.Scripts.Data
{
    public class DuelistData
    {
        public uint StartingLp { get; }
        public uint StartingHand { get; }
        public uint Draw { get; }
        public IReadOnlyList<ICardData> MainDeck { get; }
        public IReadOnlyList<ICardData> ExtraDeck { get; }
        
        private DuelistData(uint startingLp, uint startingHand, uint draw, List<ICardData> mainDeck,
            List<ICardData> extraDeck)
        {
            StartingLp = startingLp;
            StartingHand = startingHand;
            Draw = draw;
            MainDeck = mainDeck;
            ExtraDeck = extraDeck;
        }
        
        public static DuelistDataBuilder CreateBuilder() => new DuelistDataBuilder();
        
        public class DuelistDataBuilder
        {
            public uint StartingLp { get; private set; }
            public uint StartingHand { get; private set; }
            public uint CardsPerDraw { get; private set; }
            public List<ICardData> MainDeck { get; private set; }
            public List<ICardData> ExtraDeck { get; private set; }
            
            public DuelistData Build()
            {
                if (StartingLp == 0)
                    throw new InvalidOperationException($"{nameof(StartingLp)} cannot be 0");
                if (StartingHand == 0 || StartingHand > MainDeck.Count)
                    throw new InvalidOperationException($"{nameof(StartingHand)} cannot be 0");
                if (CardsPerDraw == 0 || CardsPerDraw > (MainDeck.Count - StartingHand + 1))
                    throw new InvalidOperationException($"{nameof(CardsPerDraw)} cannot be 0");
                if (MainDeck == null)
                    throw new InvalidOperationException($"{nameof(MainDeck)} cannot be null");
                if (ExtraDeck == null)
                    throw new InvalidOperationException($"{nameof(ExtraDeck)} cannot be null");
                if (MainDeck.Count is < 40 or > 60)
                    throw new InvalidOperationException($"{nameof(MainDeck)} size must be between 40 and 60");
                if (ExtraDeck.Count > 15)
                    throw new InvalidOperationException($"{nameof(ExtraDeck)} size must not exceed 15");
                return new DuelistData(StartingLp, StartingHand, CardsPerDraw, MainDeck, ExtraDeck);
            }

            public DuelistDataBuilder WithStartingLp(uint startingLp)
            {
                StartingLp = startingLp;
                return this;
            }

            public DuelistDataBuilder WithStartingHand(uint startingHand)
            {
                StartingHand = startingHand;
                return this;
            }

            public DuelistDataBuilder WithCardsPerDraw(uint cardsPerDraw)
            {
                CardsPerDraw = cardsPerDraw;
                return this;
            }

            public DuelistDataBuilder WithMainDeck(List<ICardData> mainDeck)
            {
                MainDeck = mainDeck;
                return this;
            }

            public DuelistDataBuilder WithExtraDeck(List<ICardData> extraDeck)
            {
                ExtraDeck = extraDeck;
                return this;
            }
        }
    }
}