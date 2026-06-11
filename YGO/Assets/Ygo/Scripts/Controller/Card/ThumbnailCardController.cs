using System;
using System.Collections.Generic;
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
    public class ThumbnailCardController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
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
        private Action<IReadOnlyDictionary<string, Action>, Transform> _showActionMenu;
        private Dictionary<string, Action> _availableCommands;
        
        public void Init(Action<CardModel, bool> onEnter, Action<IReadOnlyDictionary<string, Action>, Transform> showActionMenu)
        {
            _onEnter = onEnter;
            _showActionMenu = showActionMenu;
            Enabled = false;
            _availableCommands = new Dictionary<string, Action>();
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
            ClearAvailableCommands();
        }

        public void SetAction(Action onClickAction)
        {
            _onClickAction = onClickAction;
        }

        public void ClearAction()
        {
            _onClickAction = null;
        }

        public void ClearAvailableCommands()
        {
            _availableCommands.Clear();
        }

        public void AddCommand(string commandName, Action onClickAction)
        {
            if(!_availableCommands.TryAdd(commandName, onClickAction))
                throw new InvalidOperationException("There is already an action with the name: " + commandName);
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

        public void OnPointerEnter(PointerEventData eventData)
        {
            _showActionMenu?.Invoke(_availableCommands, gameObject.transform);
        }
    }
}