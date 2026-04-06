using TMPro;
using UnityEngine;
using Ygo.View.ScriptableObjects;

namespace Ygo.View.Field
{
    public class FieldCardView
    {
        [Header("Frame")] 
        [field: SerializeField]
        private SpriteRenderer frameImage;

        [field: SerializeField] 
        private GameObject highlight;
        
        [field: SerializeField] 
        private CardFrameDatabase frameDatabase;

        [Header("Name")] 
        [field: SerializeField]
        private TextMeshPro cardName;
        
        [Header("Icon")]
        [field: SerializeField] 
        private SpriteRenderer iconImage;

        [field: SerializeField] 
        private CardIconDatabase iconDatabase;
        
        [Header("Levels")]
        [field: SerializeField]
        private GameObject[] levelsContainer;

        [Header("Illustration")] 
        [field: SerializeField]
        private SpriteRenderer illustration;

        [field: SerializeField] 
        private Sprite emptyIllustrationSprite;

        [Header("CardBox")] 
        [field: SerializeField]
        private TextMeshPro monsterType;
        
        [field: SerializeField]
        private TextMeshPro monsterText;
        
        [field: SerializeField]
        private TextMeshPro monsterAtk;
        
        [field: SerializeField]
        private TextMeshPro monsterDef;
        
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
        }

        public void SetIllustration(string value)
        {
            var path = "Card/Illustrations/" + value;
            var sprite = Resources.Load<Sprite>(path);
            if(sprite != null)
                illustration.sprite = sprite;
            else
            {
                //Debug.LogWarning(path + " is not a valid illustration");
                illustration.sprite = emptyIllustrationSprite;
            }
        }

        public void SetMonsterType(string value)
        {
            monsterType.text = value;
        }

        public void SetMonsterText(string value)
        {
            monsterText.text = value;
        }

        public void SetMonsterAtk(string value)
        {
            monsterAtk.text = value;
        }

        public void SetMonsterDef(string value)
        {
            monsterDef.text = value;
        }

        public void ToggleHighlight(bool value)
        {
            highlight.SetActive(value);
        }

        public void Clear()
        {
            //TODO: Rotina de Clear.
        }
    }
}