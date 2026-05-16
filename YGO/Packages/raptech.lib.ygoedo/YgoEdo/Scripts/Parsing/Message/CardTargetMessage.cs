using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class CardTargetMessage : BaseMessage, ICardTargetMessage
    {
        private readonly FullLocationReference _card;
        private readonly FullLocationReference _target;

        public CardTargetMessage(FullLocationReference card, FullLocationReference target)
        {
            _card = card;
            _target = target;
        }

        public IFullLocationReference Card => _card;
        public IFullLocationReference Target => _target;

        public override string ToString()
        {
            return $"CardTarget, Card={Card}, Target={Target}";
        }
    }
}