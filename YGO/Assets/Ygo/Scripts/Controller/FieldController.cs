using System;
using System.Linq;
using UnityEngine;
using Ygo.Controller.Card;
using Ygo.Controller.Field;

namespace Ygo.Controller
{
    public class FieldController : MonoBehaviour
    {
        [field:SerializeField]
        private ZoneController[] frontRowZones;
        [field:SerializeField]
        private CardController[] frontRowCards;
        [field: SerializeField] 
        private PointOfView pointOfView;

        private Guid _requesterId;
        private Guid _ownerId;
        
        public void Init()
        {
        }
    }
}