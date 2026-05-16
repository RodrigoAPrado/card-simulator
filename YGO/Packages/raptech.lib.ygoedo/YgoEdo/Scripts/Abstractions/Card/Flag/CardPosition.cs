using System;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card.Flag
{
    [Flags]
    public enum CardPosition
    {
        FaceUpAttack = 1 << 0,
        FaceDownAttack = 1 << 1,
        FaceUpDefense = 1 << 2,
        FaceDownDefense = 1 << 3,
        FaceUp = FaceUpAttack | FaceUpDefense,
        FaceDown = FaceDownAttack | FaceDownDefense,
        Attack = FaceUpAttack | FaceDownAttack,
        Defense = FaceUpDefense | FaceDownDefense
    }
}