using System.Collections.Generic;
using UnityEngine;
using Ygo.Service;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card;

namespace Ygo.Controller.Data
{
    public class CardImageLibrary
    {
        private readonly IReadOnlyDictionary<uint, ICardData> _cards;
        private readonly bool _small;
        private Dictionary<uint, Sprite> _cardImages;
        
        public CardImageLibrary(IReadOnlyDictionary<uint, ICardData> cards, bool small)
        {
            _cards = cards;
            _small = small;
        }

        public void LoadImages()
        {
            _cardImages = new Dictionary<uint, Sprite>();
            foreach (var card in _cards)
            {
                if (_cardImages.ContainsKey(card.Key))
                    continue;
                var sprite = ImageLoader.LoadSpriteFromFile(card.Key, _small);
                if(sprite == null)
                    sprite = ImageLoader.LoadSpriteFromFile(card.Value.Alias, _small);
                _cardImages.Add(card.Key, sprite);
            }
        }

        public Sprite GetCardImage(uint card)
        {
            return _cardImages.GetValueOrDefault(card);
        }
    }
}