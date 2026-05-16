using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Flag;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component.Command.Base
{
    public abstract class BattleCmdChoiceCard : BattleCmdChoice
    {
        protected readonly OCG_CardLocation _location;

        protected BattleCmdChoiceCard(
            PlayerBattleAction action,
            uint index,
            uint cardCode,
            byte controller,
            OCG_CardLocation location,
            uint sequence)
            : base(action, index)
        {
            CardCode = cardCode;
            Controller = controller;
            _location = location;
            Sequence = sequence;
        }

        public uint CardCode { get; }
        public byte Controller { get; }
        public uint Sequence { get; }
    }
}