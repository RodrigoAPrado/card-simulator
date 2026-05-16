using System.Text;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class AttackMessage : BaseMessage, IAttackMessage
    {
        private readonly FullLocationReference _attacker;
        private readonly FullLocationReference _target;

        public AttackMessage(FullLocationReference attacker, FullLocationReference target)
        {
            _attacker = attacker;
            _target = target;
        }

        public IFullLocationReference Attacker => _attacker;
        public IFullLocationReference Target => _target;

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Player {_attacker.Controller} declares attack on Player {_target.Controller}");
            sb.Append($"in {_target.Sequence}, that is {_target.Position}, and on {_target.Location}, with ");
            sb.Append($"a card in {_attacker.Sequence}, that is {_attacker.Position}, and on {_attacker.Location}");
            return sb.ToString();
        }
    }
}