namespace Ygo.Core.Board
{
    public class PutCardInZoneResult
    {
        public bool Ok => Result == PutCardInZoneResultCode.Success;
        public bool Fail => !Ok;
        public PutCardInZoneResultCode Result { get; }

        public PutCardInZoneResult(PutCardInZoneResultCode result)
        {
            Result = result;
        }
    }

    public enum PutCardInZoneResultCode
    {
        Success = 0,
        ZoneOccupied = 1,
        ImproperZone = 2,
    }
}