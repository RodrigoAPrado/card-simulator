using TMPro;
using UnityEngine;

namespace Ygo.View.Card
{
    public class StatsTextViewUI : TextViewUI
    {
        [field: SerializeField] 
        private TextMeshProUGUI textShadow;
        
        public override void SetText(string value)
        {
            text.text = value;
            textShadow.text = value;
        }
    }
}