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

        [Header("CardBox")] 
        [field: SerializeField]
        private TextMeshProUGUI monsterType;
        
        [field: SerializeField]
        private TextMeshProUGUI monsterText;
        
        [field: SerializeField]
        private TextMeshProUGUI monsterAtk;
        
        [field: SerializeField]
        private TextMeshProUGUI monsterDef;
        
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
            var sprite = Resources.Load<Sprite>("Card/Illustrations/" + value);
            illustration.sprite = sprite;
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
    }
}