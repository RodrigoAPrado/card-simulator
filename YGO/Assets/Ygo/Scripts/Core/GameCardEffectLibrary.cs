using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Ygo.Core.Abstract;
using Ygo.Core.Effects.Abstract;

namespace Ygo.Core
{
    public class GameCardEffectLibrary
    {
        private readonly Dictionary<string, Dictionary<Guid, ICardEffect>> _cardEffects;
        public GameCardEffectLibrary(ICardEffectRepository repo, TurnContext context)
        {
            _cardEffects = new Dictionary<string, Dictionary<Guid, ICardEffect>>();
            foreach (var player in context.Players)
            {
                foreach (var card in player.CardsHandler.PlayerCards)
                {
                    var effectData = repo.GetEffectById(card.Data.Id);
                    if (effectData == null)
                        continue;
                    if(_cardEffects.ContainsKey(effectData.Id))
                        continue;
                    var effects = new Dictionary<Guid, ICardEffect>();
                    foreach (var e in effectData.Effects)
                    {
                        var effect = CardEffectFactory.CreateEffectFromData(card.Data.Id, e);
                        effects.Add(effect.Id, effect);
                    }
                    _cardEffects.Add(card.Data.Id, effects);
                }
            }
        }

        public IDictionary<Guid,ICardEffect> GetCardEffectsByCardId(string cardId)
        {
            var effects = _cardEffects.GetValueOrDefault(cardId);
            return effects ?? new Dictionary<Guid, ICardEffect>();
        }

        public string GetCardEffectTextByCardId(string cardId)
        {
            var effects = _cardEffects.GetValueOrDefault(cardId);
            if (effects == null)
                return string.Empty;

            var sb = new StringBuilder();
            foreach (var val in effects)
            {
                sb.Append(val.Value.Description);
            }
            return sb.ToString();
        }

        public ICardEffect GetCardEffectById(Guid effectId)
        {
            foreach (var e in _cardEffects)
            {
                e.Value.TryGetValue(effectId, out ICardEffect effect);
                if(effect != null)
                    return effect;
            }
            return null;
        }
    }
}