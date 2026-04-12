using System;
using Ygo.Core.Abstract;
using Ygo.Core.Board.Abstract;
using Ygo.Core.Enums;
using Ygo.Core.Response;

namespace Ygo.Core.Phases.Abstract
{
    public interface IGamePhase : IGamePhaseAction, IGamePhaseCommand
    {
        string Name { get; }
        GamePhase Phase { get; }
        PhaseStep CurrentStep { get; }
        void Init();
        
        /*
        ClickedOnCardResponse ClickedOnCardInHand(ICardInstance card);
        WhereToSummonMonsterResponse CheckWhereToSummonMonster(ICardInstance card);
        void CancelSummoning();
        bool SummonCardOnSelectedZone(ICardInstance card, IBoardZone zone);
        void ToOpenGameStep();
        void GoToNextPhase();
        ClickedOnCardResponse ClickedOnCardInField(ICardInstance card);
        CheckAttackTargetsResponse CheckAttackTargets(ICardInstance card);
        BattleResponse DeclareAttack(ICardInstance attacker, ICardInstance target);
        void ContinueTheDamageStep();
        */
    }
}