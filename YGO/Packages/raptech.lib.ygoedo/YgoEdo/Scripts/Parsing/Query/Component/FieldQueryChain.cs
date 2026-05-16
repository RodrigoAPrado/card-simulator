using YgoSoul.RapTech.Lib.YgoEdo.Core.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Data.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Query.Component
{
    public class FieldQueryChain
    {
        public FieldQueryChain(
            uint cardCode,
            FullLocationReference location,
            byte triggerController,
            OCG_CardLocation triggerLocation,
            uint triggerSequence,
            ulong description
        )
        {
            CardCode = cardCode;
            Location = location;
            TriggerController = triggerController;
            TriggerLocation = triggerLocation;
            TriggerSequence = triggerSequence;
            Description = description;
        }

        public uint CardCode { get; }
        public FullLocationReference Location { get; }
        public byte TriggerController { get; }
        public OCG_CardLocation TriggerLocation { get; }
        public uint TriggerSequence { get; }
        public ulong Description { get; }

        public override string ToString()
        {
            return $"FieldQueryChain=[Card={CardLibrary.InternalGetCard(CardCode).Name}, " +
                   $"Location=[{Location}], " +
                   $"TriggerController={TriggerController}, " +
                   $"TriggerLocation={TriggerLocation}, " +
                   $"TriggerSequence={TriggerSequence}], " +
                   $"Description={DescriptionUtil.GetDescription(Description, CardLibrary.Instance)}," +
                   $"DescriptionHex={Description:x16}]";
        }
    }
}