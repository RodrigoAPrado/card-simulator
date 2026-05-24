using System;
using UnityEngine;
using Ygo.Controller.Component;
using Ygo.Scripts.Core.Enum;
using Ygo.Scripts.Core.Event.Base;
using Ygo.Scripts.Data;
using Ygo.View;
using Ygo.View.Field;

namespace Ygo.Controller.Field
{
    public class DeckController : MonoBehaviour
    {
        public PointOfView PointOfView => pointOfView;
        
        [field: SerializeField] 
        protected DeckView view;
        [field: SerializeField] 
        protected PointOfView pointOfView;
        
        public virtual void Init(int deckCount, EventQueue eventQueue)
        {
            view.SetSize(deckCount);
        }

        protected void SetDeckCount(int deckCount)
        {
            view.SetSize(deckCount);
        }
    }
}