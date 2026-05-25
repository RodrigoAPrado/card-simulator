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

        public async UniTask MoveCard(RectTransform thisPosition, RectTransform targetPosition)
        {
            await animateView.MoveCard(thisPosition, targetPosition);
        }
    }
}