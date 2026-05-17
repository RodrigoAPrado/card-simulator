using System;
using System.Text;
using UnityEngine;
using Ygo.Controller.Component;
using Ygo.View.Card;
using Ygo.View.ScriptableObjects;
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

        private Action<uint, bool> _onEnter;
        
        public uint CardCode { get; private set; }
        public bool Dirty { get; private set; }
        public bool Enabled { get; private set; }
        private bool Hidden { get; set; }
        
        public void Init(Action<uint, bool> onEnter = null)
        {
            _onEnter = onEnter;
            view.ToggleField(cardMode == CardControllerMode.Field);
            hoverController.Init(OnClick, OnEnter, OnExit);
            highlightController.Init();
        }

        public void UpdateCard(uint cardCode)
        {
            CardCode = cardCode;
            Dirty = false;
            view.SetIllustration(GetIllustrationFileName());
        }

        public void OnDestroy()
        {
            CardCode = 0;
        }

        private void InitMonster()
        {
        }

        private void InitSpell()
        {
        }

        private string GetIllustrationFileName()
        {
            return "";
            //UnityEngine.Application.streamingAssetsPath + "/" + 
        }

        public void OnEnter()
        {
            if (!Enabled)
                return;
            
            _onEnter?.Invoke(CardCode, Hidden);
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
            CardCode = 0;
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