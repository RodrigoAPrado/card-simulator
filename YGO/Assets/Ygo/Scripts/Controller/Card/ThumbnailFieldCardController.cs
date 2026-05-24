using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Ygo.Scripts.Core.Model;
using Ygo.View.Card;
using Ygo.View.Component;

namespace Ygo.Controller.Card
{
    public class ThumbnailFieldCardController : MonoBehaviour
    {
        [field: SerializeField] 
        private ThumbCardView view;

        [field: SerializeField] 
        private CardFieldStatsView statsView;
        
        public CardModel CardModel { get; private set; }
        public bool Enabled { get; private set; }
        private bool Hidden { get; set; }

        public void Init()
        {
            Enabled = false;
            gameObject.SetActive(false);
            statsView.Disable();
        }

        public void UpdateCard(CardModel cardModel, Sprite cardImage)
        {
            CardModel = cardModel;
            view.SetIllustration(cardImage);
        }

        public void ShowCard(bool value)
        {
            if (value)
            {
                view.ShowFront();
                statsView.SetStats(
                    CardModel.Data.OriginalAttack.ToString(),
                    CardModel.Data.OriginalDefense.ToString(),
                    CardModel.Data.Level.ToString());
            }
            else
            {
                view.ShowBack();
                statsView.Disable();
            }
        }

        public void ToggleAtkDefMode(bool isDefMode)
        {
            statsView.ToggleAtkDefMode(isDefMode);
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