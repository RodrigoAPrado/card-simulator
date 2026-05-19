using System.Collections.Generic;
using Ygo.Scripts.Core.Enum;
using Ygo.Scripts.Data;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card;

namespace Ygo.Core.Duel
{
    public class DuelState
    {
        public DuelInteraction CurrentInteraction { get; }
        
        public List<ICardData> MainDeck { get; }
        
        public DuelState(DuelData data)
        {
            
        }
    }
}