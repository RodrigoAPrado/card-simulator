using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Ygo.Controller.Card;
using Ygo.Controller.Data;
using Ygo.Controller.Field;
using Ygo.Core.Duel;
using Ygo.Scripts.Core.Enum;
using Ygo.Scripts.Core.Event;
using Ygo.Scripts.Core.Event.Base;
using Ygo.Scripts.Core.Model;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Flag;

namespace Ygo.Controller
{
    public class FieldController : MonoBehaviour
    {
        [field:SerializeField]
        private FieldZoneController[] fieldZones;
        [field:SerializeField]
        private GraveyardController[] graveyards;
        
        private IReadOnlyDictionary<PointOfView, IReadOnlyDictionary<FieldZones, FieldZoneController>> _fieldZonesDict;
        private IReadOnlyDictionary<PointOfView, GraveyardController> _graveyardsDict;
        
        private DuelInstance _duelInstance;
        private bool _showOpponent;
        private CardImageLibrary _library;
        
        public void Init(DuelInstance duelInstance, CardImageLibrary library, bool showOpponent = true)
        {
            var fieldZonesDict = new Dictionary<PointOfView, Dictionary<FieldZones, FieldZoneController>>();
            
            foreach (var fieldZone in fieldZones)
            {
                fieldZone.Init(library);
                fieldZonesDict.TryGetValue(fieldZone.PointOfView, out var pointOfViewDict);
                if (pointOfViewDict == null)
                {
                    pointOfViewDict = new Dictionary<FieldZones, FieldZoneController>();
                    fieldZonesDict.Add(fieldZone.PointOfView, pointOfViewDict);
                }

                if (!pointOfViewDict.TryAdd(fieldZone.FieldZone, fieldZone))
                    throw new InvalidOperationException($"Field {fieldZone.PointOfView}/{fieldZone.FieldZone} " +
                                                        $"zone already exists!");
            }

            _fieldZonesDict = fieldZonesDict
                .Select(x => x)
                .ToDictionary(x => x.Key,
                x => (IReadOnlyDictionary<FieldZones, FieldZoneController>)x.Value);
            
            var graveyardsDict = new Dictionary<PointOfView, GraveyardController>();
            
            foreach (var graveyard in graveyards)
            {
                graveyard.Init(library);
                graveyardsDict.TryGetValue(graveyard.PointOfView, out var graveyardController);
                if (graveyardController == null)
                {
                    graveyardsDict.Add(graveyard.PointOfView, graveyard);
                }
            }
            
            _graveyardsDict = graveyardsDict;
            
            duelInstance.EventQueue.Subscribe<SelectPlaceEvent>(OnSelectPlaceEvent);
            _showOpponent = showOpponent;
            _duelInstance = duelInstance;
            _library = library;
        }

        private UniTask OnSelectPlaceEvent(SelectPlaceEvent e)
        {
            if (e.PointOfView == PointOfView.Opponent && !_showOpponent)
                return UniTask.CompletedTask; 
            
            var opponentPointOfView = e.PointOfView == PointOfView.Player ? PointOfView.Opponent : PointOfView.Player;

            var choiceIndex = 0;
            foreach (var choice in e.Choices)
            {
                var pointOfView = (int) choice >= 100 ? opponentPointOfView : e.PointOfView;
                var actualChoice = (int) choice >= 100 ? choice - 100 : choice;
                var fieldZone = _fieldZonesDict[pointOfView][actualChoice];
                var actualChoiceIndex = choiceIndex;
                fieldZone.ToggleHighlight(true);
                fieldZone.SetAction(() => ConfirmSelection(actualChoiceIndex));
                choiceIndex++;
            }

            return UniTask.CompletedTask;
        }

        private void ConfirmSelection(int index)
        {
            foreach (var field in fieldZones)
            {
                field.ToggleHighlight(false);
                field.ClearAction();
            }

            _ = _duelInstance.SetResponse(new List<int>() { index });
        }

        public async UniTask MoveCardFromHandToFieldZone(RectTransform originalPosition, FieldZones toFieldZone, 
            AnimatingCardController animatingCard, CardModel card, PointOfView pointOfView)
        {
            animatingCard.Show(_library.GetCardImage(card.Data.Code));
            animatingCard.transform.position = new Vector3(originalPosition.transform.position.x,
                originalPosition.transform.position.y, originalPosition.transform.position.z);
            var animatingCardRect = animatingCard.GetComponent<RectTransform>();

            animatingCardRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalPosition.rect.width);
            animatingCardRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, originalPosition.rect.height);
            
