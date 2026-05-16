using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component
{
    public class FullLocationReference : IFullLocationReference
    {
        private readonly OCG_CardLocation _ocgLocation;
        private readonly OCG_CardPosition _ocgPosition;

        public FullLocationReference(byte controller, OCG_CardLocation location, uint sequence,
            OCG_CardPosition position)
        {
            Controller = controller;
            _ocgLocation = location;
            Sequence = sequence;
            _ocgPosition = position;
        }

        public byte Controller { get; }
        public Location Location => _ocgLocation.ToLocation();
        public uint Sequence { get; }
        public CardPosition Position => _ocgPosition.ToCardPosition();

        public bool IsLocationEmpty()
        {
            return _ocgLocation == OCG_CardLocation.Unknown;
        }

        public override string ToString()
        {
            return $"[Player={Controller}, Location={_ocgLocation}, Sequence={Sequence}, Position={_ocgPosition}]";
        }
    }
}