using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Ygo.View.ScriptableObjects;

namespace Ygo.View.Card
{
    public class SpellTrapTypeView : MonoBehaviour
    {
        [field: SerializeField] 
        private SpellTrapIconDatabase iconDatabase;
        [field: SerializeField] 
        private Image iconImage;
        [field: SerializeField]
        private TextMeshProUGUI text;

        private const string SpellNoIcon = "[Spell Card]";
        private const string SpellWithIcon = "[Spell Card    ]";
        private const string TrapNoIcon = "[Trap Card]";
        private const string TrapWithIcon = "[Trap Card    ]";

        public void SetValues(bool isTrap, bool withIcon)
        {
            text.text = isTrap ? 
                withIcon ? 
                    TrapWithIcon : 
                    TrapNoIcon 
                : withIcon ? 
                    SpellWithIcon : 
                    SpellNoIcon;
            
            iconImage.enabled = withIcon;
        }
        
        public void SetIcon(SpellTrapIconType type)
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
    }
}