using System;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using Ygo.Controller.Component;
using Ygo.Scripts.Core.Model;
using Ygo.View.Card;
using Ygo.View.Component;
using Ygo.View.ScriptableObjects;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card.Enum;

namespace Ygo.Controller.Card
{
    public class ThumbnailCardController : MonoBehaviour, IPointerClickHandler
    {
        [field: SerializeField] 
        private ThumbCardView view;
        [field: SerializeField] 
        private HoverView hoverView;
        [field: SerializeField] 
        private SelectableView selectableView;

        private Action<CardModel, bool> _onEnter;
        
        public CardModel CardModel { get; private set; }
        public bool Dirty { get; private set; }
        public bool Enabled { get; private set; }
        private bool Hidden { get; set; }
        private Action _onClickAction;
        
        public void Init(Action<CardModel, bool> onEnter)
        {
            _onEnter = onEnter;
            Enabled = false;
            gameObject.SetActive(false);
            hoverView.ToggleEnable(true);
            selectableView.Init();
        }

        public void Highlight()
        {
            selectableView.Animate();
        }

        public void StopHighlight()
        {
            selectableView.StopAnimating();
        }

        public void UpdateCard(CardModel cardModel, Sprite cardImage)
        {
            CardModel = cardModel;
            Dirty = false;
            view.SetIllustration(cardImage);
            hoverView.ToggleEnable(true);
        }

        public void OnDestroy()
        {
            CardModel = null;
        }

        public void SetDirty()
        {
            Dirty = true;
        }

        public void Enable()
        {
            Enabled = true;
            gameObject.SetActive(true);
            hoverView.ToggleEnable(true);
        }

        public void Disable()
        {
            Enabled = false;
            CardModel = null;
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

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!Enabled)
                return;
            if(eventData.button == PointerEventData.InputButton.Left)
                _onClickAction?.Invoke();
        }

        public void HideView()
        {
            view.HideAll();
        }
    }
}