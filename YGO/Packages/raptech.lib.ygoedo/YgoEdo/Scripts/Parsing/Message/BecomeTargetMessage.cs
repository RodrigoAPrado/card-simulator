using System.Collections.Generic;
using System.Text;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class BecomeTargetMessage : BaseMessage, IBecomeTargetMessage
    {
        private readonly List<FullLocationReference> _locations;

        public BecomeTargetMessage(List<FullLocationReference> locations)
        {
            _locations = locations;
        }

        public IReadOnlyList<IFullLocationReference> Locations => _locations;

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("[");

            for (var i = 0; i < _locations.Count; i++) sb.AppendLine($"Target_{i}={_locations[i]}");

            sb.AppendLine("]");
            return sb.ToString();
        }
    }
}