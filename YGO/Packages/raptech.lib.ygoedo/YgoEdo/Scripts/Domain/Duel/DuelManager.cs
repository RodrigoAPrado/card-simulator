using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Constant;
using YgoSoul.RapTech.Lib.YgoEdo.Domain.Bridge;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser.Abstr;

namespace YgoSoul.RapTech.Lib.YgoEdo.Domain.Duel
{
    public class DuelManager : IDuelManager
    {
        private Duel _ocgDuel;
        private readonly Dictionary<OCG_GameMessage, IParser> _parsers;

        public DuelManager(ICardLibrary cardLibrary, Dictionary<OCG_GameMessage, IParser> parsers)
        {
            CardLibrary = cardLibrary;
            _parsers = parsers;
        }

        public ICardLibrary CardLibrary { get; }
        public IDuel CurrentDuel => _ocgDuel;

        public bool CreateDuel()
        {
            if (_ocgDuel != null)
                return false;
            _ocgDuel = new Duel(ParseMessages);
            return true;
        }

        public bool DisposeDuel()
        {
            if (_ocgDuel == null)
                return false;

            _ocgDuel.Dispose();
            _ocgDuel = null;
            return true;
        }

        private bool ParseMessages(OcgResponse response)
        {
            var buffer = new byte[response.Length];
            Marshal.Copy(response.Message, buffer, 0, (int)response.Length);

            var offset = 0;
            var messages = new List<IOcgMessage>();

            while (offset < buffer.Length)
            {
                var msgSize = BitConverter.ToInt32(buffer, offset);
                offset += 4;

                var msgData = new byte[msgSize];
                Array.Copy(buffer, offset, msgData, 0, msgSize);
                offset += msgSize;

                messages.Add(ParseSingleMessage(msgData));
            }

            return _ocgDuel != null && _ocgDuel.SetNewMessages(messages);
        }

        private IOcgMessage ParseSingleMessage(byte[] buffer)
        {
            var msgType = (OCG_GameMessage)buffer[0];
            _parsers.TryGetValue(msgType, out var parser);
            if (parser == null)
                throw new InvalidOperationException();
            return parser.Parse(buffer);
        }
    }
}