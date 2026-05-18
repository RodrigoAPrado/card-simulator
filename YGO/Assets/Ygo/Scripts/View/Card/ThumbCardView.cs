using UnityEngine;
using UnityEngine.UI;

namespace Ygo.View.Card
{
    public class ThumbCardView : MonoBehaviour
    {
        [field: SerializeField]
        private GameObject FieldStats { get; set; }
        [field: SerializeField]
        private Image CardFrontImage { get; set; }
        [field: SerializeField]
        private Image CardBack { get; set; }
        
        public void ToggleField(bool fieldMode)
        {
            FieldStats.SetActive(fieldMode);
        }

        public void SetIllustration(Sprite illustration)
        {
            CardBack.gameObject.SetActive(false);
            CardFrontImage.sprite = illustration;
        }

        public void Clear()
        {
            
        }
    }
}