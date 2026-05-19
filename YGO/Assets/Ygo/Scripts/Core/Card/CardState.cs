using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card.Flag;

namespace Ygo.Scripts.Core.Card
{
    public class CardState
    {
        public ICardData Data { get; }
        public byte Sequence { get; private set; }
        public CardPosition Position { get; private set; }
        
        public CardState(ICardData data, byte sequence)
        {
            Data = data;
            Sequence = sequence;
            Position = CardPosition.FaceDown;
        }
    }
}