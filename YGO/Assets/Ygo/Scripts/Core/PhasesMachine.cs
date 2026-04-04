using System;
using UnityEngine;
using Ygo.Core.Phases;
using Ygo.Core.Phases.Abstract;

namespace Ygo.Core
{
    public class PhasesMachine
    {
        public IGamePhase CurrentPhase { get; private set; }
        private IGamePhase _firstGamePhase;
        private CardsHandler _cardsHandler;

        public event Action PhaseChange;
        
        public void Setup(CardsHandler cardsHandler)
        {
            _firstGamePhase = new EndPhase(null, cardsHandler, AdvancePhase);
            CurrentPhase = new MainPhase2(_firstGamePhase, cardsHandler, AdvancePhase);
            _firstGamePhase = CurrentPhase;
            CurrentPhase = new BattlePhase(_firstGamePhase, cardsHandler, AdvancePhase);
            _firstGamePhase = CurrentPhase;
            CurrentPhase = new MainPhase1(_firstGamePhase, cardsHandler, AdvancePhase);
            _firstGamePhase = CurrentPhase;
            CurrentPhase = new StandbyPhase(_firstGamePhase, cardsHandler, AdvancePhase);
            _firstGamePhase = CurrentPhase;
            CurrentPhase = new DrawPhase(_firstGamePhase, cardsHandler, AdvancePhase);
            _firstGamePhase = CurrentPhase;
        }

        public void Init()
        {
            CurrentPhase.Init();
        }

        public void SubscribeToPhaseChange(Action action)
        {
            PhaseChange += action;
        }

        public void UnsubscribeToPhaseChange(Action action)
        {
            PhaseChange -= action;
        }

        private void AdvancePhase()
        {
            CurrentPhase = CurrentPhase.NextPhase;
            PhaseChange?.Invoke();
            CurrentPhase.Init();
        }
    }
}