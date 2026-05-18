using System.Collections.Generic;
using UnityEngine;
using Ygo.Service;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card;

namespace Ygo.Controller.Data
{
    public class CardImageLibrary
    {
        private readonly IReadOnlyList<ICardData> _cards;
        private readonly bool _small;
        private Dictionary<uint, Sprite> _cardImages;
        
        public CardImageLibrary(IReadOnlyList<ICardData> cards, bool small)
        {
            _cards = cards;
            _small = small;
        }

        public void LoadImages()
        {
            _cardImages = new Dictionary<uint, Sprite>();
            foreach (var card in _cards)
            {
                if (_cardImages.ContainsKey(card.Code))
                    continue;
                var sprite = ImageLoader.LoadSpriteFromFile(card.Code, _small);
                if(sprite == null)
                    sprite = ImageLoader.LoadSpriteFromFile(card.Alias, _small);
                _cardImages.Add(card.Code, sprite);
            }
        }

        public Sprite GetCardImage(uint card)
        {
            return _cardImages.GetValueOrDefault(card);
        }
    }
}