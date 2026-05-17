using System;
using System.Collections.Generic;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component.Command.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component.Command.Idle;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser
{
    public class SelectIdleCmdParser : BaseParser
    {
        protected override IOcgMessage DoParse(byte[] buffer)
        {
            var reader = new PacketReader(buffer);

            reader.ReadByte(); // msg id

            var player = reader.ReadByte();

            var choices = new List<IIdleCmdChoice>();


            // helper local
            void ReadCardList(PlayerIdleAction action)
            {
                uint index = 0;
                var count = reader.ReadInt32();

                for (var i = count; i > 0; i--)
                {
                    var code = reader.ReadUInt32();
                    var controller = reader.ReadByte();
                    var location = (OCG_CardLocation)reader.ReadByte();
                    var sequence = action == PlayerIdleAction.ChangeCardPosition
                        ? reader.ReadByte()
                        : reader.ReadUInt32();

                    ulong description = 0;
                    if (action == PlayerIdleAction.EffectActivation)
                    {
                        description = reader.ReadULong64();
                        reader.Skip(1); // client mode
                    }

                    switch (action)
                    {
                        case PlayerIdleAction.NormalSummon:
                            choices.Add(new IdleCmdNormalSummon(
                                action,
                                code,
                                controller,
                                location,
                                sequence,
                                index,
                                description
                            ));
                            break;
                        case PlayerIdleAction.SpecialSummon:
                            choices.Add(new IdleCmdSpecialSummon(
                                action,
                                code,
                                controller,
                                location,
                                sequence,
                                index,
                                description
                            ));
                            break;
                        case PlayerIdleAction.ChangeCardPosition:
                            choices.Add(new IdleCmdChangeCardPosition(
                                action,
                                code,
                                controller,
                                location,
                                sequence,
                                index,
                                description
                            ));
                            break;
                        case PlayerIdleAction.Set:
                            choices.Add(new IdleCmdSet(
                                action,
                                code,
                                controller,
                                location,
                                sequence,
                                index,
                                description
                            ));
                            break;
                        case PlayerIdleAction.SpellOrTrapSet:
                            choices.Add(new IdleCmdSpellOrTrapSet(
                                action,
                                code,
                                controller,
                                location,
                                sequence,
                                index,
                                description
                            ));
                            break;
                        case PlayerIdleAction.EffectActivation:
                            choices.Add(new IdleCmdEffectActivation(
                                action,
                                code,
                                controller,
                                location,
                                sequence,
                                index,
                                description
                            ));
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(action), action, null);

                    }
                    index++;
                }
            }

            // ordem FIXA do protocolo
            ReadCardList(PlayerIdleAction.NormalSummon);
            ReadCardList(PlayerIdleAction.SpecialSummon);
            ReadCardList(PlayerIdleAction.ChangeCardPosition);
            ReadCardList(PlayerIdleAction.Set);
            ReadCardList(PlayerIdleAction.SpellOrTrapSet);
            ReadCardList(PlayerIdleAction.EffectActivation);

            // fases
            if (reader.ReadByte() == 1)
            {
                choices.Add(new IdleCmdToBattlePhase(PlayerIdleAction.GoToBattlePhase, 0));
            }

            if (reader.ReadByte() == 1)
            {
                choices.Add(new IdleCmdToEndPhase(PlayerIdleAction.GotoEndPhase, 0));
            }

            if (reader.ReadByte() == 1)
                choices.Add(new IdleCmdShuffleHand(PlayerIdleAction.ShuffleHand, 0));

            return new SelectIdleCmdMessage(player, choices);
        }
    }
}