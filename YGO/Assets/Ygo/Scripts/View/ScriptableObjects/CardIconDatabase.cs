using UnityEngine;

namespace Ygo.View.ScriptableObjects
{

    public enum CardIconType
    {
        Dark,
        Light,
        Fire,
        Wind,
        Earth,
        Water,
        Divine,
        Trap,
        Spell
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
