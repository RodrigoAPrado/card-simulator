using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class EquipMessage : BaseMessage, IEquipMessage
    {
        private readonly FullLocationReference _equipment;
        private readonly FullLocationReference _target;

        public EquipMessage(FullLocationReference equipment, FullLocationReference target)
        {
            _equipment = equipment;
            _target = target;
        }

        public IFullLocationReference Equipment => _equipment;
        public IFullLocationReference Target => _target;

        public override string ToString()
        {
            return $"Equip, Equipment={_equipment}, Target={_target}";
        }
    }
}