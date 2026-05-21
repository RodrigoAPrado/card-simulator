using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Ygo.Scripts.Data;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;

namespace Ygo.Core.Duel
{
    public class DuelBridge
    {
        private readonly IDuelManager _duelManager;
        private DuelData _duelData;
        
        public DuelBridge(IDuelManager duelManager)
        {
            _duelManager = duelManager;
        }
        
        public bool StartDuel(DuelData duelData)
        {
            if (_duelData != null)
            {
                return false;
            }
            _duelManager.DisposeDuel();
            var result = _duelManager.CreateDuel();

            if (!result)
            {
                Debug.Log("Failed to set create duel.");
                return false;
            }

            var duel = _duelManager.CurrentDuel;
            
            result = duel.SetupDuelOptions(
                duelData.DuelMode,
                duelData.Duelist0.StartingLp,
                duelData.Duelist0.StartingHand,
                duelData.Duelist0.Draw,
                duelData.Duelist1.StartingLp,
                duelData.Duelist1.StartingHand,
                duelData.Duelist1.Draw);

            if (!result)
            {
                Debug.Log("Failed to set duel options.");
                return false;
            }

            result = duel.InitDuel();

            if (!result)
            {
                Debug.Log("Failed to init duel.");
                return false;
            }

            result = duel.SetDecks(duelData.Duelist0.MainDeck, duelData.Duelist0.ExtraDeck, duelData.Duelist1.MainDeck,
                duelData.Duelist1.ExtraDeck);

            if (!result)
            {
                Debug.Log("Failed to set deck.");
                return false;
            }

            result = duel.StartDuel();


            if (!result)
            {
                Debug.Log("Failed to start duel.");
                return false;
            }
            
            _duelData = duelData;
            return true;
        }

        public bool ProceedDuel()
        {
            return _duelManager.CurrentDuel.ProceedDuel();
        }

        public IDuelMessage GetMessage()
        {
            if (_duelManager.CurrentDuel.Messages.Count <= 0)
                return null;
            return _duelManager.CurrentDuel.Messages[_duelManager.CurrentDuel.CurrentMessageIndex];
        }

        public bool NextMessage()
        {
            return _duelManager.CurrentDuel.NextMessage();
        }

        public bool SetResponse(List<int> response)
        {
            if (response.Count == 1 && response[0] == -1)
            {
                return _duelManager.CurrentDuel.SetCancel();
            }
            return _duelManager.CurrentDuel.SetResponse(response);
        }
    }
}