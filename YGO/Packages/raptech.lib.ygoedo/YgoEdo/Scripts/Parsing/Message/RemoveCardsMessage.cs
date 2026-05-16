using System.Collections.Generic;
using System.Text;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class RemoveCardsMessage : BaseMessage, IRemoveCardsMessage
    {
        private readonly List<FullLocationReference> _locations;

        public RemoveCardsMessage(List<FullLocationReference> locations)
        {
            _locations = locations;
        }

        public IReadOnlyList<IFullLocationReference> Locations => _locations;

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("RemoveCards=[");
            foreach (var l in Locations) sb.AppendLine($"{l},");
            sb.AppendLine("]");
            return sb.ToString();
        }
    }
}