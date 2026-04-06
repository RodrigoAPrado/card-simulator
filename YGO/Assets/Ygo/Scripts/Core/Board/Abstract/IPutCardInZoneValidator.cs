using Ygo.Core.Abstract;

namespace Ygo.Core.Board.Abstract
{
    public interface IPutCardInZoneValidator
    {
        bool Validate(ICardInstance cardInstance);
    }
}