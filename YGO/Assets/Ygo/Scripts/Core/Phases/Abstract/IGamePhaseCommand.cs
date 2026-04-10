using System;
using Ygo.Core.Response;

namespace Ygo.Core.Phases.Abstract
{
    public interface IGamePhaseCommand
    {
        ActionQuery ClickedOnMainDeck(Guid playerId);
    }
}