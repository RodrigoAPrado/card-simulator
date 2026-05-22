using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Ygo.Controller.Card;
using Ygo.Controller.Data;
using Ygo.Core.Duel;
using Ygo.Scripts.Core.Enum;
using Ygo.Scripts.Core.Event;
using Ygo.Scripts.Core.Event.Base;
using Ygo.Scripts.Core.Model;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;

namespace Ygo.Controller
{
    public class HandController : MonoBehaviour
    {
        [field:SerializeField]
        private ThumbnailCardController[] cardControllers;
        [field: SerializeField] 
        private PointOfView pointOfView;
        private CardImageLibrary _library;

        public void Init(
            EventQueue eventQueue,
            CardImageLibrary library,
            Action<CardModel, bool> onHover)
        {
            _library = library;
            foreach (var controller in cardControllers)
            {
                controller.Init(onHover);
            }
            eventQueue.Subscribe<DrawEvent>(OnDrawEvent);
        }

        private async UniTask OnDrawEvent(DrawEvent e)
        {
            if (e.PointOfView != pointOfView)
                return;

            SetState(e.HandBefore);
            //TODO: Fazer animação
            await UniTask.DelayFrame(6);
            SetState(e.HandAfter);
        }

        private void SetState(IReadOnlyList<CardModel> cards)
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
                cardControllers[i].UpdateCard(cards[i], _library.GetCardImage(cards[i].Data.Code));
                cardControllers[i].Enable();
            }

            foreach (var controller in cardControllers)
            {
                if(controller.Dirty)
                    controller.Disable();
            }
        }
    }
}