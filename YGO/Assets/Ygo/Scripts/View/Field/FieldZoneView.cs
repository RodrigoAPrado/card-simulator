using UnityEngine;

namespace Ygo.View.Field
{
    public class FieldZoneView : MonoBehaviour
    {
        [field: SerializeField]
        private Transform content;

        public void Init()
        {
            FlipStraight();
        }
        
        public void Flip90()
        {
            content.localRotation = Quaternion.Euler(0f, 0f, 90f);
        }

        public void FlipStraight()
        {
            content.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}