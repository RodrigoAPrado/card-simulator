using System;
using System.Collections.Generic;
using Ygo.Core.Actions.Abstract;

namespace Ygo.Core.Effects.Resolution.Abstract
{
    public interface ICardEffectResolution
    {
        IList<IGameAction> Resolve(Guid playerId, GameState gameState);
    }
}