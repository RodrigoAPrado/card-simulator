using System.Collections.Generic;
using System.Text;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class CardSelectedMessage : BaseMessage, ICardSelectMessage
    {
        private readonly List<FullLocationReference> _locations;

        public CardSelectedMessage(List<FullLocationReference> locations)
        {
            _locations = locations;
        }

        public IReadOnlyList<IFullLocationReference> Locations => _locations;

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Cards selected on the following locations:");
            foreach (var l in _locations) sb.AppendLine(l.ToString());

            return sb.ToString();
        }
    }
}