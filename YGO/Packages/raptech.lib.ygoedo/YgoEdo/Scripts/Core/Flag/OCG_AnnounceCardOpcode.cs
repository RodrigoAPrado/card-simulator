using System;

namespace YgoSoul.RapTech.Lib.YgoEdo.Core.Flag
{
    [Flags]
    public enum OCG_AnnounceCardOpcode : ulong
    {
        None = 0,

        // Operadores Aritméticos e Lógicos
        Add = 0x4000000000000000,
        Sub = 0x4000000100000000,
        Mul = 0x4000000200000000,
        Div = 0x4000000300000000,
        And = 0x4000000400000000, // Lógico (&&)
        Or = 0x4000000500000000, // Lógico (||)
        Neg = 0x4000000600000000,
        Not = 0x4000000700000000,

        // Operadores Bitwise
        BAnd = 0x4000000800000000, // Bitwise (&)
        BOr = 0x4000000900000000, // Bitwise (|)
        BNot = 0x4000001000000000, // Bitwise (~)
        BXor = 0x4000001100000000,
        LShift = 0x4000001200000000,
        RShift = 0x4000001300000000,

        // Flags de Permissão
        AllowAliases = 0x4000001400000000,
        AllowTokens = 0x4000001500000000,

        // Verificadores (Comparam o valor da pilha com a carta)
        IsCode = 0x4000010000000000,
        IsSetCard = 0x4000010100000000,
        IsType = 0x4000010200000000,
        IsRace = 0x4000010300000000,
        IsAttribute = 0x4000010400000000,

        // Getters (Pegam dados da carta e jogam na pilha)
        GetCode = 0x4000010500000000,
        GetSetCard = 0x4000010600000000,
        GetType = 0x4000010700000000,
        GetRace = 0x4000010800000000,
        GetAttribute = 0x4000010900000000
    }

/*
 * The True Name
 * Question
 * Prohibition
 * Primite Roar
 * Primite Lordly Lode
 * Primite Howl
 * Mind Scan (card)
 * Mind Crush
 * Lullaby of Obedience
 * Gunkan Suship Catch-of-the-Day
 * Gravekeeper's Trap -
 * Goddess Urd's Verdict -
 * Dirge of the Lost Dragon
 * Dark Designator
 * D.D. Designator
 * Crossout Designator
 * Cloak and Dagger
 * The Black Goat Laughs
 * Artmage Academic Arcane Arts Acropolis -
 * Archfiend's Oath
 */

    /*
     * Akashic Magician
     * Alsei, the Sylvan High Protector
     * Ancient Gear Gadget -
     * Engraver of the Mask
     * GIshki Diviner
     * Great Phantom Thief
     * Ma'at -
     * Psi-Blocker
     * The Suppression Pluto
     */
}