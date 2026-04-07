using System;
using UnityEngine;
using Ygo.Core.Response;

namespace Ygo.Controller.Hand
{
    public class HandController : MonoBehaviour
    {
        [Header("Actions")] 
        [field: SerializeField]
        private GameObject normalSummonButton;
        [field: SerializeField]
        private GameObject setButton;
        [field: SerializeField]
        private GameObject tributeSummonButton;
        [field: SerializeField]
        private GameObject tributeSetButton;
        [field: SerializeField]
        private GameObject cancelButton;

        private Action _normalSummonAction;
        private Action _setAction;
        private Action _tributeSummonAction;
        private Action _tributeSetAction;
        private Action _cancelAction;
        
        public void Init(Action normalSummonAction, Action setAction, Action tributeSummonAction, Action tributeSetAction, Action cancelAction)
        {
            _normalSummonAction = normalSummonAction;
            _setAction = setAction;
            _tributeSummonAction = tributeSummonAction;
            _tributeSetAction = tributeSetAction;
            _cancelAction = cancelAction;
        }

        public void HideAll()
        {
            normalSummonButton.SetActive(false);
            setButton.SetActive(false);
            tributeSummonButton.SetActive(false);
            tributeSetButton.SetActive(false);
            cancelButton.SetActive(false);
        }

        public void Show(ClickedOnCardHandResponse response, float xPosition)
        {
            transform.position = new Vector2(xPosition, transform.position.y);
            normalSummonButton.SetActive(response.NormalSummon);
            setButton.SetActive(response.NormalSet);
            tributeSummonButton.SetActive(response.TributeSummon);
            tributeSetButton.SetActive(response.TributeSet);
            cancelButton.SetActive(true);
        }

        public void OnNormalSummon()
        {
            _normalSummonAction?.Invoke();
        }

        public void OnSet()
        {
            _setAction?.Invoke();
        }

        public void OnTributeSummon()
        {
            _tributeSummonAction?.Invoke();
        }

        public void OnTributeSet()
        {
            _tributeSetAction?.Invoke();
        }
        
        public void OnCancel()
        {
            _cancelAction?.Invoke();
        }
    }
}