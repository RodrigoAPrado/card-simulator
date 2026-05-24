using System;
using UnityEngine;
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
    public class FieldZoneController : MonoBehaviour
    {
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
            if (card.Position == CardPosition.Defense)
            {
                Flip90();
            }
            else
            {
                FlipStraight();
            }
            
            fieldCard.ShowCard(card.Position == CardPosition.FaceUp);
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
    }
}