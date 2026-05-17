using UnityEngine;

namespace Ygo.View.ScriptableObjects
{

    public enum CardIconType
    {
        Earth = 0,
        Water = 1,
        Fire = 2,
        Wind = 3,
        Light = 4,
        Dark = 5,
        Divine = 6,
        Trap = 7,
        Spell = 8
    }

    [CreateAssetMenu(fileName = "CardIconDatabase", menuName = "Cards/CardIconDatabase")]
    public class CardIconDatabase : ScriptableObject
    {
        [System.Serializable]
        public struct IconEntry
        {
            public CardIconType type;
            public Sprite icon;
        }

        public IconEntry[] icons;

        public Sprite GetIcon(CardIconType type)
        {
            foreach (var entry in icons)
            {
                if (entry.type == type)
                    return entry.icon;
            }

            Debug.LogWarning("Icon not found for type: " + type);
            return null;
        }
    }
}
