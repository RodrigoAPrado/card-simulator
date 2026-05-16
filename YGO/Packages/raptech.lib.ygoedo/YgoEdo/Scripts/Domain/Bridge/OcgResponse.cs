using System;

namespace YgoSoul.RapTech.Lib.YgoEdo.Domain.Bridge
{
    public class OcgResponse
    {
        public OcgResponse(IntPtr message, uint length)
        {
            Message = message;
            Length = length;
        }

        public IntPtr Message { get; }
        public uint Length { get; }
    }
}