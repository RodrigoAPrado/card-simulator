using UnityEngine;
using Ygo.Scripts.Core.Model;
using Ygo.View.Card;

namespace Ygo.Controller.Card
{
    public class ThumbnailSimpleCardController : MonoBehaviour
    {
        [field: SerializeField] 
        private ThumbCardView view;
        
        public CardModel CardModel { get; private set; }
        public bool Enabled { get; private set; }
        private bool Hidden { get; set; }

        public void Init()
        {
            Enabled = false;
            gameObject.SetActive(false);
        }

        public void UpdateCard(CardModel cardModel, Sprite cardImage)
        {
            CardModel = cardModel;
            view.SetIllustration(cardImage);
        }

        public void ShowCard()
        {
            view.ShowFront();
            Enable();
        }

        public void OnDestroy()
        {
            CardModel = null;
        }

        public void Enable()
        {
            Enabled = true;
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            Enabled = false;
            CardModel = null;
            gameObject.SetActive(false);
        }
    }
}