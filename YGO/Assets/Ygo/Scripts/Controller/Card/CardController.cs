using System;
using System.Linq;
using System.Text;
using UnityEngine;
using Ygo.Controller.Component;
using Ygo.Controller.Data;
using Ygo.View.Card;
using Ygo.View.ScriptableObjects;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card.Enum;

namespace Ygo.Controller.Card
{
    public class CardController : MonoBehaviour
    {
        [field: SerializeField] 
        private CardView view;

        private ICardData _cardData;
        public bool Dirty { get; private set; }
        private bool Hidden { get; set; }
        private CardImageLibrary _cardImageLibrary;
        
        public void Init(CardImageLibrary cardImageLibrary)
        {
            _cardImageLibrary = cardImageLibrary;
        }

        public void UpdateCard(ICardData card)
        {
            _cardData = card;
            Dirty = false;
            
            view.SetName(_cardData.Name);
            view.SetFrame(GetCardFrameType());
            view.SetIcon(GetCardIconType());
            view.SetIllustration(_cardImageLibrary.GetCardImage(card.Code));

            if (!_cardData.Types.Contains(CardType.Spell) && !_cardData.Types.Contains(CardType.Trap))
                InitMonster();
            else
                InitSpellTrap();
        }

        public void OnDestroy()
        {
            _cardData = null;
        }

        private void InitMonster()
        {
            view.SetLevel(_cardData.Level);
            view.ToggleMonsterBox(true);
            view.ToggleSpellTrapBox(false);
            view.ToggleSpellTrapTypeBox(false);
            view.SetMonsterText(_cardData.Description);
            view.SetMonsterType(GetMonsterType());
            view.SetMonsterAtk(_cardData.OriginalAttack.ToString());
            view.SetMonsterDef(_cardData.OriginalDefense.ToString());
        }

        private void InitSpellTrap()
        {
            view.SetLevel(0);
            view.ToggleMonsterBox(false);
            view.ToggleSpellTrapBox(true);
            view.ToggleSpellTrapTypeBox(true);
            view.SetSpellTrapText(_cardData.Description);
            view.SetSpellTrapSubType(false, GetSpellTrapIconType());
        }

        private CardFrameType GetCardFrameType()
        {
            return (CardFrameType) _cardData.Frame;
        }

        private CardIconType GetCardIconType()
        {
            return (CardIconType) _cardData.CardAttribute;
        }

        private SpellTrapIconType GetSpellTrapIconType()
        {
            if (_cardData.Types.Contains(CardType.Continuous))
                return SpellTrapIconType.Continuous;
            if (_cardData.Types.Contains(CardType.Counter))
                return SpellTrapIconType.CounterTrap;
            if (_cardData.Types.Contains(CardType.Equip))
                return SpellTrapIconType.EquipSpell;
            if (_cardData.Types.Contains(CardType.Field))
                return SpellTrapIconType.FieldSpell;
            if (_cardData.Types.Contains(CardType.QuickPlay))
                return SpellTrapIconType.QuickSpell;
            if (_cardData.Types.Contains(CardType.Ritual))
                return SpellTrapIconType.RitualSpell;
            return SpellTrapIconType.NoIcon;
        }

        private string GetMonsterType()
        {
            var sb = new StringBuilder();
            sb.Append($" {_cardData.Type} ");
            foreach (var cardType in _cardData.Types)
            {
                switch (cardType)
                {
                    case CardType.Effect:
                    case CardType.Fusion:
                    case CardType.Ritual:
                    case CardType.Spirit:
                    case CardType.Union:
                    case CardType.Gemini:
                    case CardType.Tuner:
                    case CardType.Synchro:
                    case CardType.Token:
                    case CardType.Toon:
                    case CardType.Xyz:
                    case CardType.Pendulum:
                    case CardType.Link:
                        continue;
                }

                sb.Append($"/ {cardType} ");
            }
            return sb.ToString();
        }

        public void SetDirty()
        {
            Dirty = true;
        }
    }
}