            var fieldZone = _fieldZonesDict[pointOfView][toFieldZone];
            await animatingCard.MoveCardField(fieldZone.Content.transform, animatingCardRect,
                fieldZone.Content);
            fieldZone.InitCard(card);
            animatingCard.Hide();
            await UniTask.Delay(30);
        }

        public UniTask MoveCardFromHandToFieldArea(RectTransform originalPosition, Location cardLocation, 
            AnimatingCardController animatingCard, CardModel card, PointOfView pointOfView)
        {
            switch (cardLocation)
            {
                case Location.Deck:
                    throw new NotImplementedException();
                case Location.Grave:
                    return MoveCardFromHandToGraveyard(originalPosition, animatingCard, card, pointOfView);
                case Location.Banishment:
                    throw new NotImplementedException();
                case Location.Extra:
                    throw new NotImplementedException();
                case Location.Overlay:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException(nameof(cardLocation), cardLocation, null);
            }
        }

        private async UniTask MoveCardFromHandToGraveyard(RectTransform originalPosition, 
            AnimatingCardController animatingCard, CardModel card, PointOfView pointOfView)
        {
            animatingCard.Show(_library.GetCardImage(card.Data.Code));
            animatingCard.transform.position = new Vector3(originalPosition.transform.position.x,
                originalPosition.transform.position.y, originalPosition.transform.position.z);
            var animatingCardRect = animatingCard.GetComponent<RectTransform>();

            animatingCardRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalPosition.rect.width);
            animatingCardRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, originalPosition.rect.height);
            
            var targetLocation = _graveyardsDict[pointOfView];
            await animatingCard.MoveCardField(targetLocation.Content.transform, animatingCardRect,
                targetLocation.Content);
            
            targetLocation.InitCard(card);
            animatingCard.Hide();
            await UniTask.Delay(30);
        }

        public async UniTask MoveCardFromZoneToAnother(AnimatingCardController animatingCard, MoveEvent e)
        {
            var beginLocation = GetLocationToMove(e.BeginLocation, e.BeginPointOfView, e.BeginFieldZone, true);

            animatingCard.Show(_library.GetCardImage(e.CardModel.Data.Code));
            animatingCard.transform.position = new Vector3(beginLocation.transform.position.x,
                beginLocation.transform.position.y, beginLocation.transform.position.z);
            
            animatingCard.transform.rotation = beginLocation.transform.rotation;
            
            var endLocation = GetLocationToMove(e.EndLocation, e.EndPointOfView, e.EndFieldZone);
            
            await animatingCard.MoveCardFieldOnly(endLocation.transform);

            InitZone(e.CardModel, e.EndLocation, e.EndPointOfView, e.EndFieldZone);
            animatingCard.Hide();
            await UniTask.Delay(30);
        }

        private Transform GetLocationToMove(Location location, PointOfView pointOfView, FieldZones zone, bool clear = false)
        {
            Transform locationToMove;
            switch(location)
            {
                case Location.Deck:
                    throw new NotImplementedException();
                case Location.MonsterZone:
                case Location.SpellTrapZone:
                    var zoneController = _fieldZonesDict[pointOfView][zone];
                    if(clear)
                        zoneController.Clear();
                    locationToMove = zoneController.Content.transform;
                    break;
                case Location.Grave:
                    var graveyard = _graveyardsDict[pointOfView];
                    locationToMove = graveyard.Content.transform;
                    break;
                case Location.Banishment:
                    throw new NotImplementedException();
                case Location.Extra:
                    throw new NotImplementedException();
                case Location.Overlay:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException();
            };
            return locationToMove;
        }

        private void InitZone(CardModel card, Location location, PointOfView pointOfView, FieldZones zone)
        {
            switch(location)
            {
                case Location.Deck:
                    throw new NotImplementedException();
                case Location.MonsterZone:
                case Location.SpellTrapZone:
                    var zoneController = _fieldZonesDict[pointOfView][zone];
                    zoneController.InitCard(card);
                    break;
                case Location.Grave:
                    _graveyardsDict[pointOfView].InitCard(card);
                    break;
                case Location.Banishment:
                    throw new NotImplementedException();
                case Location.Extra:
                    throw new NotImplementedException();
                case Location.Overlay:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException();
            };
        }
    }
}