using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Ygo.Controller.Component;
using Ygo.Controller.Field;
using Ygo.Core.Duel;
using Ygo.Scripts.Core.Event;

namespace Ygo.Controller
{
    public class IdleCommandController : MonoBehaviour
    {
        [Header("Buttons")] 
        [field: SerializeField]
        private ButtonController battlePhaseButton;
        [field: SerializeField]
        private ButtonController endPhaseButton;
        [field: SerializeField] 
        private ButtonController shuffleHandButton;

        private DuelInstance _duelInstance;
        private HandController[] _handControllers;
        private FieldController _fieldController;
        private DeckController[] _deckControllers;
        
        public void Init(
            DuelInstance duelInstance,
            HandController[] handControllers, 
            FieldController fieldController,
            DeckController[] deckControllers
            )
        {
            _duelInstance = duelInstance;
            _handControllers = handControllers;
            _fieldController = fieldController;
            _deckControllers = deckControllers;
            
            _duelInstance.EventQueue.Subscribe<SelectIdleCommandEvent>(OnSelectIdleCommandEvent);
            battlePhaseButton.Init(() => { }, "Battle\nPhase");
            battlePhaseButton.Disable(true);
            endPhaseButton.Init(() => {}, "End\nPhase");
            endPhaseButton.Disable(true);
            shuffleHandButton.Init(() => {}, "Shuffle\nHands");
            shuffleHandButton.Disable(true);
        }

        private UniTask OnSelectIdleCommandEvent(SelectIdleCommandEvent e)
        {
            if(e.HasBattlePhase)
                battlePhaseButton.Init(() => {SelectCommand(e.BattlePhaseIndex);}, "Battle\nPhase");
            if(e.HasEndPhase)
                endPhaseButton.Init(() => {SelectCommand(e.EndPhaseIndex);}, "End\nPhase");
            if(e.HasShuffleHand)
                shuffleHandButton.Init(() => {SelectCommand(e.ShuffleHandIndex);}, "Shuffle\nHands");
            
            return UniTask.CompletedTask;
        }

        private void SelectCommand(int value)
        {
            battlePhaseButton.Disable(true);
            endPhaseButton.Disable(true);
            shuffleHandButton.Disable(true);
            _ = _duelInstance.SetResponse(new List<int>() { value });
        }
    }
}