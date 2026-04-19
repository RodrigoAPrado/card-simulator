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

        [Header("Field")] 
        [field: SerializeField]
        private GameObject fieldBox;
        [field: SerializeField]
        private TextMeshProUGUI fieldLevelText;
        [field: SerializeField]
        private Transform cardContent;
        [field: SerializeField]
        private Transform fieldAtkParent;
        [field: SerializeField]
        private Transform fieldDefParent;
        [field: SerializeField]
        private TextMeshProUGUI fieldAtkText;
        [field: SerializeField]
        private TextMeshProUGUI fieldDefText;

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
            fieldBox.SetActive(_fieldEnabled && !_isHidden);
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

        public void SetLevel(int value)
        {
            for (var i = 0; i < levelsContainer.Length; i++)
            {
                levelsContainer[i].SetActive(value > i);
            }

            if (_fieldEnabled)
            {
                fieldLevelText.text = value.ToString();
            }
        }

        public void SetIllustration(string value)
        {
            var path = "Card/Illustrations/" + value;
            var sprite = Resources.Load<Sprite>(path);
            if(sprite != null)
                illustration.sprite = sprite;
            else
            {
                illustration.sprite = emptyIllustrationSprite;
            }
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
            
            if (_fieldEnabled)
            {
                fieldAtkText.text = value;
            }
        }

        public void SetMonsterDef(string value)
        {
            monsterDef.gameObject.SetActive(true);
            monsterDef.text = value;
            
            if (_fieldEnabled)
            {
                fieldDefText.text = value;
            }
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

        public void ToggleField(bool value)
        {
            _fieldEnabled = value;
            fieldBox.SetActive(_fieldEnabled && !_isHidden);
        }

        public void ToggleDefenseMode(bool value)
        {
            if (!_fieldEnabled)
                return;
            cardContent.localRotation = Quaternion.Euler(0f, 0f, value ? 90f : 0f);
            fieldAtkParent.localScale = new Vector3(value ? 0.7f : 1.1f, value ? 0.7f : 1.1f, 1f);
            fieldDefParent.localScale = new Vector3(value ? 1.1f : 0.7f, value ? 1.1f : 0.7f, 1f);
        }
        
        public void Clear()
        {
            cardContent.DOKill();
        }

        public void Animate()
        {
            if(_fieldEnabled)
                AnimateFloating();
        }
        
        private void AnimateFloating()
        {
            // 1. Garante que qualquer animação anterior no cardContent seja parada para não encavalar
            cardContent.DOKill();

            // 2. Faz o movimento no eixo Z local
            // Supondo que 0.1f seja a altura da flutuação
            cardContent.DOLocalMoveZ(-0.08f, 1.2f) 
                .SetEase(Ease.InOutSine) // Movimento suave nas extremidades
                .SetLoops(-1, LoopType.Yoyo); // -1 = Infinito, Yoyo = vai e volta
        }
    }
}