using TMPro;
using UnityEngine;

namespace Ygo.View
{
    public class TextViewUI : MonoBehaviour
    {
        [field: SerializeField] 
        protected TextMeshProUGUI text;

        public virtual void SetText(string value)
        {
            text.text = value;
        }
    }
}