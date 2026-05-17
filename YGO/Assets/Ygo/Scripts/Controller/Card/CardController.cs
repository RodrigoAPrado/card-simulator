using System;
using System.Linq;
using System.Text;
using UnityEngine;
using Ygo.Controller.Component;
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
        
        public ICardData CardData { get; private set; }
        public bool Dirty { get; private set; }
        private bool Hidden { get; set; }
        
        public void Init(Action<uint, bool> onEnter = null)
        {
        }

        public void UpdateCard(ICardData cardData)
        {
            CardData = cardData;
            Dirty = false;
            view.Animate();
            
            view.SetName(CardData.Name);
            view.SetFrame(GetCardFrameType());
            view.SetIcon(GetCardIconType());
            view.SetIllustration(GetIllustrationFileName());

            if (!CardData.Types.Contains(CardType.Spell) && !CardData.Types.Contains(CardType.Trap))
                InitMonster();
            else
                InitSpellTrap();
        }

        public void OnDestroy()
        {
            CardData = null;
        }

        private void InitMonster()
        {
            view.SetLevel(CardData.Level);
            view.ToggleMonsterBox(true);
            view.ToggleSpellTrapBox(false);
            view.ToggleSpellTrapTypeBox(false);
            view.SetMonsterText(CardData.Description);
            view.SetMonsterType(GetMonsterType());
            view.SetMonsterAtk(CardData.OriginalAttack.ToString());
            view.SetMonsterDef(CardData.OriginalDefense.ToString());
        }

        private void InitSpellTrap()
        {
            view.SetLevel(0);
            view.ToggleMonsterBox(false);
            view.ToggleSpellTrapBox(true);
            view.ToggleSpellTrapTypeBox(true);
            view.SetSpellTrapText(CardData.Description);
            view.SetSpellTrapSubType(false, GetSpellTrapIconType());
        }

        private CardFrameType GetCardFrameType()
        {
            return (CardFrameType) CardData.Frame;
        }

        private CardIconType GetCardIconType()
        {
            return (CardIconType) CardData.CardAttribute;
        }

        private SpellTrapIconType GetSpellTrapIconType()
        {
            if (CardData.Types.Contains(CardType.Continuous))
                return SpellTrapIconType.Continuous;
            if (CardData.Types.Contains(CardType.Counter))
                return SpellTrapIconType.CounterTrap;
            if (CardData.Types.Contains(CardType.Equip))
                return SpellTrapIconType.EquipSpell;
            if (CardData.Types.Contains(CardType.Field))
                return SpellTrapIconType.FieldSpell;
            if (CardData.Types.Contains(CardType.QuickPlay))
                return SpellTrapIconType.QuickSpell;
            if (CardData.Types.Contains(CardType.Ritual))
                return SpellTrapIconType.RitualSpell;
            return SpellTrapIconType.NoIcon;
        }

        private string GetIllustrationFileName()
        {
            return "";
        }

        private string GetMonsterType()
        {
            var sb = new StringBuilder();
            sb.Append($" {CardData.Type} ");
            foreach (var cardType in CardData.Types)
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