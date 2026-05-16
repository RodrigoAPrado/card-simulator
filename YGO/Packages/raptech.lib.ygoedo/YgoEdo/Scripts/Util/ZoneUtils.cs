using System.Collections.Generic;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Constant;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Flag;

namespace YgoSoul.RapTech.Lib.YgoEdo.Util
{
    public class ZoneUtils
    {
        public static readonly Dictionary<OCG_Zone, OCG_CardLocation> ZoneLocation = new()
        {
            { OCG_Zone.Monster0, OCG_CardLocation.MonsterZone },
            { OCG_Zone.Monster1, OCG_CardLocation.MonsterZone },
            { OCG_Zone.Monster2, OCG_CardLocation.MonsterZone },
            { OCG_Zone.Monster3, OCG_CardLocation.MonsterZone },
            { OCG_Zone.Monster4, OCG_CardLocation.MonsterZone },
            { OCG_Zone.ExtraMonsterZone0, OCG_CardLocation.MonsterZone },
            { OCG_Zone.ExtraMonsterZone1, OCG_CardLocation.MonsterZone },
            { OCG_Zone.SpellTrap0, OCG_CardLocation.SpellTrapZone },
            { OCG_Zone.SpellTrap1, OCG_CardLocation.SpellTrapZone },
            { OCG_Zone.SpellTrap2, OCG_CardLocation.SpellTrapZone },
            { OCG_Zone.SpellTrap3, OCG_CardLocation.SpellTrapZone },
            { OCG_Zone.SpellTrap4, OCG_CardLocation.SpellTrapZone },
            { OCG_Zone.FieldZone, OCG_CardLocation.SpellTrapZone },
            { OCG_Zone.Pendulum0, OCG_CardLocation.SpellTrapZone },
            { OCG_Zone.Pendulum1, OCG_CardLocation.SpellTrapZone },
            { OCG_Zone.OpponentMonster0, OCG_CardLocation.MonsterZone },
            { OCG_Zone.OpponentMonster1, OCG_CardLocation.MonsterZone },
            { OCG_Zone.OpponentMonster2, OCG_CardLocation.MonsterZone },
            { OCG_Zone.OpponentMonster3, OCG_CardLocation.MonsterZone },
            { OCG_Zone.OpponentMonster4, OCG_CardLocation.MonsterZone },
            { OCG_Zone.OpponentExtraMonsterZone0, OCG_CardLocation.MonsterZone },
            { OCG_Zone.OpponentExtraMonsterZone1, OCG_CardLocation.MonsterZone },
            { OCG_Zone.OpponentSpellTrap0, OCG_CardLocation.SpellTrapZone },
            { OCG_Zone.OpponentSpellTrap1, OCG_CardLocation.SpellTrapZone },
            { OCG_Zone.OpponentSpellTrap2, OCG_CardLocation.SpellTrapZone },
            { OCG_Zone.OpponentSpellTrap3, OCG_CardLocation.SpellTrapZone },
            { OCG_Zone.OpponentSpellTrap4, OCG_CardLocation.SpellTrapZone },
            { OCG_Zone.OpponentFieldZone, OCG_CardLocation.SpellTrapZone },
            { OCG_Zone.OpponentPendulum0, OCG_CardLocation.SpellTrapZone },
            { OCG_Zone.OpponentPendulum1, OCG_CardLocation.SpellTrapZone }
        };

        public static readonly Dictionary<OCG_Zone, uint> ZoneIndexInput = new()
        {
            { OCG_Zone.Monster0, 0 },
            { OCG_Zone.Monster1, 1 },
            { OCG_Zone.Monster2, 2 },
            { OCG_Zone.Monster3, 3 },
            { OCG_Zone.Monster4, 4 },
            { OCG_Zone.ExtraMonsterZone0, 5 },
            { OCG_Zone.ExtraMonsterZone1, 6 },
            { OCG_Zone.SpellTrap0, 0 },
            { OCG_Zone.SpellTrap1, 1 },
            { OCG_Zone.SpellTrap2, 2 },
            { OCG_Zone.SpellTrap3, 3 },
            { OCG_Zone.SpellTrap4, 4 },
            { OCG_Zone.FieldZone, 5 },
            { OCG_Zone.Pendulum0, 6 },
            { OCG_Zone.Pendulum1, 7 },
            { OCG_Zone.OpponentMonster0, 0 },
            { OCG_Zone.OpponentMonster1, 1 },
            { OCG_Zone.OpponentMonster2, 2 },
            { OCG_Zone.OpponentMonster3, 3 },
            { OCG_Zone.OpponentMonster4, 4 },
            { OCG_Zone.OpponentExtraMonsterZone0, 5 },
            { OCG_Zone.OpponentExtraMonsterZone1, 6 },
            { OCG_Zone.OpponentSpellTrap0, 0 },
            { OCG_Zone.OpponentSpellTrap1, 1 },
            { OCG_Zone.OpponentSpellTrap2, 2 },
            { OCG_Zone.OpponentSpellTrap3, 3 },
            { OCG_Zone.OpponentSpellTrap4, 4 },
            { OCG_Zone.OpponentFieldZone, 5 },
            { OCG_Zone.OpponentPendulum0, 6 },
            { OCG_Zone.OpponentPendulum1, 7 }
        };

        public static readonly List<OCG_Zone> ZoneIndexQuery = new()
        {
            OCG_Zone.Monster0,
            OCG_Zone.Monster1,
            OCG_Zone.Monster2,
            OCG_Zone.Monster3,
            OCG_Zone.Monster4,
            OCG_Zone.ExtraMonsterZone0,
            OCG_Zone.ExtraMonsterZone1,
            OCG_Zone.SpellTrap0,
            OCG_Zone.SpellTrap1,
            OCG_Zone.SpellTrap2,
            OCG_Zone.SpellTrap3,
            OCG_Zone.SpellTrap4,
            OCG_Zone.FieldZone,
            OCG_Zone.Pendulum0,
            OCG_Zone.Pendulum1
        };
    }
}