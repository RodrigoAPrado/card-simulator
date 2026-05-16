using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.System.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Native;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Struct;
using YgoSoul.RapTech.Lib.YgoEdo.Domain.Bridge;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Domain.Duel
{
    public class Duel : IDuel, IDisposable
    {
        private readonly List<IOcgMessage> _messages;

        private readonly Func<OcgResponse, bool> _processMessage;
        private OCG_DuelOptions _options;
        private IntPtr _pDuel;

        public Duel(Func<OcgResponse, bool> processMessage)
        {
            _processMessage = processMessage;
            _messages = new List<IOcgMessage>();
            CurrentMessageIndex = 0;
            RetryCount = 0;
        }

        public void Dispose()
        {
            OCG_Api.Setup.OCG_DestroyDuel(_pDuel);
            _messages.Clear();
            _options = default;
            State = DuelState.Destroyed;
        }

        public DuelState State { get; private set; } = DuelState.NotInitiated;

        public IReadOnlyList<IDuelMessage> Messages => _messages;
        public int CurrentMessageIndex { get; private set; }
        public int RetryCount { get; private set; }

        public Tuple<int, int> GetOcgVersion()
        {
            OCG_Api.Info.OCG_GetVersion(out var major, out var minor);
            return new Tuple<int, int>(major, minor);
        }

        public bool SetupDuelOptions(
            DuelMode duelMode,
            uint player1Lp,
            uint player1Hand,
            uint player1Draw,
            uint player2Lp,
            uint player2Hand,
            uint player2Draw
        )
        {
            if (State != DuelState.NotInitiated)
            {
                Console.WriteLine($"Current state is: {State}.");
                return false;
            }

            _options = new OCG_DuelOptions
            {
                seed0 = 0x12345,
                flags = (ulong)duelMode.FromDuelMode(),
                team1 = new OCG_Player
                {
                    startingLP = player1Lp,
                    startingDrawCount = player1Hand,
                    drawCountPerTurn = player1Draw
                },
                team2 = new OCG_Player
                {
                    startingLP = player2Lp,
                    startingDrawCount = player2Hand,
                    drawCountPerTurn = player2Draw
                },
                cardReader = Marshal.GetFunctionPointerForDelegate(OcgBridge.CardReader),
                scriptReader = Marshal.GetFunctionPointerForDelegate(OcgBridge.ScriptReader),
                logHandler = Marshal.GetFunctionPointerForDelegate(OcgBridge.LogHandler),
                cardReaderDone = Marshal.GetFunctionPointerForDelegate(OcgBridge.ReaderDone),
                enableUnsafeLibraries = 0
            };
            State = DuelState.DuelOptionsSet;
            return true;
        }

        public bool InitDuel()
        {
            if (State != DuelState.DuelOptionsSet)
            {
                Console.WriteLine($"Current state is: {State}.");
                return false;
            }

            if (OCG_Api.Setup.OCG_CreateDuel(out var pDuel, ref _options) != 0)
                throw new Exception("Failed to create duel.");

            _pDuel = pDuel;
            LoadBaseScripts();
            State = DuelState.DuelCreated;
            return true;
        }

        public bool SetDecks(
            IReadOnlyList<ICardData> mainDeck0,
            IReadOnlyList<ICardData> extraDeck0,
            IReadOnlyList<ICardData> mainDeck1,
            IReadOnlyList<ICardData> extraDeck1
        )
        {
            if (State != DuelState.DuelCreated)
            {
                Console.WriteLine($"Current state is: {State}.");
                return false;
            }

            SetDeck(mainDeck0, false, 0);
            SetDeck(extraDeck0, true, 0);
            SetDeck(mainDeck1, false, 1);
            SetDeck(extraDeck1, true, 1);
            State = DuelState.DecksSet;
            return true;
        }

        public bool StartDuel()
        {
            if (State != DuelState.DecksSet)
            {
                Console.WriteLine($"Current state is: {State}.");
                return false;
            }

            OCG_Api.Setup.OCG_StartDuel(_pDuel);
            State = DuelState.DuelReady;
            return true;
        }

        public bool ProceedDuel()
        {
            if (State != DuelState.DuelReady)
            {
                Console.WriteLine($"Current state is: {State}.");
                return false;
            }

            var result = (OCG_DuelStatus)OCG_Api.Run.OCG_DuelProcess(_pDuel);

            State = result switch
            {
                OCG_DuelStatus.OcgDuelStatusEnd => DuelState.DuelFinished,
                OCG_DuelStatus.OcgDuelStatusAwating => DuelState.WaitingInput,
                OCG_DuelStatus.OcgDuelStatusContinue => DuelState.DuelReady,
                _ => throw new ArgumentOutOfRangeException()
            };

            var messagePtr = OCG_Api.Run.OCG_DuelGetMessage(_pDuel, out var length);

            return _processMessage.Invoke(new OcgResponse(messagePtr, length));
        }

        public bool SetResponse(List<int> playerInput)
        {
            if (State != DuelState.WaitingInput)
            {
                Console.WriteLine($"Current state is: {State}.");
                return false;
            }

            if (_messages.Count == 0)
                return false;

            var currentMessage = _messages[CurrentMessageIndex];

            if (currentMessage.Input is InputType.None or InputType.Unknown or InputType.Win) return false;

            var response = currentMessage.GetResponse(playerInput);

            OCG_Api.Run.OCG_DuelSetResponse(_pDuel, response, (uint)response.Length);

            State = DuelState.DuelReady;
            return true;
        }

        public bool SetCancel()
        {
            if (State != DuelState.WaitingInput)
            {
                Console.WriteLine($"Current state is: {State}.");
                return false;
            }

            if (_messages.Count == 0)
                return false;

            var currentMessage = _messages[CurrentMessageIndex];

            if (currentMessage is not ISelectionOcgMessage { CanCancel: true } selectionOcgMessage)
                return false;

            var response = selectionOcgMessage.Cancel();

            OCG_Api.Run.OCG_DuelSetResponse(_pDuel, response, (uint)response.Length);

            State = DuelState.DuelReady;

            return true;
        }

        public bool NextMessage()
        {
            if (_messages.Count == 0)
                return false;

            if (_messages[CurrentMessageIndex].Input is not InputType.None)
                return false;

            if (CurrentMessageIndex + 1 >= _messages.Count)
                return false;

            CurrentMessageIndex++;


            return true;
        }

        public bool SetNewMessages(List<IOcgMessage> messages)
        {
            if (messages.Count == 1)
            {
                if (messages[0].Input == InputType.Retry)
                {
                    RetryCount++;
                    return false;
                }
            }

            _messages.Clear();
            _messages.AddRange(messages);
            CurrentMessageIndex = 0;
            RetryCount = 0;
            return true;
        }

        private void SetDeck(IReadOnlyList<ICardData> deck, bool isExtra, byte team)
        {
            foreach (var card in deck)
            {
                var ocgNewCardInfo = GetNewCardInfo(card, isExtra, team);
                OCG_Api.Setup.OCG_DuelNewCard(_pDuel, ref ocgNewCardInfo);
            }
        }

        private OCG_NewCardInfo GetNewCardInfo(ICardData cardData, bool isExtra, byte team)
        {
            return new OCG_NewCardInfo
            {
                team = team,
                duelist = 0,
                code = cardData.Code,
                con = team,
                loc = (uint)(isExtra ? OCG_CardLocation.Extra : OCG_CardLocation.Deck),
                pos = (uint)OCG_CardPosition.FaceDown
            };
        }

        private void LoadBaseScripts()
        {
            OcgBridge.LoadScript(_pDuel, "constant.lua");
            OcgBridge.LoadScript(_pDuel, "utility.lua");
            OcgBridge.LoadScript(_pDuel, "rankup_functions.lua");
        }
    }
}