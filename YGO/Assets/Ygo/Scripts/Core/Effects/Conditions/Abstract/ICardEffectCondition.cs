using System;

namespace Ygo.Core.Effects.Conditions.Abstract
{
    public interface ICardEffectCondition
    {
        bool CanActivate(Guid playerId, TurnContext context);
    }
}