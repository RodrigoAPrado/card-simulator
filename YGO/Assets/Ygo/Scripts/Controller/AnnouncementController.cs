using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Ygo.Scripts.Core.Event;
using Ygo.Scripts.Core.Event.Base;
using Ygo.View;

namespace Ygo.Controller
{
    public class AnnouncementController : MonoBehaviour
    {
        [field:SerializeField]
        private TextViewUI whiteText;
        [field:SerializeField]
        private TextViewUI blackText;
        [field: SerializeField] 
        private AnnouncementView view;

        public void Init(EventQueue eventQueue)
        {
            whiteText.gameObject.SetActive(false);
            blackText.gameObject.SetActive(false);
            eventQueue.Subscribe<NewTurnEvent>(OnNewTurn);
            eventQueue.Subscribe<NewPhaseEvent>(OnNewTurn);
        }
        
        private async UniTask OnNewTurn(NewTurnEvent e)
        {
            //TODO: Fazer animação
            await Announce("New Turn!");
        }
        
        private async UniTask OnNewTurn(NewPhaseEvent e)
        {
            //TODO: Fazer animação
            await Announce(e.Phase.ToString());
        }

        private async UniTask Announce(string announcement)
        {
            whiteText.gameObject.SetActive(true);
            blackText.gameObject.SetActive(true);
            whiteText.SetText(announcement);
            blackText.SetText(announcement);

            await view.Animate();
            await UniTask.DelayFrame(20);
            whiteText.gameObject.SetActive(false);
            blackText.gameObject.SetActive(false);
        }
    }
}