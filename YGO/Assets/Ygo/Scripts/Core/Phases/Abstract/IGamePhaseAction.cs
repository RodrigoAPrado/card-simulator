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
        ActionResult DoNormalSet(Guid playerId, ICardInstance card, IBoardZone boardZone);
        ActionResult DoFlipSummon(Guid playerId, ICardInstance card);
        ActionResult DoSwitchMonsterToAttack(Guid playerId, ICardInstance card);
        ActionResult DoSwitchMonsterToDefense(Guid playerId, ICardInstance card);
        ActionResult CheckAttack(Guid playerId, ICardInstance card);
        ActionResult DeclareAttack(Guid playerId, ICardInstance attacker, ICardInstance defender);
        ActionResult DeclareDirectAttack(Guid playerId, ICardInstance attacker);
        
    }
}