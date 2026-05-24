using UnityEngine;
using Ygo.Scripts.Core.Enum;
using Ygo.View.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Enum;

namespace Ygo.Controller.Field
{
    public class FieldZoneController : MonoBehaviour
    {
        [field: SerializeField] 
        private HoverView hoverView;
        [field: SerializeField] 
        private FieldZones fieldZone;
        [field: SerializeField]
        private PointOfView pointOfView;
        [field: SerializeField]
        private Transform content;

        public void Init()
        {
            hoverView.ToggleEnable(true);
        }

        public void Flip90()
        {
            content.localRotation = Quaternion.Euler(0f, 90f, 0f);
        }

        public void FlipStraight()
        {
            content.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}