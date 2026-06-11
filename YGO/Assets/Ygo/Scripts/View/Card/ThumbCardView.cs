using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Ygo.View.Card
{
    public class ThumbCardView : MonoBehaviour
    {
        [field: SerializeField]
        private Image CardFrontImage { get; set; }
        [field: SerializeField]
        private Image CardBack { get; set; }
        
        public Transform FrontTransform => CardFrontImage.transform;
        public Transform BackTransform => CardBack.transform;

        public void SetIllustration(Sprite illustration)
        {
            CardFrontImage.sprite = illustration;
            ShowFront();
        }

        public void ShowBack()
        {
            CardBack.gameObject.SetActive(true);
            CardFrontImage.gameObject.SetActive(false);
        }

        public void ShowFront()
        {
            CardBack.gameObject.SetActive(false);
            CardFrontImage.gameObject.SetActive(true);
        }

        public void HideAll()
        {
            CardBack.gameObject.SetActive(false);
            CardFrontImage.gameObject.SetActive(false);
        }
    }
}