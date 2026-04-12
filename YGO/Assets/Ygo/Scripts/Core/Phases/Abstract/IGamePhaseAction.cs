using System;
using Ygo.Core.Abstract;
using Ygo.Core.Board.Abstract;
using Ygo.Core.Response;

namespace Ygo.Core.Phases.Abstract
{
    public interface IGamePhaseAction
    {
        ActionResult DrawForTurn(Guid ownerId);
        ActionResult CheckNormalSummon(Guid ownerId, ICardInstance card);
        ActionResult CheckNormalSet(Guid ownerId, ICardInstance card);
        ActionResult DoNormalSummon(Guid ownerId, ICardInstance card, IBoardZone boardZone);
        ActionResult DoNormalSet(Guid ownerId, ICardInstance card, IBoardZone boardZone);
        ActionResult DoFlipSummon(Guid ownerId, ICardInstance card);
        ActionResult DoTryFlip(Guid ownerId, ICardInstance card);
        ActionResult DoSwitchMonsterToAttack(Guid ownerId, ICardInstance card);
        ActionResult DoSwitchMonsterToDefense(Guid ownerId, ICardInstance card);
        ActionResult CheckAttack(Guid ownerId, ICardInstance attacker);
        ActionResult DeclareAttack(Guid ownerId, ICardInstance attacker, ICardInstance defender);
        ActionResult DeclareDirectAttack(Guid ownerId, ICardInstance attacker);
        
    }
}