using Ygo.Core.Abstract;
using Ygo.Core.Enums;

namespace Ygo.Core.Events.Abstract
{
    public interface IBattleState
    {
        ICardInstance Attacker { get; }
        ICardInstance Defender { get; }
        BattleStep CurrentStep { get; }
        void ProceedBattleState();
    }
}