using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Ygo.Controller.Card;
using Ygo.Controller.Data;
using Ygo.Scripts.Core.Enum;
using Ygo.Scripts.Core.Model;
using Ygo.View.Component;

namespace Ygo.Controller.Field
{
    public class GraveyardController : MonoBehaviour, IPointerClickHandler
    {
        public PointOfView PointOfView => pointOfView;
        public RectTransform Content => content;
        
        [field: SerializeField] 
        private RectTransform content;
        [field: SerializeField] 
        private HoverView hoverView;
        [field: SerializeField]
        private SelectableView selectableView;
        [field: SerializeField]
        private ThumbnailSimpleCardController cardController;
        [field: SerializeField] 
        private PointOfView pointOfView;
        
        private CardImageLibrary _library;
        private Action _action;

        public void Init(CardImageLibrary library)
        {
            _library = library;
            hoverView.ToggleEnable(true);
            selectableView.Init();
            cardController.Init();
        }
        
        public void InitCard(CardModel card)
        {
            cardController.UpdateCard(card, _library.GetCardImage(card.Data.Code));
            cardController.ShowCard();
        }

        public void SetAction(Action action)
        {
            _action = action;
        }

        public void ClearAction()
        {
            _action = null;
        }

        public void ToggleHighlight(bool toggle)
        {
            if(toggle)
                selectableView.Animate();
            else
                selectableView.StopAnimating();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(eventData.button == PointerEventData.InputButton.Left)
                _action?.Invoke();
        }

    }
}