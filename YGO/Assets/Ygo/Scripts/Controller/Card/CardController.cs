using System;
using System.Text;
using UnityEditor;
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
        
        private ICardInstance _card;

        private Action<ICardInstance> _onEnter;
        private Action<ICardInstance> _onCLick;
        
        public bool Dirty { get; private set; }
        public bool Enabled { get; private set; }
        
        public void Init(Action<ICardInstance> onEnter = null, Action<ICardInstance> onClick = null)
        {
            _onEnter = onEnter;
            _onCLick = onClick;
            view.ToggleField(cardMode == CardControllerMode.Field);
            hoverController.Init(OnClick, OnEnter, OnExit, cardMode == CardControllerMode.Zoom);
            highlightController.Init();
        }

        public void UpdateCard(ICardInstance cardInstance, bool hidden = false)
        {
            _card = cardInstance;
            Dirty = false;
            view.SetHidden(hidden);
            view.ToggleDefenseMode(false);
            view.Animate();

            if (hidden)
                return;
            
            view.SetName(_card.Data.Name);
            view.SetFrame(GetCardFrameType());
            view.SetIcon(GetCardIconType());
            view.SetIllustration(GetIllustrationFileName());

            if (_card.IsValidMonster)
                InitMonster();
        }

        public void OnDestroy()
        {
            _card = null;
        }

        private void InitMonster()
        {
            view.SetLevel(_card.CurrentLevel.GetValueOrDefault());
            view.SetMonsterText(cardMode == CardControllerMode.Zoom ? _card.CardText : "");
            view.SetMonsterType(GetMonsterType());
            view.SetMonsterAtk(_card.CurrentAtk.GetValueOrDefault().ToString());
            view.SetMonsterDef(_card.CurrentDef.GetValueOrDefault().ToString());
        }

        private CardFrameType GetCardFrameType()
        {
            switch (_card.Data.CardType)
            {
                case CardType.Monster:
                    if (!_card.IsValidMonster)
                        throw new InvalidOperationException($"{nameof(_card.Data.MonsterData)} cannot be null.");
                    if(_card.IsRitual)
                        return _card.IsPendulum ? CardFrameType.RitualPendulum : CardFrameType.Ritual;
                    if(_card.IsFusion)
                        return _card.IsPendulum ? CardFrameType.FusionPendulum : CardFrameType.Fusion;
                    if(_card.IsSynchro)
                        return _card.IsPendulum ? CardFrameType.SynchroPendulum : CardFrameType.Synchro;
                    if(_card.IsXyz)
                        return _card.IsPendulum ? CardFrameType.XyzPendulum : CardFrameType.Xyz;
                    if (_card.IsLink)
                        return CardFrameType.Link;
                    if (_card.IsEffect)
                        return _card.IsPendulum ? CardFrameType.EffectPendulum : CardFrameType.Effect;
                    return _card.IsPendulum ? CardFrameType.NormalPendulum : CardFrameType.Normal;
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
            switch (_card.Data.CardType)
            {
                case CardType.Monster:
                    if (!_card.IsValidMonster)
                        throw new InvalidOperationException($"{nameof(_card.Data.MonsterData)} cannot be null.");
                    switch (_card.Data.MonsterData.Attribute)
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
                        case MonsterAttribute.Unknown:
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
            var id = _card.Data.Id.ToString();
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
            var monsterData = _card.Data.MonsterData;
            if(monsterData == null)
                throw new InvalidOperationException($"There is no monster data for {_card.Data.Id}");
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
            
            _onEnter?.Invoke(_card);
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
            _onCLick?.Invoke(_card);
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
            _card = null;
            view.Clear();
            gameObject.SetActive(false);
            Dirty = false;
        }
    }
}