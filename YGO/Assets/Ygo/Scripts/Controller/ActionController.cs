using System;
using UnityEngine;
using Ygo.Controller.Component;
using Ygo.Core;
using Ygo.Core.Response;

namespace Ygo.Controller
{
    public class ActionController : MonoBehaviour
    {
        [Header("Actions")] 
        [field: SerializeField]
        private ButtonController normalSummonButton;
        [field: SerializeField]
        private ButtonController setButton;
        [field: SerializeField]
        private ButtonController tributeSummonButton;
        [field: SerializeField]
        private ButtonController tributeSetButton;
        [field: SerializeField]
        private ButtonController attackButton;
        [field: SerializeField]
        private ButtonController cancelButton;
        
        public void Init(GameCommandBus gameCommandBus)
        {
            normalSummonButton.Init(OnNormalSummon, "Normal Summon");
            setButton.Init(OnSet, "Set");
            tributeSummonButton.Init(OnTributeSummon, "Tribute Summon");
            tributeSetButton.Init(OnTributeSet, "Tribute Set");
            attackButton.Init(OnAttack, "Attack");
            cancelButton.Init(OnCancel, "Cancel");
            HideAll();
        }

        private void HideAll()
        {
            normalSummonButton.gameObject.SetActive(false);
            setButton.gameObject.SetActive(false);
            tributeSummonButton.gameObject.SetActive(false);
            tributeSetButton.gameObject.SetActive(false);
            attackButton.gameObject.SetActive(false);
            cancelButton.gameObject.SetActive(false);
        }

        public void Show(ClickedOnCardResponse response, float xPosition, float yPosition)
        {
        }

        public void OnNormalSummon()
        {
        }

        public void OnSet()
        {
        }

        public void OnTributeSummon()
        {
        }

        public void OnTributeSet()
        {
        }
        
        public void OnAttack()
        {
        }
        
        public void OnCancel()
        {
        }
    }
}