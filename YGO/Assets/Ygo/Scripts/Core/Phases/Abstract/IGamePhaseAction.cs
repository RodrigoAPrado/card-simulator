using System;
using Ygo.Core.Abstract;
using Ygo.Core.Board.Abstract;
using Ygo.Core.Response;

namespace Ygo.Core.Phases.Abstract
{
    public interface IGamePhaseAction
    {
        ActionResult DrawCard(Guid playerId);
        ActionResult CheckNormalSummon(Guid playerId, ICardInstance card);
        ActionResult CheckNormalSet(Guid playerId, ICardInstance card);
        ActionResult DoNormalSummon(Guid playerId, ICardInstance card, IBoardZone boardZone);
        
    }
}