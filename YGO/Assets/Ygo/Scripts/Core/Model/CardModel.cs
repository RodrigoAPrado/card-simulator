using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Flag;

namespace Ygo.Scripts.Core.Model
{
    public class  CardModel
    {
        public ICardData Data { get; set; }
        public uint Sequence { get; set; }
        public CardPosition Position { get; set; }
        public Location CardLocation { get; set; }
        public FieldZones CardFieldZone { get; set; }
        public byte Controller { get; set; }
        public string Description { get; set; }
    }
}