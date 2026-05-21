using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Flag;

namespace Ygo.Scripts.Core.Card
{
    public class CardState
    {
        public ICardData Data { get; }
        public byte Sequence { get; private set; }
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
    }
}