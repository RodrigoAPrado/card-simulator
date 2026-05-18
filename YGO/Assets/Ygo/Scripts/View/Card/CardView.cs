using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Ygo.View.ScriptableObjects;

namespace Ygo.View.Card
{
    public class CardView : MonoBehaviour
    {
        [Header("Frame")] 
        [field: SerializeField]
        private Image frameImage;
        [field: SerializeField] 
        private CardFrameDatabase frameDatabase;

        [Header("Name")] 
        [field: SerializeField]
        private TextMeshProUGUI cardName;
        
        [Header("Icon")]
        [field: SerializeField] 
        private Image iconImage;
        [field: SerializeField] 
        private CardIconDatabase iconDatabase;
        
        [Header("Levels")]
        [field: SerializeField]
        private GameObject[] levelsContainer;

        [Header("Illustration")] 
        [field: SerializeField]
        private Image illustration;
        [field: SerializeField] 
        private Sprite emptyIllustrationSprite;

        [Header("MonsterBox")]
        [field: SerializeField]
        private GameObject monsterBox;
        [field: SerializeField]
        private TextMeshProUGUI monsterType;
        [field: SerializeField]
        private TextMeshProUGUI monsterText;
        [field: SerializeField]
        private TextMeshProUGUI monsterAtk;
        [field: SerializeField]
        private TextMeshProUGUI monsterDef;
        
        [Header("SpellTrapBox")]
        [field: SerializeField]
        private GameObject spellTrapTypeBox;
        [field: SerializeField]
        private SpellTrapTypeView spellTrapType;
        [field: SerializeField]
        private GameObject spellTrapBox;
        [field: SerializeField]
        private TextMeshProUGUI spellTrapText;

        [Header("CardBack")]
        [field: SerializeField]
        private GameObject cardBack;
        [field: SerializeField]
        private GameObject cardFront;
        
        private bool _fieldEnabled;
        private bool _isHidden;

        public void SetHidden(bool value)
        {
            _isHidden = value;
            cardBack.SetActive(_isHidden);
            cardFront.SetActive(!_isHidden);
        }
        
        public void SetFrame(CardFrameType type)
        {
            if(frameDatabase != null)
            {
                frameImage.sprite = frameDatabase.GetFrame(type);
                frameImage.enabled = true;
            }
            else
            {
                frameImage.enabled = false;
            }
        }
        
        public void SetName(string value)
        {
            cardName.text = value;
        }
        
        public void SetIcon(CardIconType type)
        {
            if(iconDatabase != null)
            {
                iconImage.sprite = iconDatabase.GetIcon(type);
                iconImage.enabled = true;
            }
            else
            {
                iconImage.enabled = false;
            }
        }

        public void SetLevel(uint value)
        {
            for (var i = 0; i < levelsContainer.Length; i++)
            {
                levelsContainer[i].SetActive(value > i);
            }
        }

        public void SetIllustration(Sprite value)
        {
            illustration.sprite = value;
        }

        public void SetMonsterType(string value)
        {
            monsterType.gameObject.SetActive(true);
            monsterType.text = value;
        }

        public void SetMonsterText(string value)
        {
            monsterText.gameObject.SetActive(true);
            monsterText.text = value;
        }

        public void SetMonsterAtk(string value)
        {
            monsterAtk.gameObject.SetActive(true);
            monsterAtk.text = value;
        }

        public void SetMonsterDef(string value)
        {
            monsterDef.gameObject.SetActive(true);
            monsterDef.text = value;
        }

        public void SetSpellTrapText(string value)
        {
            spellTrapText.gameObject.SetActive(true);
            spellTrapText.text = value;
        }
        
        public void SetSpellTrapSubType(bool isTrap, SpellTrapIconType iconType)
        {
            spellTrapType.SetValues(isTrap, iconType != SpellTrapIconType.NoIcon);
            if(iconType != SpellTrapIconType.NoIcon)
                spellTrapType.SetIcon(iconType);
        }

        public void ToggleMonsterBox(bool value)
        {
            monsterBox.SetActive(value);
        }
        
        public void ToggleSpellTrapBox(bool value)
        {
            spellTrapBox.SetActive(value);
        }
        
        public void ToggleSpellTrapTypeBox(bool value)
        {
            spellTrapTypeBox.SetActive(value);
        }

        public void SetTitleColor(CardFrameType frameType)
        {
            switch (frameType)
            {
                case CardFrameType.Normal:
                case CardFrameType.NormalPendulum:
                case CardFrameType.Effect:
                case CardFrameType.EffectPendulum:
                case CardFrameType.Ritual:
                case CardFrameType.RitualPendulum:
                case CardFrameType.Fusion:
                case CardFrameType.FusionPendulum:
                case CardFrameType.Synchro:
                case CardFrameType.SynchroPendulum:
                case CardFrameType.Token:
                    cardName.color = Color.black;
                    break;
                case CardFrameType.Xyz:
                case CardFrameType.XyzPendulum:
                case CardFrameType.Link:
                case CardFrameType.Spell:
                case CardFrameType.Trap:
                    cardName.color = Color.white;
                    break;
            }
        }
    }
}