using System.Collections.Generic;
using System.Text;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class RandomSelectedMessage : BaseMessage, IRandomSelectedMessage
    {
        private readonly List<FullLocationReference> _locations;

        public RandomSelectedMessage(byte player, List<FullLocationReference> location)
        {
            Player = player;
            _locations = location;
        }

        public byte Player { get; }
        public IReadOnlyList<IFullLocationReference> Locations => _locations;

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"RandomSelected=[\nPlayer={Player},Locations=[");

            foreach (var location in _locations) sb.AppendLine($"[{location}],");

            sb.AppendLine("]]");
            return sb.ToString();
        }
    }
}