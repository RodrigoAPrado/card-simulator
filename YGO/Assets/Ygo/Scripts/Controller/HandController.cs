using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Ygo.Controller.Card;
using Ygo.Controller.Data;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;

namespace Ygo.Controller
{
    public class HandController : MonoBehaviour
    {
        [field:SerializeField]
        private ThumbnailCardController[] cardControllers;
        [field: SerializeField] 
        private PointOfView pointOfView;
        private byte _cardsInHand = 0;
        private CardImageLibrary _library;
        private IReadOnlyDictionary<uint, ICardData> _cards;

        public void Init(
            CardImageLibrary library, 
            IReadOnlyDictionary<uint, ICardData> cards, 
            Action<ICardData, bool> onHover)
        {
            _library = library;
            _cards = cards;
            foreach (var controller in cardControllers)
            {
                controller.Init(onHover);
            }
        }

        public async UniTask Draw(IReadOnlyList<IDrawnCard> cardsDrawn)
        {
            foreach (var card in cardsDrawn)
            {
                if (_cardsInHand >= cardControllers.Length)
                    return;
                var cardData = _cards.GetValueOrDefault(card.CardCode);
                cardControllers[_cardsInHand].Enable();
                cardControllers[_cardsInHand].UpdateCard(cardData, _library.GetCardImage(card.CardCode));
                Debug.Log($"(Card Drawn) Card={card}, Index={_cardsInHand}");
                _cardsInHand++;
            }
        }
    }
}