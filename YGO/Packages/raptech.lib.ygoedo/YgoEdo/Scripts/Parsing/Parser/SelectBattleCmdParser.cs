using System.Collections.Generic;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component.Command.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component.Command.Battle;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser
{
    public class SelectBattleCmdParser : BaseParser
    {
        protected override IOcgMessage DoParse(byte[] buffer)
        {
            var reader = new PacketReader(buffer);
            reader.ReadByte(); //msg
            var player = reader.ReadByte();

            var choices = new List<BattleCmdChoice>();

            void AddBattleCmdChoice(PlayerBattleAction action)
            {
                var count = reader.ReadUInt32();
                uint index = 0;
                for (var i = count; i > 0; i--)
                {
                    var cardCode = reader.ReadUInt32();
                    var controller = reader.ReadByte();
                    var location = (OCG_CardLocation)reader.ReadByte();
                    if (action == PlayerBattleAction.ActivateEffect)
                    {
                        var sequence = reader.ReadUInt32();
                        var description = reader.ReadULong64();
                        reader.Skip(1); // client mode
                        choices.Add(new BattleCmdEffectChoice(action, index, cardCode, controller, location, sequence,
                            description));
                    }
                    else
                    {
                        uint sequence = reader.ReadByte();
                        var directAttack = reader.ReadByte() == 1;
                        choices.Add(new BattleCmdAttackChoice(action, index, cardCode, controller, location, sequence,
                            directAttack));
                    }

                    index++;
                }
            }

            AddBattleCmdChoice(PlayerBattleAction.ActivateEffect);
            AddBattleCmdChoice(PlayerBattleAction.Attack);

            var toMainPhase2 = reader.ReadByte() == 1;
            if (toMainPhase2)
                choices.Add(new BattleCmdToMain2Choice(PlayerBattleAction.GoToMainPhase2, 0));
            var toEndPhase = reader.ReadByte() == 1;
            if (toEndPhase)
                choices.Add(new BattleCmdToEndPhaseChoice(PlayerBattleAction.GoToEndPhase, 0));

            return new SelectBattleCmdMessage(player, choices);
        }
    }
}