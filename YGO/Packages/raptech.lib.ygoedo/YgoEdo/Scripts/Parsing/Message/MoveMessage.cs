using System.Collections.Generic;
using System.Linq;
using System.Text;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.System.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Constant;
using YgoSoul.RapTech.Lib.YgoEdo.Data.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class MoveMessage : BaseMessage, IMoveMessage
    {
        private readonly FullLocationReference _newLocation;
        private readonly FullLocationReference _oldLocation;
        private readonly List<OCG_Reason> _reasons;

        public MoveMessage(
            uint cardCode,
            FullLocationReference oldLocation,
            FullLocationReference newLocation,
            List<OCG_Reason> reasons)
        {
            CardCode = cardCode;
            _oldLocation = oldLocation;
            _newLocation = newLocation;
            _reasons = reasons;
            Reasons = _reasons.Select(x => x.ToSystemReason()).ToList();
        }

        public uint CardCode { get; }
        public IFullLocationReference OldLocation => _oldLocation;
        public IFullLocationReference NewLocation => _newLocation;
        public IReadOnlyList<SystemReason> Reasons { get; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(
                $"Card {CardLibrary.InternalGetCard(CardCode).Name} was with player {_oldLocation.Controller} on the " +
                $"{_oldLocation.Location}, in sequence {_oldLocation.Sequence} and position {_oldLocation.Position}.");
            sb.AppendLine($"It moved to {_newLocation.Location}, in sequence {_newLocation.Sequence} and position " +
                          $"{_newLocation.Position}, and is now controlled by {_newLocation.Controller}, because of {_reasons.ToString()}");
            return sb.ToString();
        }
    }
}