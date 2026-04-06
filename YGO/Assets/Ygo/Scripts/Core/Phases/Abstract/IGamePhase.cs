using Ygo.Core.Abstract;
using Ygo.Core.Board.Abstract;
using Ygo.Core.Enums;
using Ygo.Core.Response;

namespace Ygo.Core.Phases.Abstract
{
    public interface IGamePhase
    {
        string Name { get; }
        IGamePhase NextPhase { get; }
        bool HasNextPhase { get; }
        GameStep CurrentStep { get; }
        void Init(TurnContext context);
        bool DrawFromDeck();
        ClickedOnCardHandResponse ClickedOnCardInHand(ICardInstance card);
        WhereToSummonMonsterResponse CheckWhereToSummonMonster(ICardInstance card);
        void CancelSummoning();
        bool SummonCardOnSelectedZone(ICardInstance card, IBoardZone zone);
        void ToOpenGameStep();
    }
}