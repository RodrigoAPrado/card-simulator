using System.Collections.Generic;
using UnityEngine;
using Ygo.Controller.Card;
using Ygo.Scripts.Application;
using Ygo.Service;

namespace Ygo.Scripts.Controller
{
    public class GameManager : MonoBehaviour
    {
        [Header("CardPrefab")] 
        [field: SerializeField]
        private CardController cardPrefab;

        [Header("HandsArea")] 
        [field: SerializeField]
        private GameObject playerHandArea;

        private GameApplication _application;
        private List<CardController> _cards;
        
        public void Awake()
        {
            var service = new CardLoaderService();
            var data = service.LoadCards();
            _application = new GameApplication(data);
            _application.InitializeGame();
        }

        public void DrawCards()
        {
            if (_cards != null)
            {
                for (var i = _cards.Count-1; i >= 0; i--)
                {
                   Destroy(_cards[i].gameObject);
                   _cards.RemoveAt(i);
                }
            }
            else
            {
                _cards = new List<CardController>();
            }
            var drawnCards = _application.DrawCards();
            foreach (var card in drawnCards)
            {
                var o = Instantiate(cardPrefab, playerHandArea.transform);
                o.Init(card);
                _cards.Add(o);
            }
        }
    }
}