using UnityEngine;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Api;

namespace Ygo.Core.Bridge
{
    public class DuelBridge
    {
        private IDuelManager _duelManager; 
        public void Start()
        {
            _duelManager = YgoEdo.Init($"{Application.streamingAssetsPath}/");
            
            var result = _duelManager.CreateDuel();

            if (!result)
            {
                Debug.Log("Failed to set create duel.");
                return;
            }

            var duel = _duelManager.CurrentDuel;
            result = duel.SetupDuelOptions(
                DuelMode.MasterRule5,
                8000,
                5,
                1,
                8000,
                5,
                1);

            if (!result)
            {
                Debug.Log("Failed to set duel options.");
                return;
            }

            result = duel.InitDuel();

            if (!result)
            {
                Debug.Log("Failed to init duel.");
                return;
            }

            result = duel.SetDecks(
                DummyDeck.CreateDeck(0, true, false, _duelManager.CardLibrary),
                DummyDeck.CreateDeck(0, false, true, _duelManager.CardLibrary),
                DummyDeck.CreateDeck(1, true, false, _duelManager.CardLibrary),
                DummyDeck.CreateDeck(1, false, true, _duelManager.CardLibrary)
            );

            if (!result)
            {
                Debug.Log("Failed to set deck.");
                return;
            }

            result = duel.StartDuel();


            if (!result)
            {
                Debug.Log("Failed to start duel.");
                return;
            }
            
            RunDuel();
        }

        private void RunDuel()
        {
            Debug.Log("Starting the duel.");
            var result = true;
            while (_duelManager.CurrentDuel.State is not DuelState.Destroyed and not DuelState.DuelFinished)
            {
                if (result == false)
                    break;

                if (_duelManager.CurrentDuel.Messages.Count <= 0)
                {
                    result = _duelManager.CurrentDuel.ProceedDuel();
                    continue;
                }

                CheckMessage(_duelManager.CurrentDuel.Messages[_duelManager.CurrentDuel.CurrentMessageIndex]);

                if (!_duelManager.CurrentDuel.NextMessage())
                    result = _duelManager.CurrentDuel.ProceedDuel();
            }

            Debug.Log("Duel Finished!");
        }
        
        private static void CheckMessage(IDuelMessage message)
        {
            Debug.Log(message.ToString());
        }
    }
}