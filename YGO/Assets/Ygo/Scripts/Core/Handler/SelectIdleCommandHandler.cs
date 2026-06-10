using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Ygo.Core.Duel;
using Ygo.Scripts.Core.Event.Base;
using Ygo.Scripts.Core.Handler.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component.Command.Idle;

namespace Ygo.Scripts.Core.Handler
{
    public class SelectIdleCommandHandler : BaseHandler<ISelectIdleCommandMessage>
    {
        public override UniTask<IReadOnlyList<IEvent>> HandleMessage(ISelectIdleCommandMessage message, DuelState duelState)
        {
            foreach (var command in message.Choices)
            {
                switch (command)
                {
                    case IIdleNormalSummon typedCommand:
                        break;
                    case IIdleSpecialSummon typedCommand:
                        break;
                    case IIdleChangeCardPosition typedCommand:
                        break;
                    case IIdleSet typedCommand:
                        break;
                    case IIdleSpellOrTrapSet typedCommand:
                        break;
                    case IIdleEffectActivation typedCommand:
                        break;
                    case IIdleToBattlePhase typedCommand:
                        break;
                    case IIdleToEndPhase typedCommand:
                        break;
                    case IIdleShuffleHand typedCommand:
                        break;
                }
            }
        }
    }
}

/*
 *
 *
        NormalSummon = 0,
        SpecialSummon = 1,
        ChangeCardPosition = 2,
        Set = 3,
        SpellOrTrapSet = 4,
        EffectActivation = 5,
        GoToBattlePhase = 6,
        GotoEndPhase = 7,
        ShuffleHand = 8
 *
 * 
 */

/*
YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.SelectIdleCmdMessage
Player 0, input your action:
[0] => to SpellOrTrapSet The Fallen & The Virtuous, Location=Hand, Sequence=0, Index=0...
[1] => to SpellOrTrapSet Gold Sarcophagus, Location=Hand, Sequence=2, Index=1...
[2] => to SpellOrTrapSet Foolish Burial, Location=Hand, Sequence=3, Index=2...
[3] => to EffectActivation Albion the Shrouded Dragon, Location=Hand, Sequence=1, Index=0, Description=Special Summon...
[4] => to EffectActivation Gold Sarcophagus, Location=Hand, Sequence=2, Index=1, Description=Activate...
[5] => to EffectActivation Foolish Burial, Location=Hand, Sequence=3, Index=2, Description=...
[6] => to GotoEndPhase...
[7] => to ShuffleHand...

 */