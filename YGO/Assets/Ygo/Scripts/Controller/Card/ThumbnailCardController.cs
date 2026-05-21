using System;
using System.Text;
using UnityEngine;
using Ygo.Controller.Component;
using Ygo.View.Card;
using Ygo.View.ScriptableObjects;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card.Enum;

namespace Ygo.Controller.Card
{
    public class ThumbnailCardController : MonoBehaviour
    {
        [field: SerializeField] 
        private ThumbCardView view;
        
        [field: SerializeField]
        private CardControllerMode cardMode;
        [field: SerializeField]
        private HoverController hoverController;
        [field: SerializeField]
        private HighlightController highlightController;

        private Action<ICardData, bool> _onEnter;
        
        public ICardData CardData { get; private set; }
        public bool Dirty { get; private set; }
        public bool Enabled { get; private set; }
        private bool Hidden { get; set; }
        
        public void Init(Action<ICardData, bool> onEnter)
        {
            _onEnter = onEnter;
            view.ToggleField(cardMode == CardControllerMode.Field);
            hoverController.Init(OnClick, OnEnter, OnExit);
            highlightController.Init();
            Enabled = false;
            gameObject.SetActive(false);
        }

        public void UpdateCard(ICardData cardData, Sprite cardImage)
        {
            CardData = cardData;
            Dirty = false;
            view.SetIllustration(cardImage);
        }

        public void OnDestroy()
        {
            CardData = null;
        }

        public void OnEnter()
        {
            if (!Enabled)
                return;
            
            _onEnter?.Invoke(CardData, Hidden);
        }

        public void OnExit()
        {
            if (!Enabled)
                return;
        }

        public void OnClick()
        {
            if (!Enabled)
                return;
        }

        public void SetDirty()
        {
            Dirty = true;
        }

        public void Enable()
        {
            Enabled = true;
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            Enabled = false;
            CardData = null;
            view.Clear();
            gameObject.SetActive(false);
            Dirty = false;
        }
        
        public void ToggleHighlight(bool value)
        {
            if(value)
                highlightController.Enable();
            else
                highlightController.Disable();
        }
    }
}