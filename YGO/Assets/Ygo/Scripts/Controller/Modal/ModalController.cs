using UnityEngine;
using Ygo.Controller.Component;
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

        public void Init()
        {
            okButton.Init(() => {}, "Ok");
            cancelButton.Init(() => {}, "Cancel");
            Hide();
        }

        public void Hide()
        {
            okButton.gameObject.SetActive(false);
            cancelButton.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }

        public void ShowCardsModal(bool cancelable)
        {
            cancelButton.gameObject.SetActive(cancelable);
        }
    }
}