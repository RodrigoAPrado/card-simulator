using TMPro;
using UnityEngine;

namespace Ygo.Scripts.View
{
    public class TextView : MonoBehaviour
    {
        [field: SerializeField] 
        private TextMeshProUGUI Text;

        public void SetText(string text)
        {
            Text.text = text;
        }
    }
}