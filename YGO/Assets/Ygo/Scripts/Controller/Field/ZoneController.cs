using UnityEngine;
using Ygo.Controller.Component;
using Ygo.View.Field;

namespace Ygo.Controller.Field
{
    public class ZoneController : MonoBehaviour
    {
        [field: SerializeField] 
        private HoverController hoverController;
        [field: SerializeField] 
        private HighlightController highlightController;
        [field: SerializeField] 
        private ZoneView view;
        
        public void Init()
        {
        }

        public void ToggleHighlight(bool value)
        {
            if(value)
                highlightController.Enable();
            else
                highlightController.Disable();
        }
    }
}