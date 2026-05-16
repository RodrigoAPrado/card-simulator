using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class HintAttributeMessage : BaseMessage, IHintAttributeMessage
    {
        private readonly OCG_MonsterAttributes _attribute;

        public HintAttributeMessage(byte player, OCG_MonsterAttributes attribute)
        {
            Player = player;
            _attribute = attribute;
            Attribute = _attribute.ToCardAttribute();
        }

        public byte Player { get; }
        public CardAttribute Attribute { get; }

        public override string ToString()
        {
            return $"Hint: Player={Player} Attribute={_attribute}";
        }
    }
}