using System.Collections.Generic;
using Ygo.Scripts.Cards.Enums;

namespace Ygo.Scripts.Cards
{
    public class MonsterCard
    {
        public int Id { get; }
        public CardType CardType { get; }
        public MonsterAttribute Attribute { get; }
        public int Level { get; }
        public MonsterType MonsterType { get; }
        public IList<MonsterKind> MonsterKinds { get; }
        public int Atk { get; }
        public int Def { get; }
        public int PendulumScale { get; }
        public IList<int> LinkArrows { get; }
        public IList<int> EffectIds { get; }
        public string Text { get; }

        public MonsterCard(int id, CardType cardType)
        {
            
        }
    }
}