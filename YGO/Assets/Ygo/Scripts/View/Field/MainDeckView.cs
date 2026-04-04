using TMPro;
using UnityEngine;

namespace Ygo.Scripts.View.Field
{
    public class MainDeckView : MonoBehaviour
    {
        [field: SerializeField] 
        private TextMeshProUGUI label;
        
        public void SetDeckSize(string deckSize)
        {
            label.text = deckSize;    
        }
    }
}