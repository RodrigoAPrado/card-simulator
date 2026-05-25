using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Ygo.Controller.Card;
using Ygo.Controller.Data;
using Ygo.Core.Duel;
using Ygo.Scripts.Core.Enum;
using Ygo.Scripts.Core.Event;
using Ygo.Scripts.Core.Event.Base;
using Ygo.Scripts.Core.Model;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;

namespace Ygo.Controller
{
    public class HandController : MonoBehaviour
    {
        public PointOfView PointOfView => pointOfView;
        
        [field:SerializeField]
        private ThumbnailCardController[] cardControllers;
        [field: SerializeField] 
        private PointOfView pointOfView;
        [field:SerializeField]
        private AnimatingCardController animatingCardControllerPrefab;
        [field: SerializeField] 
        private Transform animatingLayer;
        private CardImageLibrary _library;

        public void Init(
            EventQueue eventQueue,
            CardImageLibrary library,
            Action<CardModel, bool> onHover)
        {
            _library = library;
            foreach (var controller in cardControllers)
            {
                controller.Init(onHover);
            }
            eventQueue.Subscribe<DrawHandEvent>(OnDrawEvent);
        }

        private async UniTask OnDrawEvent(DrawHandEvent e)
        {
            if (e.PointOfView != pointOfView)
                return;

            SetState(e.HandBefore);
            await UniTask.DelayFrame(6);
            SetState(e.HandAfter);
        }

        private void SetState(IReadOnlyList<CardModel> cards)
        {
            foreach (var controller in cardControllers)
            {
                controller.SetDirty();
            }

            for (var i = 0; i < cards.Count; i++)
            {
                if (i >= cardControllers.Length)
                {
                    Debug.Log("Mais carta do que tem controller instanciado. Fazer esquema para resolver isso depois");
                    break;
                }
                cardControllers[i].UpdateCard(cards[i], _library.GetCardImage(cards[i].Data.Code));
                cardControllers[i].Enable();
            }

            foreach (var controller in cardControllers)
            {
                if(controller.Dirty)
                    controller.Disable();
            }
        }

        public async UniTask MoveCardAway(RectTransform targetPosition, AnimatingCardController animatingCard, CardModel card)
        {
            var index = 0;
            var targetController = 0;
            var animatingList = new Dictionary<int, AnimatingCardController>();
            var animatingTransform = new Dictionary<int, RectTransform>();
            var realCardsTransform = new Dictionary<int, RectTransform>();
            RectTransform animatingCardTransform = null;
            var realCardsModel = new List<CardModel>();
            
            foreach (var cardController in cardControllers)
            {
                if (!cardController.Enabled)
                    continue;
                var rectController = cardController.GetComponent<RectTransform>();
                RectTransform rectAnimating;
                if (cardController.CardModel.Data == card.Data)
                {
                    targetController = index;
                    rectAnimating = animatingCard.GetComponent<RectTransform>();
                    rectAnimating.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rectController.rect.width);
                    rectAnimating.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rectController.rect.height);
                    animatingCard.transform.position = new Vector3(cardController.transform.position.x,
                        cardController.transform.position.y, cardController.transform.position.z);
                    animatingCard.Init();
                    animatingCard.Show(_library.GetCardImage(cardController.CardModel.Data.Code));
                    animatingCardTransform = rectAnimating;
                    continue;
                }
                realCardsModel.Add(cardController.CardModel);
                var animatingGhostCard = Instantiate(animatingCardControllerPrefab, animatingLayer);
                animatingGhostCard.transform.position = cardController.transform.position;
                rectAnimating = animatingGhostCard.GetComponent<RectTransform>();
                
                rectAnimating.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rectController.rect.width);
                rectAnimating.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rectController.rect.height);
                
                animatingList.Add(index, animatingGhostCard);
                cardController.HideView();
                animatingGhostCard.Init();
                animatingGhostCard.Show(_library.GetCardImage(cardController.CardModel.Data.Code));
                animatingTransform.Add(index, rectAnimating);
                realCardsTransform.Add(index, rectController);
                index++;
            }
            animatingCard.transform.SetAsLastSibling();
            cardControllers[targetController].Disable();

            if (animatingCardTransform == null)
                throw new InvalidOperationException("No animating card transform");

            // esperar as cartas de verdade se moverem.
            await UniTask.DelayFrame(3);
            
            var tasks = new List<UniTask>();
            tasks.Add(animatingCard.MoveCardHand(animatingCardTransform, targetPosition));

            foreach (var animating in animatingList)
            {
                tasks.Add(
                    animating.Value.MoveCardHandX(animatingTransform[animating.Key], realCardsTransform[animating.Key]));
            }
            
            await UniTask.WhenAll(tasks);
            
            SetState(realCardsModel);
            animatingTransform.Clear();

            foreach (var anim in animatingList)
            {
                Destroy(anim.Value.gameObject);
            }
            animatingList.Clear();
        }
    }
}