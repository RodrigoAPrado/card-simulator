using UnityEngine;

namespace Ygo.View.ScriptableObjects
{
    public enum SpellTrapIconType
    {
        NoIcon = 0,
        QuickSpell = 1,
        EquipSpell = 2,
        Continuous = 3,
        RitualSpell = 4,
        FieldSpell = 5,
        CounterTrap = 6
    }
    
    [CreateAssetMenu(fileName = "SpellTrapIconDatabase", menuName = "Cards/SpellTrapIconDatabase")]
    public class SpellTrapIconDatabase : ScriptableObject
    {
        [System.Serializable]
        public struct IconEntry
        {
            public SpellTrapIconType type;
            public Sprite icon;
        }
        
        public IconEntry[] icons;
        
        public Sprite GetIcon(SpellTrapIconType type)
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