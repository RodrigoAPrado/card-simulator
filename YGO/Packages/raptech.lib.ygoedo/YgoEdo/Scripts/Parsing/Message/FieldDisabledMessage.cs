using System.Collections.Generic;
using System.Linq;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Constant;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class FieldDisabledMessage : BaseMessage, IFieldDisabledMessage
    {
        private readonly uint _mask;
        private readonly List<OCG_Zone> _zones;

        public FieldDisabledMessage(List<OCG_Zone> zones, uint mask)
        {
            _zones = zones;
            _mask = mask;
            Zones = _zones.Select(x => x.ToFieldZone()).ToList().AsReadOnly();
        }

        public IReadOnlyList<FieldZones> Zones { get; }

        public override string ToString()
        {
            return $"FieldDisabled={_mask:x8}";
        }
    }
}