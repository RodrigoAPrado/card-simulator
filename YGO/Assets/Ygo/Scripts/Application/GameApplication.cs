using System;
using Ygo.Core.Duel;
using Ygo.Scripts.Data;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Api;

namespace Ygo.Application
{
    public class GameApplication
    {
        private IDuelManager _duelManager;
        private DuelInstance _duelInstance;

        public void Setup()
        {
            _duelManager = YgoEdo.Init(UnityEngine.Application.streamingAssetsPath + "/");
        }

        public DuelInstance Init()
        {
            if (_duelInstance != null)
                return _duelInstance;
            
            var duelist0 = DuelistData.CreateBuilder()
                .WithStartingLp(8000)
                .WithStartingHand(5)
                .WithCardsPerDraw(1)
                .WithMainDeck(DummyDeck.CreateDeck(0, true, false, _duelManager.CardLibrary))
                .WithExtraDeck(DummyDeck.CreateDeck(0, false, true, _duelManager.CardLibrary))
                .Build();
            
            var duelist1 = DuelistData.CreateBuilder()
                .WithStartingLp(8000)
                .WithStartingHand(5)
                .WithCardsPerDraw(1)
                .WithMainDeck(DummyDeck.CreateDeck(1, true, false, _duelManager.CardLibrary))
                .WithExtraDeck(DummyDeck.CreateDeck(1, false, true, _duelManager.CardLibrary))
                .Build();
            
            _duelInstance = new DuelInstance(_duelManager);
            var result = _duelInstance.StartDuel(DuelData.CreateBuilder().WithDuelist0(duelist0).WithDuelist1(duelist1)
                .WithDuelMode(DuelMode.MasterRule5).Build());

            if (!result)
                throw new Exception("Duel could not start!");

            return _duelInstance;
        }
    }
}