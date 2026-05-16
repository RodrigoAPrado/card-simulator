using System;

namespace YgoSoul.RapTech.Lib.YgoEdo.Core.Flag
{
    [Flags]
    public enum OCG_CardPosition : byte
    {
        FaceUpAttack = 0x1,
        FaceDownAttack = 0x2,
        FaceUpDefense = 0x4,
        FaceDownDefense = 0x8,
        FaceUp = FaceUpAttack | FaceUpDefense,
        FaceDown = FaceDownAttack | FaceDownDefense,
        Attack = FaceUpAttack | FaceDownAttack,
        Defense = FaceUpDefense | FaceDownDefense
    }
}