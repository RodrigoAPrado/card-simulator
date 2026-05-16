using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component.Command.Idle;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component.Command.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component.Command.Idle
{
    public class IdleCmdChangeCardPosition : IdleCmdChoiceCard, IIdleChangeCardPosition
    {
        public IdleCmdChangeCardPosition(PlayerIdleAction playerIdleAction, uint cardCode, byte player,
            OCG_CardLocation location,
            uint sequence, uint index, ulong description) : base(playerIdleAction, cardCode, player, location, sequence,
            index, description)
        {
            Location = _location.ToLocation();
        }

        public Location Location { get; }
    }
}