using System;

namespace YgoSoul.RapTech.Lib.YgoEdo.Core.Flag
{
    [Flags]
    public enum OCG_MonsterRaces : ulong
    {
        None = 0x0,
        Warrior = 0x1,
        SpellCaster = 0x2,
        Fairy = 0x4,
        Fiend = 0x8,
        Zombie = 0x10,
        Machine = 0x20,
        Aqua = 0x40,
        Pyro = 0x80,
        Rock = 0x100,
        WingedBeast = 0x200,
        Plant = 0x400,
        Insect = 0x800,
        Thunder = 0x1000,
        Dragon = 0x2000,
        Beast = 0x4000,
        BeastWarrior = 0x8000,
        Dinosaur = 0x10000,
        Fish = 0x20000,
        SeaSerpent = 0x40000,
        Reptile = 0x80000,
        Psychic = 0x100000,
        Divine = 0x200000,
        CreatorGod = 0x400000,
        Wyrm = 0x800000,
        Cyberse = 0x1000000,
        Illusion = 0x2000000,
        Cyborg = 0x4000000,
        MagicalKnight = 0x8000000,
        HighDragon = 0x10000000,
        OmegaPsychic = 0x20000000,
        CelestialWarrior = 0x40000000,
        Galaxy = 0x80000000,
        Yokai = 0x4000000000000000,

        // Auxiliares
        Max = Galaxy,
        All = ((Max << 1) - 1) | Yokai
    }
}