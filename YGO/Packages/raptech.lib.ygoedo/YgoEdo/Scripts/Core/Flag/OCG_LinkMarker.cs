using System;

namespace YgoSoul.RapTech.Lib.YgoEdo.Core.Flag
{
    [Flags]
    public enum OCG_LinkMarker : uint
    {
        None = 0,
        BottomLeft = 0x1, // 0001 octal
        Bottom = 0x2, // 0002 octal
        BottomRight = 0x4, // 0004 octal
        Left = 0x8, // 0010 octal
        Right = 0x20, // 0040 octal
        TopLeft = 0x40, // 0100 octal
        Top = 0x80, // 0200 octal
        TopRight = 0x100 // 0400 octal
    }
}