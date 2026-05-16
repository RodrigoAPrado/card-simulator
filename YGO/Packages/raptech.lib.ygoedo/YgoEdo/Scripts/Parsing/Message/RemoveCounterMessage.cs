using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Constant;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class RemoveCounterMessage : BaseMessage, IRemoveCounterMessage
    {
        private readonly OCG_CounterType _counterType;
        private readonly OCG_CardLocation _location;

        public RemoveCounterMessage(OCG_CounterType counterType, byte player, OCG_CardLocation location, byte sequence,
            ushort count)
        {
            _counterType = counterType;
            Player = player;
            _location = location;
            Sequence = sequence;
            Count = count;
            CounterType = _counterType.ToCounterType();
            Location = _location.ToLocation();
        }

        public CounterType CounterType { get; }
        public byte Player { get; }
        public Location Location { get; }
        public byte Sequence { get; }
        public ushort Count { get; }

        public override string ToString()
        {
            return
                $"RemoveCounter, CounterType={_counterType}, Player={Player}, Location={_location} Sequence={Sequence}, Count={Count}";
        }
    }
}