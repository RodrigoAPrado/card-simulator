using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Constant;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class NewPhaseMessage : SimpleTextMessage, INewPhaseMessage
    {
        private OCG_GamePhases _gamePhase;

        public NewPhaseMessage(OCG_GamePhases gamePhase) : base($"It is the {gamePhase.ToString()}")
        {
            _gamePhase = gamePhase;
            GamePhase = gamePhase.ToDuelPhase();
        }

        public DuelPhase GamePhase { get; }
    }
}