using TMPro;
using UnityEngine;

namespace Ygo.View
{
    public class TextViewUI : MonoBehaviour
    {
        [field: SerializeField] 
        private TextMeshProUGUI Text;

        public void SetText(string text)
        {
            Text.text = text;
        }
    }
}