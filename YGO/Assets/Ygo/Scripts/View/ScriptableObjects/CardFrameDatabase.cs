using UnityEngine;

namespace Ygo.View.ScriptableObjects
{
    public enum CardFrameType
    {
        Normal = 0,
        NormalPendulum = 1,
        Effect = 2,
        EffectPendulum = 3,
        Ritual = 4,
        RitualPendulum = 5,
        Fusion = 6,
        FusionPendulum = 7,
        Synchro = 8,
        SynchroPendulum = 9,
        Xyz = 10,
        XyzPendulum = 11,
        Link = 12,
        Spell = 13,
        Trap = 14,
        Token = 15
    }
    
    [CreateAssetMenu(fileName = "CardFrameDatabase", menuName = "Cards/CardFrameDatabase")]
    public class CardFrameDatabase : ScriptableObject
    {
        [System.Serializable]
        public struct FrameEntry
        {
            public CardFrameType type;
            public Sprite frame;
        }

        public FrameEntry[] frames;

        public Sprite GetFrame(CardFrameType type)
        {
            foreach (var entry in frames)
            {
                if (entry.type == type)
                    return entry.frame;
            }

            Debug.LogWarning("Frame not found for type: " + type);
            return null;
        }
    }
}