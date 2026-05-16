using YgoSoul.RapTech.Lib.YgoEdo.Core.Constant;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser
{
    public class BasicParser : BaseParser
    {
        protected override IOcgMessage DoParse(byte[] buffer)
        {
            var reader = new PacketReader(buffer);
            var msgType = (OCG_GameMessage)reader.ReadByte();
            switch (msgType)
            {
                case OCG_GameMessage.NewTurn:
                    return new NewTurnMessage(reader.ReadByte());
                case OCG_GameMessage.NewPhase:
                    return new NewPhaseMessage((OCG_GamePhases)reader.ReadByte());
                case OCG_GameMessage.ShuffleDeck:
                    return new ShuffleDeckMessage(reader.ReadByte());
                case OCG_GameMessage.ReverseDeck:
                    return new ReverseDeckMessage();
                case OCG_GameMessage.Swap:
                    return new SwapMessage(new CardReference(
                        reader.ReadUInt32(),
                        new FullLocationReference(reader.ReadByte(),
                            (OCG_CardLocation)reader.ReadByte(),
                            reader.ReadUInt32(),
                            (OCG_CardPosition)reader.ReadUInt32()),
                        0
                    ), new CardReference(
                        reader.ReadUInt32(),
                        new FullLocationReference(reader.ReadByte(),
                            (OCG_CardLocation)reader.ReadByte(),
                            reader.ReadUInt32(),
                            (OCG_CardPosition)reader.ReadUInt32()),
                        0
                    ));
                case OCG_GameMessage.Summoned:
                    return new SummonedMessage();
                case OCG_GameMessage.SpSummoned:
                    return new SpecialSummonedMessage();
                case OCG_GameMessage.FlipSummoned:
                    return new FlipSummonedMessage();
                case OCG_GameMessage.Chained:
                    return new ChainedMessage(reader.ReadByte());
                case OCG_GameMessage.ChainSolving:
                    return new ChainSolvingMessage(reader.ReadByte());
                case OCG_GameMessage.ChainSolved:
                    return new ChainSolvedMessage(reader.ReadByte());
                case OCG_GameMessage.ChainEnd:
                    return new ChainEndMessage("Chain End.");
                case OCG_GameMessage.ChainNegated:
                    return new ChainNegatedMessage(reader.ReadByte());
                case OCG_GameMessage.ChainDisabled:
                    return new ChainDisabledMessage(reader.ReadByte());
                case OCG_GameMessage.DeckTop:
                    var player = reader.ReadByte();
                    reader.ReadUInt32(); //vazio
                    return new DeckTopMessage(player, reader.ReadUInt32(), (OCG_CardPosition)reader.ReadUInt32());
                case OCG_GameMessage.PosChange:
                    return new PosChangeMessage(
                        reader.ReadUInt32(),
                        reader.ReadByte(),
                        (OCG_CardLocation)reader.ReadByte(),
                        reader.ReadByte(),
                        (OCG_CardPosition)reader.ReadByte(),
                        (OCG_CardPosition)reader.ReadByte());
                case OCG_GameMessage.Damage:
                    return new DamageMessage(reader.ReadByte(), reader.ReadUInt32());
                case OCG_GameMessage.Recover:
                    return new RecoverMessage(reader.ReadByte(), reader.ReadUInt32());
                case OCG_GameMessage.Equip:
                    return new EquipMessage(new FullLocationReference(
                            reader.ReadByte(),
                            (OCG_CardLocation)reader.ReadByte(),
                            reader.ReadUInt32(),
                            (OCG_CardPosition)reader.ReadUInt32()),
                        new FullLocationReference(
                            reader.ReadByte(),
                            (OCG_CardLocation)reader.ReadByte(),
                            reader.ReadUInt32(),
                            (OCG_CardPosition)reader.ReadUInt32()));
                case OCG_GameMessage.LpUpdate:
                    return new LpUpdateMessage(reader.ReadByte(), reader.ReadUInt32());
                case OCG_GameMessage.CardTarget:
                    return new CardTargetMessage(new FullLocationReference(
                            reader.ReadByte(),
                            (OCG_CardLocation)reader.ReadByte(),
                            reader.ReadUInt32(),
                            (OCG_CardPosition)reader.ReadUInt32()),
                        new FullLocationReference(
                            reader.ReadByte(),
                            (OCG_CardLocation)reader.ReadByte(),
                            reader.ReadUInt32(),
                            (OCG_CardPosition)reader.ReadUInt32()));
                case OCG_GameMessage.CancelTarget:
                    return new CancelTargetMessage(new FullLocationReference(
                            reader.ReadByte(),
                            (OCG_CardLocation)reader.ReadByte(),
                            reader.ReadUInt32(),
                            (OCG_CardPosition)reader.ReadUInt32()),
                        new FullLocationReference(
                            reader.ReadByte(),
                            (OCG_CardLocation)reader.ReadByte(),
                            reader.ReadUInt32(),
                            (OCG_CardPosition)reader.ReadUInt32()));
                case OCG_GameMessage.PayLpCost:
                    return new PayLpCostMessage(reader.ReadByte(), reader.ReadUInt32());
                case OCG_GameMessage.AttackDisabled:
                    return new AttackDisabledMessage();
                case OCG_GameMessage.DamageStepStart:
                    return new DamageStepStartMessage();
                case OCG_GameMessage.DamageStepEnd:
                    return new DamageStepEndMessage();
                case OCG_GameMessage.MissedEffect:
                    return new MissedEffectMessage(new FullLocationReference(
                            reader.ReadByte(),
                            (OCG_CardLocation)reader.ReadByte(),
                            reader.ReadUInt32(),
                            (OCG_CardPosition)reader.ReadUInt32()),
                        reader.ReadUInt32()
                    );
                case OCG_GameMessage.CardHint:
                    return new CardHintMessage(new FullLocationReference(
                            reader.ReadByte(),
                            (OCG_CardLocation)reader.ReadByte(),
                            reader.ReadUInt32(),
                            (OCG_CardPosition)reader.ReadUInt32()),
                        (OCG_CardHint)reader.ReadByte(),
                        reader.ReadULong64());
                case OCG_GameMessage.MatchKill:
                    return new MatchKillMessage(reader.ReadUInt32());
                default:
                    return new UnknownMessage(buffer);
            }
        }
    }
}