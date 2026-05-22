using System;
using UnityEngine;
using Ygo.Controller.Card;
using Ygo.Controller.Component;
using Ygo.Controller.Data;
using Ygo.Scripts.Core.Model;
using Ygo.View;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card;

namespace Ygo.Controller.Modal
{
    public class ConfirmEffectModalController : MonoBehaviour
    {
        [field: SerializeField]
        private TextViewUI title;
        [field: SerializeField]
        private ButtonController okButton;
        [field: SerializeField]
        private ButtonController cancelButton;
        [field: SerializeField] 
        private ThumbnailCardController cardController;
        [field: SerializeField] 
        private TextViewUI effectText;
        private CardImageLibrary _library;

        public void Init(CardImageLibrary library)
        {
            _library = library;
            
            okButton.Init(() => {}, "Yes");
            cancelButton.Init(() => {}, "No");
            cardController.Init((v1, v2) => {});
            Hide();
        }

        public void ShowSelectedCard(CardModel cardModel, Action confirm, Action cancel)
        {
            title.SetText("Activate selected card?");
            cardController.Enable();
            cardController.UpdateCard(cardModel, _library.GetCardImage(cardModel.Data.Code));
            okButton.SetAction(() =>
            {
                Hide();
                confirm?.Invoke();
            });
            cancelButton.SetAction(() =>
            {
                Hide();
                cancel?.Invoke();
            });
            string text;
            if (cardModel.Description == "Activate")
            {
                text = "Activate this card:\n" + cardModel.Data.Description;
            }
            else
            {
                text = cardModel.Description;
            }
            effectText.SetText(text);
            okButton.gameObject.SetActive(true);
            cancelButton.gameObject.SetActive(true);
            gameObject.SetActive(true);
        }
        
        private void Hide()
        {
            cardController.Disable();
            okButton.gameObject.SetActive(false);
            cancelButton.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}