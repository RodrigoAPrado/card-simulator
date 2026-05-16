using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Data.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class ChainingMessage : BaseMessage, IChainingMessage
    {
        private readonly OCG_CardLocation _activationLocation;
        private readonly ulong _description;
        private readonly FullLocationReference _locationReference;

        public ChainingMessage(uint cardCode, FullLocationReference locationReference,
            byte activationPlayer, OCG_CardLocation activationLocation, uint activationSequence, ulong description,
            uint chainSize)
        {
            CardCode = cardCode;
            _locationReference = locationReference;
            ActivationPlayer = activationPlayer;
            _activationLocation = activationLocation;
            ActivationSequence = activationSequence;
            _description = description;
            ChainSize = chainSize;
            ActivationLocation = _activationLocation.ToLocation();
            Description = DescriptionUtil.GetDescription(_description, CardLibrary.Instance);
        }

        public uint CardCode { get; }
        public IFullLocationReference LocationReference => _locationReference;
        public byte ActivationPlayer { get; }
        public Location ActivationLocation { get; }
        public uint ActivationSequence { get; }
        public string Description { get; }
        public uint ChainSize { get; }

        public override string ToString()
        {
            return $"Card {CardLibrary.InternalGetCard(CardCode).Name} from " +
                   $"Player={LocationReference.Controller}, Location={LocationReference.Location}, " +
                   $"Sequence={LocationReference.Sequence}, Position={LocationReference.Position}, " +
                   $"\nwas activated by Player={ActivationPlayer}, Location={ActivationLocation}, Sequence={ActivationSequence}, " +
                   $"\nwith Description={Description}, Chain Size={ChainSize}";
        }
    }
}