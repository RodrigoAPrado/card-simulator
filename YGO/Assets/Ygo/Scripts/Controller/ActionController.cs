using UnityEngine;
using Ygo.Controller.Component;

namespace Ygo.Controller
{
    public class ActionController : MonoBehaviour
    {
        [Header("Actions")] 
        [field: SerializeField]
        private ButtonController[] buttons;
        [field: SerializeField]
        private Transform handPosition;
        [field: SerializeField]
        private Transform frontRowPosition;
        [field: SerializeField]
        private Transform backRowPosition;

        public void Init()
        {
            
        }
    }
}