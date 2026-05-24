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
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Enum;

namespace Ygo.Controller
{
    public class FieldController : MonoBehaviour
    {
        [field:SerializeField]
        private FieldZoneController[] fieldZones;
        
        private IReadOnlyDictionary<PointOfView, IReadOnlyDictionary<FieldZones, FieldZoneController>> _fieldZonesDict;
        
        private DuelInstance _duelInstance;
        private bool _showOpponent;
        
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
            
            _duelInstance.EventQueue.Subscribe<SelectPlaceEvent>(OnSelectPlaceEvent);
            _showOpponent = showOpponent;
        }

        private async UniTask OnSelectPlaceEvent(SelectPlaceEvent e)
        {
            if (e.PointOfView == PointOfView.Opponent && !_showOpponent)
                return; 
            
            var opponentPointOfView = e.PointOfView == PointOfView.Player ? PointOfView.Opponent : PointOfView.Player;

            var choiceIndex = 0;
            foreach (var choice in e.Choices)
            {
                var pointOfView = (int) choice >= 100 ? PointOfView.Opponent : PointOfView.Player;
                var actualChoice = (int) choice >= 100 ? choice - 100 : choice;
                var fieldZone = _fieldZonesDict[pointOfView][actualChoice];
                var actualChoiceIndex = choiceIndex;
                fieldZone.ToggleHighlight(true);
                fieldZone.SetAction(() => ConfirmSelection(actualChoiceIndex));
                choiceIndex++;
            }
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
    }
}