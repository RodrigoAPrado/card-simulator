using Ygo.Core.Abstract;

namespace Ygo.Core.Response
{
    public class BattleResponse
    {
        public bool DoNothing => Attacker == null;
        public bool DirectAttack => Target == null;
        public ICardInstance Attacker { get; set; }
        public ICardInstance Target { get; set; }
        public BattleResponse(ICardInstance attacker, ICardInstance target)
        {
            Attacker = attacker;
            Target = target;
        }

        public BattleResponse(BattleContext battleContext)
        {
            Attacker = battleContext.Attacker;
            Target = battleContext.Target;
        }
    }
}