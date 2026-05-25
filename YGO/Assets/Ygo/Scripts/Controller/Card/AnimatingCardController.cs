using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Ygo.View.Card;

namespace Ygo.Controller.Card
{
    public class AnimatingCardController : MonoBehaviour
    {
        [field: SerializeField]
        private ThumbCardView view;
        [field: SerializeField]
        private ThumbCardAnimateView animateView;

        public void Init()
        {
            gameObject.SetActive(false);
        }

        public void Show(Sprite cardImage)
        {
            gameObject.SetActive(true);
            view.SetIllustration(cardImage);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public async UniTask MoveCardHand(RectTransform thisPosition, RectTransform targetPosition)
        {
            await animateView.MoveCard(thisPosition, targetPosition);
        }

        public async UniTask MoveCardHandX(RectTransform thisPosition, RectTransform targetPosition)
        {
            await animateView.MoveCardX(thisPosition, targetPosition);
        }

        public async UniTask MoveCardField(Transform targetPosition,
            RectTransform thisPositionRect, RectTransform targetPositionRect)
        {
            await animateView.MoveCardField(targetPosition, thisPositionRect, targetPositionRect);
        }
    }
}