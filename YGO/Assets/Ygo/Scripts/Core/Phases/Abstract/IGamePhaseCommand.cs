using System;
using Ygo.Core.Abstract;
using Ygo.Core.Board.Abstract;
using Ygo.Core.Response;

namespace Ygo.Core.Phases.Abstract
{
    public interface IGamePhaseCommand
    {
        ActionQuery ClickedOnMainDeck(Guid requesterId, Guid ownerId);
        ActionQuery ClickedOnCardInHand(Guid requesterId, Guid ownerId, ICardInstance card);
        ActionQuery ClickedOnZone(Guid requesterId, Guid ownerId, IBoardZone zone);
        ActionQuery ClickedOnNextPhase(Guid requesterId);
    }
}