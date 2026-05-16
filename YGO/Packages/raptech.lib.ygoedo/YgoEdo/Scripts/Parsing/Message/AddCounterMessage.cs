using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Constant;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class AddCounterMessage : BaseMessage, IAddCounterMessage
    {
        private readonly OCG_CounterType _internalCounterType;
        private readonly OCG_CardLocation _internalLocation;

        public AddCounterMessage(OCG_CounterType counterType, byte player, OCG_CardLocation location, byte sequence,
            ushort count)
        {
            _internalCounterType = counterType;
            Player = player;
            _internalLocation = location;
            Sequence = sequence;
            Count = count;
            CounterType = _internalCounterType.ToCounterType();
            Location = _internalLocation.ToLocation();
        }

        public CounterType CounterType { get; }
        public byte Player { get; }
        public Location Location { get; }
        public byte Sequence { get; }
        public ushort Count { get; }

        public override string ToString()
        {
            return
                $"[(AddCounter) CounterType={_internalCounterType}, Player={Player}, Location={_internalLocation} Sequence={Sequence}, Count={Count}]";
        }
    }
}