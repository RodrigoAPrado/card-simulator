using System;
using Ygo.Data.Effect;

namespace Ygo.Core.Effects.Abstract
{
    public interface ICardEffect
    {
        Guid Id { get; }
        string CardId { get; }
        string Description { get; }
        bool CanActivate(Guid playerId, TurnContext context);
        void ApplyCost();
        void Resolve(Guid playerId, GameState gameState);
    }
}