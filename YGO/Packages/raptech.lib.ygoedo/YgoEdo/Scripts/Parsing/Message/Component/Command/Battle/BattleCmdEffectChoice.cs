using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component.Command.Battle;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Data.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component.Command.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component.Command.Battle
{
    public class BattleCmdEffectChoice : BattleCmdChoiceCard, IBattleActivateEffect
    {
        private readonly ulong _description;

        public BattleCmdEffectChoice(
            PlayerBattleAction action,
            uint index,
            uint cardCode,
            byte controller,
            OCG_CardLocation location,
            uint sequence,
            ulong description)
            : base(action, index, cardCode, controller, location, sequence)
        {
            _description = description;
            Location = _location.ToLocation();
            Description = DescriptionUtil.GetDescription(_description, CardLibrary.Instance);
        }

        public Location Location { get; }
        public string Description { get; }

        public override string ToString()
        {
            return
                $"to activate {CardLibrary.InternalGetCard(CardCode).Name}'s effect, description={DescriptionUtil.GetDescription(_description, CardLibrary.Instance)}";
        }
    }
}