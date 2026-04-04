using System;
using UnityEngine;
using Ygo.Core.Response;

namespace Ygo.Scripts.Controller.Hand
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

        private Action _normalSummonAction;
        private Action _setAction;
        private Action _tributeSummonAction;
        private Action _tributeSetAction;
        
        public void Init(Action normalSummonAction, Action setAction, Action tributeSummonAction, Action tributeSetAction)
        {
            _normalSummonAction = normalSummonAction;
            _setAction = setAction;
            _tributeSummonAction = tributeSummonAction;
            _tributeSetAction = tributeSetAction;
        }

        public void HideAll()
        {
            normalSummonButton.SetActive(false);
            setButton.SetActive(false);
            tributeSummonButton.SetActive(false);
            tributeSetButton.SetActive(false);
        }

        public void Show(ClickedOnCardHandResponse response, float xPosition)
        {
            transform.position = new Vector2(xPosition, transform.position.y);
            normalSummonButton.SetActive(response.NormalSummon);
            setButton.SetActive(response.NormalSet);
            tributeSummonButton.SetActive(response.TributeSummon);
            tributeSetButton.SetActive(response.TributeSet);
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
    }
}