using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;

namespace Ygo.Scripts.Core.Card
{
    public class CardState
    {
        public ICardData Data { get; }
        public uint Sequence { get; private set; }
        public CardPosition Position { get; private set; }
        public Location CardLocation { get; private set; }
        public byte Controller { get; private set; }
        
        public CardState(ICardData data, Location location, byte controller)
        {
            Data = data;
            Sequence = 255;
            Position = CardPosition.FaceDown;
            CardLocation = location;
            Controller = controller;
        }

        public void UpdateState(CardPosition position, Location cardLocation, byte controller)
        {
            Position = position;
            CardLocation = cardLocation;
            Controller = controller;
        }
        
        public void UpdateState(IFullLocationReference location)
        {
            Sequence = location.Sequence;
            Position = location.Position;
            CardLocation = location.Location;
            Controller = location.Controller;
        }
    }
}