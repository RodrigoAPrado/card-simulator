using UnityEngine;
using Ygo.Controller.Card;
using Ygo.Core.Abstract;

namespace Ygo.Controller.Field
{
    public class ZoneController : MonoBehaviour
    {
        [Header("Config")]
        [field: SerializeField]
        private ZoneType zoneType;
        [field: SerializeField]
        private int zoneId;
        
        private ICardInstance _cardInstance;
        
        public bool Occupied => _cardInstance != null;

        public void OnClick()
        {
            Debug.Log("Click");
        }
    }

    public enum ZoneType
    {
        MonsterZone = 0,
        SpellTrapZone = 1,
        FieldZone = 2,
        Graveyard = 3,
        MainDeck = 4,
        ExtraDeck = 5,
    }
}