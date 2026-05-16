using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class HintRaceMessage : BaseMessage, IHintRaceMessage
    {
        private readonly OCG_MonsterRaces _race;

        public HintRaceMessage(byte player, OCG_MonsterRaces race)
        {
            Player = player;
            _race = race;
            MonsterType = _race.ToMonsterType();
        }

        public byte Player { get; }
        public MonsterType MonsterType { get; }

        public override string ToString()
        {
            return $"Hint: Player={Player}, Race={_race}";
        }
    }
}