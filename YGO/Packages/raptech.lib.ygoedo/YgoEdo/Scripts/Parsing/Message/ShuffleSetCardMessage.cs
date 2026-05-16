using System.Collections.Generic;
using System.Text;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class ShuffleSetCardMessage : BaseMessage, IShuffleSetCardMessage
    {
        private readonly List<FullLocationReference> _cards;

        private readonly OCG_CardLocation _location;
        private readonly List<FullLocationReference> _xyzs;

        public ShuffleSetCardMessage(
            OCG_CardLocation location,
            List<FullLocationReference> cards,
            List<FullLocationReference> xyzs
        )
        {
            _location = location;
            _cards = cards;
            _xyzs = xyzs;
            Location = _location.ToLocation();
        }

        public Location Location { get; }
        public IReadOnlyList<IFullLocationReference> Cards => _cards;
        public IReadOnlyList<IFullLocationReference> Xyzs => _xyzs;

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("ShuffleSetCardMessage=[Cards=[");

            foreach (var c in Cards) sb.AppendLine($"[{c}],");

            sb.AppendLine("], XyzMaterials=[");


            foreach (var c in Cards) sb.AppendLine($"[{c}],");

            sb.AppendLine("]]");
            return sb.ToString();
        }
    }
}