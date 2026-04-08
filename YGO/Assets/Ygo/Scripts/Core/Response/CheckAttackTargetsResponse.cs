using System.Collections.Generic;
using Ygo.Core.Abstract;

namespace Ygo.Core.Response
{
    public class CheckAttackTargetsResponse
    {
        public bool DoNothing => Attacker == null;
        public bool DirectAttack => CanAttackDirectly && Targets.Count <= 0;
        public bool CanAttackDirectly { get; set; }
        public ICardInstance Attacker { get; set; }
        public IList<ICardInstance> Targets { get; set; }
        public CheckAttackTargetsResponse(ICardInstance attacker, List<ICardInstance> targets)
        {
            Attacker = attacker;
            Targets = targets;
        }
    }
}