using UnityEngine;
using Ygo.View.Component;

namespace Ygo.Controller.Component
{
    public class HighlightController : MonoBehaviour
    {
        [field: SerializeField] 
        private HighlightView view;

        public void Init()
        {
            view.Init();
        }
        
        public void Enable()
        {
            view.ToggleHighlight(true);    
        }

        public void Disable()
        {
            view.ToggleHighlight(false);
        }
    }
}