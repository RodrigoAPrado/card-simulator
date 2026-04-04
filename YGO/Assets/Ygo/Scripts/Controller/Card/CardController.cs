using System;
using System.Text;
using UnityEditor;
using UnityEngine;
using Ygo.Core.Abstract;
using Ygo.Data.Enums;
using Ygo.View.Card;
using Ygo.View.ScriptableObjects;

namespace Ygo.Controller.Card
{
    public class CardController : MonoBehaviour
    {
        [field: SerializeField] 
        private CardView view;

        private ICardInstance _cardInstance;
        private bool _zoomMode;

        private Action<ICardInstance> _action;
        
        public void Init(ICardInstance cardInstance, Action<ICardInstance> action = null)
        {
            _cardInstance = cardInstance;
            _action = action;
            
            view.SetName(_cardInstance.Data.Name);
            view.SetFrame(GetCardFrameType());
            view.SetIcon(GetCardIconType());
            view.SetIllustration(GetIllustrationFileName());

            if (_cardInstance.IsValidMonster)
                InitMonster();
        }

        public void SetHandMode()
        {
            view.SetMonsterText("");
        }

        public void SetZoomMode()
        {
            _zoomMode = true;
        }

        public void OnDestroy()
        {
            _cardInstance = null;
        }

        private void InitMonster()
        {
            view.SetLevel(_cardInstance.CurrentLevel.GetValueOrDefault());
            view.SetMonsterType(GetMonsterType());
            view.SetMonsterText(_cardInstance.CardText);
            view.SetMonsterAtk(_cardInstance.CurrentAtk.GetValueOrDefault().ToString());
            view.SetMonsterDef(_cardInstance.CurrentDef.GetValueOrDefault().ToString());
        }

        private CardFrameType GetCardFrameType()
        {
            switch (_cardInstance.Data.CardType)
            {
                case CardType.Monster:
                    if (!_cardInstance.IsValidMonster)
                        throw new InvalidOperationException($"{nameof(_cardInstance.Data.MonsterData)} cannot be null.");
                    if(_cardInstance.IsRitual)
                        return _cardInstance.IsPendulum ? CardFrameType.RitualPendulum : CardFrameType.Ritual;
                    if(_cardInstance.IsFusion)
                        return _cardInstance.IsPendulum ? CardFrameType.FusionPendulum : CardFrameType.Fusion;
                    if(_cardInstance.IsSynchro)
                        return _cardInstance.IsPendulum ? CardFrameType.SynchroPendulum : CardFrameType.Synchro;
                    if(_cardInstance.IsXyz)
                        return _cardInstance.IsPendulum ? CardFrameType.XyzPendulum : CardFrameType.Xyz;
                    if (_cardInstance.IsLink)
                        return CardFrameType.Link;
                    if (_cardInstance.IsEffect)
                        return _cardInstance.IsPendulum ? CardFrameType.EffectPendulum : CardFrameType.Effect;
                    return _cardInstance.IsPendulum ? CardFrameType.NormalPendulum : CardFrameType.Normal;
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
            switch (_cardInstance.Data.CardType)
            {
                case CardType.Monster:
                    if (!_cardInstance.IsValidMonster)
                        throw new InvalidOperationException($"{nameof(_cardInstance.Data.MonsterData)} cannot be null.");
                    switch (_cardInstance.Data.MonsterData.Attribute)
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
            var id = _cardInstance.Data.Id.ToString();
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
            var monsterData = _cardInstance.Data.MonsterData;
            if(monsterData == null)
                throw new InvalidOperationException($"There is no monster data for {_cardInstance.Data.Id}");
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

        public void Hover()
        {
            if (_zoomMode)
                return;
            view.ToggleHighlight(true);
            _action.Invoke(_cardInstance);
        }

        public void Exit()
        {
            if (_zoomMode)
                return;
            view.ToggleHighlight(false);
        }
    }
}