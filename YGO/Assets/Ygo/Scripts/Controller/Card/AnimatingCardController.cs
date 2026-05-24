using UnityEngine;
using Ygo.Scripts.Core.Model;
using Ygo.View.Card;

namespace Ygo.Controller.Card
{
    public class AnimatingCardController : MonoBehaviour
    {
        [field: SerializeField]
        private ThumbCardView view;

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
    }
}