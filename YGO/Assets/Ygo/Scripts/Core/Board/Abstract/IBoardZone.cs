using System;
using Ygo.Core.Abstract;

namespace Ygo.Core.Board.Abstract
{
    public interface IBoardZone
    {
        Guid Id { get; }
        ZoneType Type { get; }
        ZonePosition Position { get; }
        ICardInstance CardInZone { get; }
        bool IsFree { get; }
        bool CanPutCard(ICardInstance cardInstance);
        PutCardInZoneResult TryPutCard(ICardInstance cardInstance);
        RemoveCardFromZoneResult TryRemoveCard();
        
    }
}