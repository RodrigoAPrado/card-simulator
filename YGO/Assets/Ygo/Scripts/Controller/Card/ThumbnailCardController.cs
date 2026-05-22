using System;
using System.Text;
using UnityEngine;
using Ygo.Controller.Component;
using Ygo.Scripts.Core.Model;
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

        private Action<CardModel, bool> _onEnter;
        
        public CardModel CardModel { get; private set; }
        public bool Dirty { get; private set; }
        public bool Enabled { get; private set; }
        private bool Hidden { get; set; }
        private Action _onClickAction;
        
        public void Init(Action<CardModel, bool> onEnter)
        {
            _onEnter = onEnter;
            view.ToggleField(cardMode == CardControllerMode.Field);
            hoverController.Init(OnClick, OnEnter, OnExit);
            highlightController.Init();
            Enabled = false;
            gameObject.SetActive(false);
        }

        public void UpdateCard(CardModel cardModel, Sprite cardImage)
        {
            CardModel = cardModel;
            Dirty = false;
            view.SetIllustration(cardImage);
        }

        public void OnDestroy()
        {
            CardModel = null;
        }

        public void OnEnter()
        {
            if (!Enabled)
                return;
            
            _onEnter?.Invoke(CardModel, Hidden);
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
            _onClickAction?.Invoke();
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
            CardModel = null;
            view.Clear();
            gameObject.SetActive(false);
            Dirty = false;
            ClearAction();
        }

        public void SetAction(Action onClickAction)
        {
            _onClickAction = onClickAction;
        }

        public void ClearAction()
        {
            _onClickAction = null;
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