using System;
using UnityEngine;
using Ygo.Controller.Card;

namespace Ygo.Controller
{
    public class HandController : MonoBehaviour
    {
        [field:SerializeField]
        private CardController[] cardControllers;
        [field: SerializeField] 
        private PointOfView pointOfView;

        private Guid _requesterId;
        private Guid _ownerId;

        public void Init()
        {
        }
    }
}