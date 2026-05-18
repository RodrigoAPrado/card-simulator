using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Ygo.Controller.Card;
using Ygo.Controller.Data;

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

        public void Init(CardImageLibrary library)
        {
            _library = library;
            foreach (var controller in cardControllers)
            {
                controller.Init();
            }
        }

        public async UniTask Draw(List<uint> cardsDrawn)
        {
            foreach (var card in cardsDrawn)
            {
                if (_cardsInHand >= cardControllers.Length)
                    return;
                cardControllers[_cardsInHand].Enable();
                cardControllers[_cardsInHand].UpdateCard(card, _library.GetCardImage(card));
                Debug.Log($"(Card Drawn) Card={card}, Index={_cardsInHand}");
                _cardsInHand++;
            }
        }
    }
}