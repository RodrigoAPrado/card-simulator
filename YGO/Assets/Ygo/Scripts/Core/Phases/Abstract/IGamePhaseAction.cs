using System;
using Ygo.Core.Response;

namespace Ygo.Core.Phases.Abstract
{
    public interface IGamePhaseAction
    {
        ActionResult DrawCard(Guid playerId);
    }
}