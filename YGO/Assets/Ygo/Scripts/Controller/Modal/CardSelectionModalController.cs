using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Ygo.Controller.Card;
using Ygo.Controller.Component;
using Ygo.Controller.Data;
using Ygo.Core.Duel;
using Ygo.Scripts.Core.Event;
using Ygo.Scripts.Core.Event.Base;
using Ygo.Scripts.Core.Model;
using Ygo.View;

namespace Ygo.Controller.Modal
{
    public class CardSelectionModalController : MonoBehaviour
    {
        [field: SerializeField]
        private TextViewUI title;
        [field: SerializeField]
        private ButtonController okButton;
        [field: SerializeField]
        private ButtonController cancelButton;
        [field: SerializeField] 
        private ThumbnailCardController[] cardControllers;
        [field: SerializeField] 
        private ConfirmEffectModalController confirmEffectModal;
        private CardImageLibrary _library;
        private DuelInstance _duelInstance;

        public void Init(DuelInstance duelInstance, CardImageLibrary library)
        {
            _duelInstance = duelInstance;
            _library = library;
            foreach (var controller in cardControllers)
            {
                controller.Init((v1, v2) => {});
            }
            okButton.Init(() => {}, "Ok");
            cancelButton.Init(() => {}, "Cancel");
            Hide();
            _duelInstance.EventQueue.Subscribe<SelectChainEvent>(OnSelectChainEvent);
        }

        private void SoftHide()
        {
            gameObject.SetActive(false);
        }

        private void SoftShow()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            foreach (var controller in cardControllers)
            {
                controller.Disable();
            }
            okButton.gameObject.SetActive(false);
            cancelButton.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }

        private async UniTask OnSelectChainEvent(SelectChainEvent e)
        {
            gameObject.SetActive(true);
            cancelButton.gameObject.SetActive(e.CanCancel);
            if (e.CanCancel)
            {
                cancelButton.SetAction(Cancel);
                title.SetText($"Player {e.Player}, do you want to activate an effect?");
            }
            else
            {
                title.SetText($"Player {e.Player}, please activate the following effects...");
            }
            SetupControllers(e.Cards);
        }

        private void SetupControllers(IReadOnlyList<CardModel> cards)
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
                var cardModel = cards[i];
                var index = i;
                cardControllers[i].UpdateCard(cards[i], _library.GetCardImage(cards[i].Data.Code));
                cardControllers[i].Enable();
                cardControllers[i].SetAction(() => SelectCard(index, cardModel));
            }

            foreach (var controller in cardControllers)
            {
                if(controller.Dirty)
                    controller.Disable();
            }
        }

        private void SelectCard(int index, CardModel cardModel)
        {
            SoftHide();
            confirmEffectModal.ShowSelectedCard(cardModel, () => ConfirmSelection(index), CancelSelection);
        }

        private void ConfirmSelection(int index)
        {
            Hide();
            _ = _duelInstance.SetResponse(new List<int>() { index });
        }

        private void CancelSelection()
        {
            SoftShow();
        }

        private void Cancel()
        {
            Hide();
            _ = _duelInstance.SetResponse(new List<int>() { -1 });
        }
    }
}