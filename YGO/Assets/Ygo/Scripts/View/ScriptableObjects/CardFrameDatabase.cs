using UnityEngine;

namespace Ygo.View.ScriptableObjects
{
    public enum CardFrameType
    {
        Normal,
        Effect,
        Ritual,
        Fusion,
        Synchro,
        Xyz,
        Link,
        NormalPendulum,
        EffectPendulum,
        RitualPendulum,
        FusionPendulum,
        SynchroPendulum,
        XyzPendulum,
        Spell,
        Trap,
        Token
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