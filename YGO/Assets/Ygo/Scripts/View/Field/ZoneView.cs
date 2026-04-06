using UnityEngine;
using UnityEngine.Serialization;

namespace Ygo.View.Field
{
    public class ZoneView : MonoBehaviour
    {
        [field: SerializeField] 
        private GameObject highlight;

        [field: SerializeField]
        private GameObject hover;
        
        public void ToggleHighlight(bool value)
        {
            highlight.SetActive(value);
        }
        
        public void ToggleHover(bool value)
        {
            hover.SetActive(value);
        }
    }
}