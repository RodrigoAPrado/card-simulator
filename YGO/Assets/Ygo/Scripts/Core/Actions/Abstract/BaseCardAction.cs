using System;
using Ygo.Core.Abstract;

namespace Ygo.Core.Actions.Abstract
{
    public abstract class BaseCardAction : BaseGameAction
    {
        protected readonly Guid PlayerId;
        protected readonly ICardInstance Card;
        
        protected BaseCardAction(GameState gameState, Guid playerId, ICardInstance card) : base(gameState)
        {
            PlayerId = playerId;
            Card = card;
        }
    }
}