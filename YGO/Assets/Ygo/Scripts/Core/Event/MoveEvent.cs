using Ygo.Scripts.Core.Event.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Flag;

namespace Ygo.Scripts.Core.Event
{
    public class MoveEvent : IEvent
    {
        public uint CardCode { get; }
        
        public Location BeginLocation { get; }
        public int BeginSequence { get; }
        public FieldZones BeginFieldZone { get; }
        public byte BeginController { get; }
        public CardPosition BeginCardPosition { get; }
        
        public Location EndLocation { get; }
        public int EndSequence { get; }
        public FieldZones EndFieldZone { get; }
        public byte EndController { get; }
        public CardPosition EndCardPosition { get; }

        public MoveEvent(
            uint cardCode, 
            Location beginLocation, 
            int beginSequence, 
            FieldZones beginFieldZone,
            byte beginController,
            CardPosition beginCardPosition, 
            Location endLocation, 
            int endSequence, 
            FieldZones endFieldZone,
            byte endController,
            CardPosition endCardPosition)
        {
            CardCode = cardCode;
            BeginLocation = beginLocation;
            BeginSequence = beginSequence;
            BeginFieldZone = beginFieldZone;
            BeginController = beginController;
            BeginCardPosition = beginCardPosition;
            EndLocation = endLocation;
            EndSequence = endSequence;
            EndFieldZone = endFieldZone;
            EndController = endController;
            EndCardPosition = endCardPosition;
        }
    }
}