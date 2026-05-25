using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Ygo.Controller.Card;
using Ygo.Controller.Data;
using Ygo.Scripts.Core.Enum;
using Ygo.Scripts.Core.Model;
using Ygo.View.Component;
using Ygo.View.Field;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Enum;

namespace Ygo.Controller.Field
{
    public class FieldZoneController : MonoBehaviour, IPointerClickHandler
    {
        public FieldZones FieldZone => fieldZone;
        public PointOfView PointOfView => pointOfView;
        public RectTransform Content => content;

        [field: SerializeField] 
        private RectTransform content;
        [field: SerializeField] 
        private FieldZoneView view;
        [field: SerializeField] 
        private HoverView hoverView;
        [field: SerializeField]
        private SelectableView selectableView;
        [field: SerializeField] 
        private ThumbnailFieldCardController fieldCard;
        [field: SerializeField] 
        private FieldZones fieldZone;
        [field: SerializeField]
        private PointOfView pointOfView;

        private CardImageLibrary _library;
        private Action _action;
        
        public void Init(CardImageLibrary library)
        {
            _library = library;
            view.Init();
            hoverView.ToggleEnable(true);
            selectableView.Init();
            fieldCard.Init();
        }

        public void InitCard(CardModel card)
        {
            fieldCard.UpdateCard(card, _library.GetCardImage(card.Data.Code));
            if (card.Position is CardPosition.FaceDownDefense or CardPosition.FaceUpDefense or CardPosition.Defense)
            {
                Flip90();
            }
            else
            {
                FlipStraight();
            }
            
            fieldCard.ShowCard(card.Position is CardPosition.FaceUpAttack or CardPosition.FaceUpDefense or CardPosition.FaceUp);
            fieldCard.ShowStats(card.Position is CardPosition.FaceUpAttack or CardPosition.FaceUpDefense);
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

        private void Flip90()
        {
            view.Flip90();
        }

        private void FlipStraight()
        {
            view.FlipStraight();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(eventData.button == PointerEventData.InputButton.Left)
                _action?.Invoke();
        }
    }
}