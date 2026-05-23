using UnityEngine;
using Ygo.View.Component;

namespace Ygo.Controller.Field
{
    public class FieldZoneController : MonoBehaviour
    {
        [field: SerializeField] 
        private HoverView hoverView;

        public void Init()
        {
            hoverView.ToggleEnable(true);
        }
    }
}