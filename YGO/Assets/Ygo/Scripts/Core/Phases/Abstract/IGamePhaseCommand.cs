using System;
using Ygo.Core.Abstract;
using Ygo.Core.Response;

namespace Ygo.Core.Phases.Abstract
{
    public interface IGamePhaseCommand
    {
        ActionQuery ClickedOnMainDeck(Guid playerId);
        ActionQuery ClickedOnCardInHand(Guid playerId, ICardInstance card);
    }
}