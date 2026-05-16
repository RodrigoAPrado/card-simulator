using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Data.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component
{
    public class ChainOption : IChainOption
    {
        private readonly ulong _description;
        private readonly FullLocationReference _locationReference;

        public ChainOption(uint code, FullLocationReference locationReference, ulong description)
        {
            Code = code;
            _locationReference = locationReference;
            _description = description;
            Description = DescriptionUtil.GetDescription(_description, CardLibrary.Instance);
        }

        public uint Code { get; }
        public string Description { get; }
        public IFullLocationReference LocationReference => _locationReference;

        public override string ToString()
        {
            return $"Card={CardLibrary.InternalGetCard(Code).Name}, " +
                   $"Ctrl={_locationReference.Controller}, " +
                   $"Loc={_locationReference.Location}, " +
                   $"Seq={_locationReference.Sequence}, " +
                   $"SubSeq={_locationReference.Position}. " +
                   $"\nDesc={DescriptionUtil.GetDescription(_description, CardLibrary.Instance)}";
        }
    }
}