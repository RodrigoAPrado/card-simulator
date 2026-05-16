using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class BattleMessage : BaseMessage, IBattleMessage
    {
        private readonly BattleReference _attacker;
        private readonly BattleReference _defender;

        public BattleMessage(
            BattleReference attacker,
            BattleReference defender
        )
        {
            _attacker = attacker;
            _defender = defender;
        }

        public IBattleReference Attacker => _attacker;
        public IBattleReference Defender => _defender;

        public override string ToString()
        {
            return $"[Attacker={_attacker}, Defender={_defender}]";
        }
    }
}