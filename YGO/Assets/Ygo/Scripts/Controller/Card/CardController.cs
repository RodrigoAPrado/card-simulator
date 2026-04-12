using System;
using System.Text;
using UnityEngine;
using Ygo.Controller.Component;
using Ygo.Core.Abstract;
using Ygo.Core.Board.Abstract;
using Ygo.Data.Enums;
using Ygo.View.Card;
using Ygo.View.ScriptableObjects;

namespace Ygo.Controller.Card
{
    public class CardController : MonoBehaviour
    {
        [field: SerializeField] 
        public ZonePosition ZonePosition { get; private set; }
        
        [field: SerializeField] 
        private CardView view;
        
        [field: SerializeField]
        private CardControllerMode cardMode;
        [field: SerializeField]
        private HoverController hoverController;
        [field: SerializeField]
        private HighlightController highlightController;
        

        private Action<ICardInstance, bool> _onEnter;
        private Action<ICardInstance> _onCLick;
        
        public ICardInstance Card { get; private set; }
        public bool Dirty { get; private set; }
        public bool Enabled { get; private set; }
        private bool Hidden { get; set; }
        
        public void Init(Action<ICardInstance, bool> onEnter = null, Action<ICardInstance> onClick = null)
        {
            _onEnter = onEnter;
            _onCLick = onClick;
            view.ToggleField(cardMode == CardControllerMode.Field);
            hoverController.Init(OnClick, OnEnter, OnExit, cardMode == CardControllerMode.Zoom);
            highlightController.Init();
        }

        public void UpdateCard(ICardInstance cardInstance, bool hidden = false)
        {
            Card = cardInstance;
            Dirty = false;
            Hidden = hidden;
            view.SetHidden(Hidden || (cardInstance?.IsFaceDown == true && cardMode != CardControllerMode.Zoom));
            view.ToggleDefenseMode(cardInstance?.IsInDefense == true);
            view.Animate();

            if (Hidden || (cardInstance?.IsFaceDown == true && cardMode != CardControllerMode.Zoom))
                return;
            
            view.SetName(Card.Data.Name);
            view.SetFrame(GetCardFrameType());
            view.SetIcon(GetCardIconType());
            view.SetIllustration(GetIllustrationFileName());

            if (Card.IsValidMonster)
                InitMonster();
        }

        public void OnDestroy()
        {
            Card = null;
        }

        private void InitMonster()
        {
            view.SetLevel(Card.CurrentLevel.GetValueOrDefault());
            view.SetMonsterText(cardMode == CardControllerMode.Zoom ? Card.CardText : "");
            view.SetMonsterType(GetMonsterType());
            view.SetMonsterAtk(Card.CurrentAtk.GetValueOrDefault().ToString());
            view.SetMonsterDef(Card.CurrentDef.GetValueOrDefault().ToString());
        }

        private CardFrameType GetCardFrameType()
        {
            switch (Card.Data.CardType)
            {
                case CardType.Monster:
                    if (!Card.IsValidMonster)
                        throw new InvalidOperationException($"{nameof(Card.Data.MonsterData)} cannot be null.");
                    if(Card.IsRitual)
                        return Card.IsPendulum ? CardFrameType.RitualPendulum : CardFrameType.Ritual;
                    if(Card.IsFusion)
                        return Card.IsPendulum ? CardFrameType.FusionPendulum : CardFrameType.Fusion;
                    if(Card.IsSynchro)
                        return Card.IsPendulum ? CardFrameType.SynchroPendulum : CardFrameType.Synchro;
                    if(Card.IsXyz)
                        return Card.IsPendulum ? CardFrameType.XyzPendulum : CardFrameType.Xyz;
                    if (Card.IsLink)
                        return CardFrameType.Link;
                    if (Card.IsEffect)
                        return Card.IsPendulum ? CardFrameType.EffectPendulum : CardFrameType.Effect;
                    return Card.IsPendulum ? CardFrameType.NormalPendulum : CardFrameType.Normal;
                case CardType.Spell:
                    return CardFrameType.Spell;
                case CardType.Trap:
                    return CardFrameType.Trap;
                case CardType.Unknown:
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private CardIconType GetCardIconType()
        {
            switch (Card.Data.CardType)
            {
                case CardType.Monster:
                    if (!Card.IsValidMonster)
                        throw new InvalidOperationException($"{nameof(Card.Data.MonsterData)} cannot be null.");
                    switch (Card.Data.MonsterData?.Attribute)
                    {
                        case MonsterAttribute.Dark:
                            return CardIconType.Dark;
                        case MonsterAttribute.Light:
                            return CardIconType.Light;
                        case MonsterAttribute.Earth:
                            return CardIconType.Earth;
                        case MonsterAttribute.Water:
                            return CardIconType.Water;
                        case MonsterAttribute.Fire:
                            return CardIconType.Fire;
                        case MonsterAttribute.Wind:
                            return CardIconType.Wind;
                        case MonsterAttribute.Divine:
                            return CardIconType.Divine;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                case CardType.Spell:
                    return CardIconType.Spell;
                case CardType.Trap:
                    return CardIconType.Trap;
                case CardType.Unknown:
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private string GetIllustrationFileName()
        {
            var id = Card.Data.Id.ToString();
            var sb = new StringBuilder();
            for (var i = 6; i > id.Length; i--)
            {
                sb.Append("0");
            }

            sb.Append(id);
            return sb.ToString();
        }

        private string GetMonsterType()
        {
            var monsterData = Card.Data.MonsterData;
            if(monsterData == null)
                throw new InvalidOperationException($"There is no monster data for {Card.Data.Id}");
            var sb = new StringBuilder();
            sb.Append("[");
            sb.Append(monsterData.Type.ToString());
            if (monsterData.Kinds == null || monsterData.Kinds.Count == 0)
            {
                sb.Append("]");
                return sb.ToString();
            }
            foreach (var kind in monsterData.Kinds)
            {
                if (kind == MonsterKind.Normal)
                    continue;
                sb.Append("/");
                sb.Append(kind.ToString());
            }
            sb.Append("]");
            return sb.ToString();
        }

        public void OnEnter()
        {
            if (!Enabled)
                return;
            if (cardMode == CardControllerMode.Zoom) 
                return;
            
            _onEnter?.Invoke(Card, Hidden);
        }

        public void OnExit()
        {
            if (!Enabled)
                return;
            if (cardMode == CardControllerMode.Zoom) 
                return;
        }

        public void OnClick()
        {
            if (!Enabled)
                return;
            _onCLick?.Invoke(Card);
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
            Card = null;
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