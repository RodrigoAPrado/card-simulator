using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component.Command.Idle;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component.Command.Base;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component.Command.Idle
{
    public class IdleCmdShuffleHand : IdleCmdChoiceOther, IIdleShuffleHand
    {
        public IdleCmdShuffleHand(PlayerIdleAction playerIdleAction, uint index) : base(playerIdleAction, index)
        {
        }
    }
}