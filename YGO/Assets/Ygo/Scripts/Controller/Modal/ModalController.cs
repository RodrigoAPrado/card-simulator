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
    public class ModalController : MonoBehaviour
    {
        [field: SerializeField]
        private TextViewUI title;
        [field: SerializeField]
        private ButtonController okButton;
        [field: SerializeField]
        private ButtonController cancelButton;
        [field: SerializeField] 
        private ThumbnailCardController[] cardControllers;
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
            if(e.CanCancel)
                cancelButton.SetAction(() => SelectCard(-1));
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
                cardControllers[i].UpdateCard(cards[i].Data, _library.GetCardImage(cards[i].Data.Code));
                cardControllers[i].Enable();
                var index = i;
                cardControllers[i].SetAction(() => SelectCard(index));
            }

            foreach (var controller in cardControllers)
            {
                if(controller.Dirty)
                    controller.Disable();
            }
        }

        private void SelectCard(int index)
        {
            Hide();
            _ = _duelInstance.SetResponse(new List<int>() { index });
        }
    }
}