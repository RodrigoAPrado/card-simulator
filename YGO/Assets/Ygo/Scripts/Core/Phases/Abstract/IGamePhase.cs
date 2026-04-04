using Ygo.Core.Abstract;
using Ygo.Core.Response;

namespace Ygo.Core.Phases.Abstract
{
    public interface IGamePhase
    {
        string Name { get; }
        IGamePhase NextPhase { get; }
        bool HasNextPhase { get; }
        void Init();
        bool DrawFromDeck();
        ClickedOnCardHandResponse ClickedOnCardInHand(ICardInstance card);

    }
}