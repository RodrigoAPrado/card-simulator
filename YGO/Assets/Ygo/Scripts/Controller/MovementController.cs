using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Ygo.Controller.Card;
using Ygo.Controller.Field;
using Ygo.Core.Duel;
using Ygo.Scripts.Core.Enum;
using Ygo.Scripts.Core.Event;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Flag;

namespace Ygo.Controller
{
    public class MovementController : MonoBehaviour
    {
        [field: SerializeField]
        private AnimatingCardController animatingCardCanvas;
        [field: SerializeField]
        private Transform neutralPositionCanvasPlayer;
        [field: SerializeField]
        private Transform neutralPositionCanvasOpponent;
        [field: SerializeField]
        private AnimatingCardController animatingCardWorld;
        [field: SerializeField]
        private Transform neutralPositionWorldPlayer;
        [field: SerializeField]
        private Transform neutralPositionWorldOpponent;
        
        private HandController[] _handControllers;
        private FieldController _fieldController;
        private DeckController[] _deckControllers;
        private DuelInstance _duelInstance;
        
        public void Init(
            DuelInstance duelInstance,
            HandController[] handControllers, 
            FieldController fieldController,
            DeckController[] deckControllers)
        {
            _duelInstance = duelInstance;
            _handControllers = handControllers;
            _fieldController = fieldController;
            _deckControllers = deckControllers;
            
            _duelInstance.EventQueue.Subscribe<MoveEvent>(OnMove);
            animatingCardCanvas.Init();
            animatingCardWorld.Init();
        }

        private async UniTask OnMove(MoveEvent e)
        {
            if (e.BeginLocation == Location.Hand || e.EndLocation == Location.Hand)
            {
                await OnMoveHand(e);
                return;
            }
            
            
        }

        private async UniTask OnMoveHand(MoveEvent e)
        {
            if (e.BeginLocation == Location.Hand)
            {
                await OnMoveHandBegin(e);
            }
        }

        private async UniTask OnMoveHandBegin(MoveEvent e)
        {
            var handController = _handControllers.FirstOrDefault(x => x.PointOfView == e.BeginPointOfView);
            var neutralPosition = e.BeginPointOfView == PointOfView.Player
                ? neutralPositionCanvasPlayer
                : neutralPositionCanvasOpponent;
            
            if (handController == null)
                throw new InvalidOperationException("No hand controller found");
            
            await handController.MoveCardAway(neutralPosition, animatingCardCanvas, e.CardModel);
        }
    }
}