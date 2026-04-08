using Ygo.Core.Abstract;

namespace Ygo.Core
{
    public class BattleContext
    {
        public bool DirectAttack => Target == null;
        public ICardInstance Attacker { get; set; }
        public ICardInstance Target { get; set; }
        public BattleContext(ICardInstance attacker, ICardInstance target)
        {
            Attacker = attacker;
            Target = target;
        }
    }
}