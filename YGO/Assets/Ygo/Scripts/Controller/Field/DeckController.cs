using System;
using UnityEngine;
using Ygo.Controller.Component;
using Ygo.Scripts.Core.Enum;
using Ygo.View;
using Ygo.View.Field;

namespace Ygo.Controller.Field
{
    public class DeckController : MonoBehaviour
    {
        [field: SerializeField] 
        private DeckView view;
        [field: SerializeField] 
        private PointOfView pointOfView;
        
        public void Init(int deckSize)
        {
            view.SetSize(deckSize);
        }
    }
}