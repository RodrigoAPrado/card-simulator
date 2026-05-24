using System;
using System.Linq;
using UnityEngine;
using Ygo.Controller.Card;
using Ygo.Controller.Data;
using Ygo.Controller.Field;
using Ygo.Core.Duel;
using Ygo.Scripts.Core.Enum;
using Ygo.Scripts.Core.Event.Base;

namespace Ygo.Controller
{
    public class FieldController : MonoBehaviour
    {
        [field:SerializeField]
        private FieldZoneController[] fieldZones;
        
        private DuelInstance _duelInstance;
        
        public void Init(DuelInstance duelInstance, CardImageLibrary library)
        {
            foreach (var fieldZone in fieldZones)
            {
                fieldZone.Init(library);
            }
            
            //_duelInstance.EventQueue.Subscribe<SelectChainEvent>(OnSelectChainEvent);
        }
    }
}