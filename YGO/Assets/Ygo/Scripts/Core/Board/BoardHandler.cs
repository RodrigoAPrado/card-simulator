using System;
using System.Collections.Generic;
using System.Linq;
using Ygo.Core.Board.Abstract;

namespace Ygo.Core.Board
{
    public class BoardHandler
    {
        public IList<IBoardZone> MonsterZones => _monsterZones.AsReadOnly();
        public IList<IBoardZone> SpellTrapZones => _spellTrapZones.AsReadOnly();
        public IBoardZone FieldZone { get; private set; }
        
        private List<IBoardZone> _monsterZones;
        private List<IBoardZone> _spellTrapZones;
        
        public void Setup(IDictionary<ZoneType, IPutCardInZoneValidator> validators)
        {
            _monsterZones = BuildZones(ZoneType.MainMonsterZone, validators[ZoneType.MainMonsterZone]);
            _spellTrapZones = BuildZones(ZoneType.SpellTrapZone, validators[ZoneType.SpellTrapZone]);
            FieldZone = new BoardZone(ZoneType.FieldZone, ZonePosition.Field, validators[ZoneType.FieldZone]);
        }

        public IList<IBoardZone> AvailableZones(ZoneType zoneType)
        {
            var zones = new List<IBoardZone>();
            switch (zoneType)
            {
                case ZoneType.MainMonsterZone:
                    zones = MonsterZones.Where(x => x.IsFree).ToList();
                    break;
                case ZoneType.SpellTrapZone:
                    zones = SpellTrapZones.Where(x => x.IsFree).ToList();
                    break;
                case ZoneType.FieldZone:
                    if(FieldZone.IsFree)
                        zones.Add(FieldZone);
                    break;
                case ZoneType.ExtraMonsterZone:
                    throw new NotImplementedException("Extra monster zone");
                default:
                    throw new ArgumentOutOfRangeException(nameof(zoneType), zoneType, null);
            }
            return zones;
        }

        public bool IsAnyFree(ZoneType zoneType)
        {
            switch (zoneType)
            {
                case ZoneType.MainMonsterZone:
                    return MonsterZones.Any(x => x.IsFree);
                case ZoneType.SpellTrapZone:
                    return SpellTrapZones.Any(x => x.IsFree);
                case ZoneType.FieldZone:
                    return FieldZone.IsFree;
                case ZoneType.ExtraMonsterZone:
                    throw new NotImplementedException("Extra monster zone");
                default:
                    throw new ArgumentOutOfRangeException(nameof(zoneType), zoneType, null);
            }
        }

        private List<IBoardZone> BuildZones(ZoneType type, IPutCardInZoneValidator validator)
        {
            return new List<IBoardZone>
            {
                new BoardZone(type, ZonePosition.LeftMost, validator),
                new BoardZone(type, ZonePosition.LeftCenter, validator),
                new BoardZone(type, ZonePosition.MiddleCenter, validator),
                new BoardZone(type, ZonePosition.RightCenter, validator),
                new BoardZone(type, ZonePosition.RightMost, validator)
            };
        }
    }
}