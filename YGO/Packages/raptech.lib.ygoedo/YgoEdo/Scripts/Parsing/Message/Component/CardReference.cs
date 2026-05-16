using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Data.Card;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component
{
    public class CardReference : ICardReference
    {
        private readonly FullLocationReference _locationReference;

        public CardReference(
            uint cardCode,
            FullLocationReference locationReference,
            uint index)
        {
            CardCode = cardCode;
            _locationReference = locationReference;
            Index = index;
        }

        public IFullLocationReference LocationReference => _locationReference;
        public uint CardCode { get; }
        public uint Index { get; }
        public byte ReleaseValue { get; set; }
        public ushort CounterAmount { get; set; }
        public ulong Sum { get; set; }

        public override string ToString()
        {
            return $"{CardLibrary.InternalGetCard(CardCode).Name}, " +
                   $"Location={LocationReference.Location}, " +
                   $"Sequence={LocationReference.Sequence}, " +
                   $"Position={LocationReference.Position}, " +
                   $"Index={Index}";
        }
    }
}