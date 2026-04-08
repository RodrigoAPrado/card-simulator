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
        private GameObject attackButton;
        [field: SerializeField]
        private GameObject cancelButton;

        private Action _normalSummonAction;
        private Action _setAction;
        private Action _tributeSummonAction;
        private Action _tributeSetAction;
        private Action _attackAction;
        private Action _cancelAction;
        
        public void Init(
            Action normalSummonAction, 
            Action setAction, 
            Action tributeSummonAction, 
            Action tributeSetAction, 
            Action cancelAction,
            Action attackAction)
        {
            _normalSummonAction = normalSummonAction;
            _setAction = setAction;
            _tributeSummonAction = tributeSummonAction;
            _tributeSetAction = tributeSetAction;
            _cancelAction = cancelAction;
            _attackAction = attackAction;
        }

        public void HideAll()
        {
            normalSummonButton.SetActive(false);
            setButton.SetActive(false);
            tributeSummonButton.SetActive(false);
            tributeSetButton.SetActive(false);
            attackButton.SetActive(false);
            cancelButton.SetActive(false);
        }

        public void Show(ClickedOnCardResponse response, float xPosition, float yPosition)
        {
            transform.position = new Vector2(xPosition, yPosition);
            normalSummonButton.SetActive(response.NormalSummon);
            setButton.SetActive(response.NormalSet);
            tributeSummonButton.SetActive(response.TributeSummon);
            tributeSetButton.SetActive(response.TributeSet);
            attackButton.SetActive(response.Attack);
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
        
        public void OnAttack()
        {
            _attackAction.Invoke();
        }
        
        public void OnCancel()
        {
            _cancelAction?.Invoke();
        }
    }
}