using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component.Command.Battle;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Data.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component.Command.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component.Command.Battle
{
    public class BattleCmdAttackChoice : BattleCmdChoiceCard, IBattleAttack
    {
        public BattleCmdAttackChoice(
            PlayerBattleAction action,
            uint index,
            uint cardCode,
            byte controller,
            OCG_CardLocation location,
            uint sequence,
            bool directAttack)
            : base(action, index, cardCode, controller, location, sequence)
        {
            DirectAttack = directAttack;
            Location = _location.ToLocation();
        }

        public Location Location { get; }
        public bool DirectAttack { get; }

        public override string ToString()
        {
            return $"to attack with {CardLibrary.InternalGetCard(CardCode).Name}...";
        }
    }
}