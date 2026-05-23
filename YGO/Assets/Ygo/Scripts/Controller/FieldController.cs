using System;
using System.Linq;
using UnityEngine;
using Ygo.Controller.Card;
using Ygo.Controller.Field;
using Ygo.Scripts.Core.Enum;

namespace Ygo.Controller
{
    public class FieldController : MonoBehaviour
    {
        [field:SerializeField]
        private FieldZoneController[] fieldZones;
        
        public void Init()
        {
            foreach (var fieldZone in fieldZones)
            {
                fieldZone.Init();
            }
        }
    }
}