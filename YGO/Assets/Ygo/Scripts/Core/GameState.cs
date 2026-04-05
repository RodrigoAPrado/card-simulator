using System;
using Ygo.Core.Phases;
using Ygo.Core.Phases.Abstract;

namespace Ygo.Core
{
    public class GameState
    {
        public TurnContext TurnContext { get; private set; }
        public IGamePhase CurrentPhase { get; private set; }
        private IGamePhase _firstGamePhase;

        public event Action PhaseChange;
        
        public void Setup(TurnContext turnContext)
        {
            TurnContext = turnContext;
            _firstGamePhase = new EndPhase(null, AdvancePhase);
            CurrentPhase = new MainPhase2(_firstGamePhase, AdvancePhase);
            _firstGamePhase = CurrentPhase;
            CurrentPhase = new BattlePhase(_firstGamePhase, AdvancePhase);
            _firstGamePhase = CurrentPhase;
            CurrentPhase = new MainPhase1(_firstGamePhase, AdvancePhase);
            _firstGamePhase = CurrentPhase;
            CurrentPhase = new StandbyPhase(_firstGamePhase, AdvancePhase);
            _firstGamePhase = CurrentPhase;
            CurrentPhase = new DrawPhase(_firstGamePhase, AdvancePhase);
            _firstGamePhase = CurrentPhase;
        }

        public void Init()
        {
            CurrentPhase.Init(TurnContext);
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
            CurrentPhase.Init(TurnContext);
        }
    }
}