using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Data.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component.Command.Base
{
    public abstract class IdleCmdChoiceCard : IIdleCmdChoice
    {
        public IdleCmdChoiceCard(PlayerIdleAction playerIdleAction, uint cardCode, byte player,
            OCG_CardLocation location, uint sequence, uint index, ulong description)
        {
            Action = playerIdleAction;
            Controller = player;
            Sequence = sequence;
            _location = location;
            CardCode = cardCode;
            Index = index;
            _description = description;
        }

        public byte Controller { get; }
        public uint Sequence { get; }
        public uint CardCode { get; }
        protected ulong _description { get; }
        protected OCG_CardLocation _location { get; }
        public PlayerIdleAction Action { get; }
        public uint Index { get; }

        public override string ToString()
        {
            var description = "";
            if (Action == PlayerIdleAction.EffectActivation)
                description = $", Description={DescriptionUtil.GetDescription(_description, CardLibrary.Instance)}";

            return
                $"to {Action.ToString()} {CardLibrary.InternalGetCard(CardCode).Name}, Location={_location.ToString()}, Sequence={Sequence}, Index={Index}{description}...";
        }
    }
}