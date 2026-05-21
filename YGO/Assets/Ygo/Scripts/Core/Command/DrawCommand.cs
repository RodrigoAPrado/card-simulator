using System.Collections.Generic;
using Ygo.Scripts.Core.Command.Base;
using Ygo.Scripts.Core.Model;

namespace Ygo.Scripts.Core.Command
{
    public class DrawCommand : ICommand
    {
        public IReadOnlyList<CardModel> HandBefore { get; }
        public IReadOnlyList<CardModel> HandAfter { get; }
        public CardModel DrawnCard { get; }

        public DrawCommand(
            IReadOnlyList<CardModel> handBefore, 
            IReadOnlyList<CardModel> handAfter, 
            CardModel drawCard)
        {
            HandBefore = handBefore;
            HandAfter = handAfter;
            DrawnCard = drawCard;
        }
    }
}