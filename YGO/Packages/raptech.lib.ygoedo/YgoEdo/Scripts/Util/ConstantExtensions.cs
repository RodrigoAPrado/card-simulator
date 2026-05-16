using System;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.System.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Constant;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Flag;

namespace YgoSoul.RapTech.Lib.YgoEdo.Util
{
    public static class ConstantExtensions
    {
        public static SystemVictoryReason ToSystemVictoryReason(this OCG_VictoryReason value)
        {
            return (SystemVictoryReason)(uint)value;
        }

        public static CoinResult ToCoinResult(this OCG_CoinResult value)
        {
            return (CoinResult)(int)value;
        }

        public static HintTiming ToHintTiming(this OCG_HintTiming value)
        {
            return (HintTiming)(uint)value;
        }

        public static PlayerHint ToPlayerHint(this OCG_PlayerHint value)
        {
            return (PlayerHint)(uint)value;
        }

        public static DuelPhase ToDuelPhase(this OCG_GamePhases value)
        {
            return (DuelPhase)(uint)value;
        }

        public static OCG_DuelFlags FromDuelMode(this DuelMode value)
        {
            switch (value)
            {
                case DuelMode.MasterRule0:
                    return OCG_DuelFlags.MasterRuleGoat;
                case DuelMode.MasterRule1:
                    return OCG_DuelFlags.MasterRule1;
                case DuelMode.MasterRule2:
                    return OCG_DuelFlags.MasterRule2;
                case DuelMode.MasterRule3:
                    return OCG_DuelFlags.MasterRule3;
                case DuelMode.MasterRule4:
                    return OCG_DuelFlags.MasterRule4;
                case DuelMode.MasterRule5:
                    return OCG_DuelFlags.MasterRule5;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }

        public static CardAttribute ToCardAttribute(this OCG_MonsterAttributes value)
        {
            switch (value)
            {
                case OCG_MonsterAttributes.Earth:
                    return CardAttribute.Earth;
                case OCG_MonsterAttributes.Water:
                    return CardAttribute.Water;
                case OCG_MonsterAttributes.Fire:
                    return CardAttribute.Fire;
                case OCG_MonsterAttributes.Wind:
                    return CardAttribute.Wind;
                case OCG_MonsterAttributes.Light:
                    return CardAttribute.Light;
                case OCG_MonsterAttributes.Dark:
                    return CardAttribute.Dark;
                case OCG_MonsterAttributes.Divine:
                    return CardAttribute.Divine;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }

        public static LinkArrow ToLinkArrow(this OCG_LinkMarker value)
        {
            switch (value)
            {
                case OCG_LinkMarker.BottomLeft:
                    return LinkArrow.BottomLeft;
                case OCG_LinkMarker.Bottom:
                    return LinkArrow.Bottom;
                case OCG_LinkMarker.BottomRight:
                    return LinkArrow.BottomRight;
                case OCG_LinkMarker.Left:
                    return LinkArrow.Left;
                case OCG_LinkMarker.Right:
                    return LinkArrow.Right;
                case OCG_LinkMarker.TopLeft:
                    return LinkArrow.TopLeft;
                case OCG_LinkMarker.Top:
                    return LinkArrow.Top;
                case OCG_LinkMarker.TopRight:
                    return LinkArrow.TopRight;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }

        public static CardType ToCardDataType(this OCG_CardType value)
        {
            switch (value)
            {
                case OCG_CardType.Monster:
                    return CardType.Monster;
                case OCG_CardType.Spell:
                    return CardType.Spell;
                case OCG_CardType.Trap:
                    return CardType.Trap;
                case OCG_CardType.Normal:
                    return CardType.Normal;
                case OCG_CardType.Effect:
                    return CardType.Effect;
                case OCG_CardType.Fusion:
                    return CardType.Fusion;
                case OCG_CardType.Ritual:
                    return CardType.Ritual;
                case OCG_CardType.TrapMonster:
                    return CardType.TrapMonster;
                case OCG_CardType.Spirit:
                    return CardType.Spirit;
                case OCG_CardType.Union:
                    return CardType.Union;
                case OCG_CardType.Gemini:
                    return CardType.Gemini;
                case OCG_CardType.Tuner:
                    return CardType.Tuner;
                case OCG_CardType.Synchro:
                    return CardType.Synchro;
                case OCG_CardType.Token:
                    return CardType.Token;
                case OCG_CardType.Maximum:
                    return CardType.Maximum;
                case OCG_CardType.QuickPlay:
                    return CardType.QuickPlay;
                case OCG_CardType.Continuous:
                    return CardType.Continuous;
                case OCG_CardType.Equip:
                    return CardType.Equip;
                case OCG_CardType.Field:
                    return CardType.Field;
                case OCG_CardType.Counter:
                    return CardType.Counter;
                case OCG_CardType.Flip:
                    return CardType.Flip;
                case OCG_CardType.Toon:
                    return CardType.Toon;
                case OCG_CardType.Xyz:
                    return CardType.Xyz;
                case OCG_CardType.Pendulum:
                    return CardType.Pendulum;
                case OCG_CardType.SpSummon:
                    return CardType.SpSummon;
                case OCG_CardType.Link:
                    return CardType.Link;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }

        public static MonsterType ToMonsterType(this OCG_MonsterRaces value)
        {
            switch (value)
            {
                case OCG_MonsterRaces.None:
                    return MonsterType.None;
                case OCG_MonsterRaces.Warrior:
                    return MonsterType.Warrior;
                case OCG_MonsterRaces.SpellCaster:
                    return MonsterType.SpellCaster;
                case OCG_MonsterRaces.Fairy:
                    return MonsterType.Fairy;
                case OCG_MonsterRaces.Fiend:
                    return MonsterType.Fiend;
                case OCG_MonsterRaces.Zombie:
                    return MonsterType.Zombie;
                case OCG_MonsterRaces.Machine:
                    return MonsterType.Machine;
                case OCG_MonsterRaces.Aqua:
                    return MonsterType.Aqua;
                case OCG_MonsterRaces.Pyro:
                    return MonsterType.Pyro;
                case OCG_MonsterRaces.Rock:
                    return MonsterType.Rock;
                case OCG_MonsterRaces.WingedBeast:
                    return MonsterType.WingedBeast;
                case OCG_MonsterRaces.Plant:
                    return MonsterType.Plant;
                case OCG_MonsterRaces.Insect:
                    return MonsterType.Insect;
                case OCG_MonsterRaces.Thunder:
                    return MonsterType.Thunder;
                case OCG_MonsterRaces.Dragon:
                    return MonsterType.Dragon;
                case OCG_MonsterRaces.Beast:
                    return MonsterType.Beast;
                case OCG_MonsterRaces.BeastWarrior:
                    return MonsterType.BeastWarrior;
                case OCG_MonsterRaces.Dinosaur:
                    return MonsterType.Dinosaur;
                case OCG_MonsterRaces.Fish:
                    return MonsterType.Fish;
                case OCG_MonsterRaces.SeaSerpent:
                    return MonsterType.SeaSerpent;
                case OCG_MonsterRaces.Reptile:
                    return MonsterType.Reptile;
                case OCG_MonsterRaces.Psychic:
                    return MonsterType.Psychic;
                case OCG_MonsterRaces.Divine:
                    return MonsterType.Divine;
                case OCG_MonsterRaces.CreatorGod:
                    return MonsterType.CreatorGod;
                case OCG_MonsterRaces.Wyrm:
                    return MonsterType.Wyrm;
                case OCG_MonsterRaces.Cyberse:
                    return MonsterType.Cyberse;
                case OCG_MonsterRaces.Illusion:
                    return MonsterType.Illusion;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }

        public static SearchCategory ToCardSearchCategory(this OCG_CardCategory value)
        {
            switch (value)
            {
                case OCG_CardCategory.None:
                    return SearchCategory.None;
                case OCG_CardCategory.DestroyMonster:
                    return SearchCategory.DestroyMonster;
                case OCG_CardCategory.DestroyST:
                    return SearchCategory.DestroySpellOrTrap;
                case OCG_CardCategory.DestroyDeck:
                    return SearchCategory.DestroyDeck;
                case OCG_CardCategory.DestroyHand:
                    return SearchCategory.DestroyHand;
                case OCG_CardCategory.SendToGY:
                    return SearchCategory.SendToGrave;
                case OCG_CardCategory.SendToHand:
                    return SearchCategory.SendToHand;
                case OCG_CardCategory.SendToDeck:
                    return SearchCategory.SendToDeck;
                case OCG_CardCategory.Banish:
                    return SearchCategory.Banish;
                case OCG_CardCategory.Draw:
                    return SearchCategory.Draw;
                case OCG_CardCategory.Search:
                    return SearchCategory.Search;
                case OCG_CardCategory.ChangeAtkDef:
                    return SearchCategory.ChangeAtkDef;
                case OCG_CardCategory.ChangeLevelRank:
                    return SearchCategory.ChangeLevelRank;
                case OCG_CardCategory.ChangePosition:
                    return SearchCategory.ChangePosition;
                case OCG_CardCategory.Piercing:
                    return SearchCategory.Piercing;
                case OCG_CardCategory.DirectAttack:
                    return SearchCategory.DirectAttack;
                case OCG_CardCategory.MultiAttack:
                    return SearchCategory.MultiAttack;
                case OCG_CardCategory.NegateActivation:
                    return SearchCategory.NegateActivation;
                case OCG_CardCategory.NegateEffect:
                    return SearchCategory.NegateEffect;
                case OCG_CardCategory.DamageLP:
                    return SearchCategory.DamageLifePoints;
                case OCG_CardCategory.RecoverLP:
                    return SearchCategory.RecoverLifePoints;
                case OCG_CardCategory.SpecialSummon:
                    return SearchCategory.SpecialSummon;
                case OCG_CardCategory.NonEffectRelated:
                    return SearchCategory.NonEffectRelated;
                case OCG_CardCategory.TokenRelated:
                    return SearchCategory.TokenRelated;
                case OCG_CardCategory.FusionRelated:
                    return SearchCategory.FusionRelated;
                case OCG_CardCategory.RitualRelated:
                    return SearchCategory.RitualRelated;
                case OCG_CardCategory.SynchroRelated:
                    return SearchCategory.SynchroRelated;
                case OCG_CardCategory.XyzRelated:
                    return SearchCategory.XyzRelated;
                case OCG_CardCategory.LinkRelated:
                    return SearchCategory.LinkRelated;
                case OCG_CardCategory.CounterRelated:
                    return SearchCategory.CounterRelated;
                case OCG_CardCategory.Gamble:
                    return SearchCategory.Gamble;
                case OCG_CardCategory.Control:
                    return SearchCategory.Control;
                case OCG_CardCategory.MoveZones:
                    return SearchCategory.MoveZones;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }

        public static CounterType ToCounterType(this OCG_CounterType value)
        {
            switch (value)
            {
                case OCG_CounterType.Spell:
                    return CounterType.Spell;
                case OCG_CounterType.Wedge:
                    return CounterType.Wedge;
                case OCG_CounterType.Bushido:
                    return CounterType.Bushido;
                case OCG_CounterType.Psychic:
                    return CounterType.Psychic;
                case OCG_CounterType.Shine:
                    return CounterType.Shine;
                case OCG_CounterType.Crystal:
                    return CounterType.Crystal;
                case OCG_CounterType.ColosseumCageOfTheGladiatorBeasts:
                    return CounterType.ColosseumCageOfTheGladiatorBeasts;
                case OCG_CounterType.Morph:
                    return CounterType.Morph;
                case OCG_CounterType.Venom:
                    return CounterType.Venom;
                case OCG_CounterType.Genex:
                    return CounterType.Genex;
                case OCG_CounterType.AncientCity:
                    return CounterType.AncientCity;
                case OCG_CounterType.Thunder:
                    return CounterType.Thunder;
                case OCG_CounterType.Greed:
                    return CounterType.Greed;
                case OCG_CounterType.A_Counter:
                    return CounterType.A_Counter;
                case OCG_CounterType.Worm:
                    return CounterType.Worm;
                case OCG_CounterType.BlackFeather:
                    return CounterType.BlackFeather;
                case OCG_CounterType.HyperVenom:
                    return CounterType.HyperVenom;
                case OCG_CounterType.Karakuri:
                    return CounterType.Karakuri;
                case OCG_CounterType.Chaos:
                    return CounterType.Chaos;
                case OCG_CounterType.MiracleJurassicEgg:
                    return CounterType.MiracleJurassicEgg;
                case OCG_CounterType.Ice:
                    return CounterType.Ice;
                case OCG_CounterType.Spellstone:
                    return CounterType.Spellstone;
                case OCG_CounterType.Nut:
                    return CounterType.Nut;
                case OCG_CounterType.Flower:
                    return CounterType.Flower;
                case OCG_CounterType.Fog:
                    return CounterType.Fog;
                case OCG_CounterType.Payback:
                    return CounterType.Payback;
                case OCG_CounterType.Clock:
                    return CounterType.Clock;
                case OCG_CounterType.D_Counter:
                    return CounterType.D_Counter;
                case OCG_CounterType.Junk:
                    return CounterType.Junk;
                case OCG_CounterType.Gate:
                    return CounterType.Gate;
                case OCG_CounterType.Bes:
                    return CounterType.Bes;
                case OCG_CounterType.Plant:
                    return CounterType.Plant;
                case OCG_CounterType.Guard:
                    return CounterType.Guard;
                case OCG_CounterType.Dragonic:
                    return CounterType.Dragonic;
                case OCG_CounterType.Ocean:
                    return CounterType.Ocean;
                case OCG_CounterType.String:
                    return CounterType.String;
                case OCG_CounterType.Chronicle:
                    return CounterType.Chronicle;
                case OCG_CounterType.MetalShooter:
                    return CounterType.MetalShooter;
                case OCG_CounterType.DesMosquito:
                    return CounterType.DesMosquito;
                case OCG_CounterType.DarkCatapulter:
                    return CounterType.DarkCatapulter;
                case OCG_CounterType.BalloonLizard:
                    return CounterType.BalloonLizard;
                case OCG_CounterType.MagicReflector:
                    return CounterType.MagicReflector;
                case OCG_CounterType.Destiny:
                    return CounterType.Destiny;
                case OCG_CounterType.YouGotItBoss:
                    return CounterType.YouGotItBoss;
                case OCG_CounterType.Kickfire:
                    return CounterType.Kickfire;
                case OCG_CounterType.Shark:
                    return CounterType.Shark;
                case OCG_CounterType.Pumpkin:
                    return CounterType.Pumpkin;
                case OCG_CounterType.HiFiveTheSky:
                    return CounterType.HiFiveTheSky;
                case OCG_CounterType.RisingSun:
                    return CounterType.RisingSun;
                case OCG_CounterType.Balloon:
                    return CounterType.Balloon;
                case OCG_CounterType.Yosen:
                    return CounterType.Yosen;
                case OCG_CounterType.Boxer:
                    return CounterType.Boxer;
                case OCG_CounterType.Symphonic:
                    return CounterType.Symphonic;
                case OCG_CounterType.Performage:
                    return CounterType.Performage;
                case OCG_CounterType.Kaiju:
                    return CounterType.Kaiju;
                case OCG_CounterType.Cubic:
                    return CounterType.Cubic;
                case OCG_CounterType.Zushin:
                    return CounterType.Zushin;
                case OCG_CounterType.Number51FinisherTheStrongArm:
                    return CounterType.Number51FinisherTheStrongArm;
                case OCG_CounterType.Predator:
                    return CounterType.Predator;
                case OCG_CounterType.FireCracker:
                    return CounterType.FireCracker;
                case OCG_CounterType.Defect:
                    return CounterType.Defect;
                case OCG_CounterType.LinkbeltWallDragon:
                    return CounterType.LinkbeltWallDragon;
                case OCG_CounterType.Scale:
                    return CounterType.Scale;
                case OCG_CounterType.GoukiCageMatch:
                    return CounterType.GoukiCageMatch;
                case OCG_CounterType.LimitCode:
                    return CounterType.LimitCode;
                case OCG_CounterType.LinkTurret:
                    return CounterType.LinkTurret;
                case OCG_CounterType.Patrol:
                    return CounterType.Patrol;
                case OCG_CounterType.Athlete:
                    return CounterType.Athlete;
                case OCG_CounterType.Otoshidamashi:
                    return CounterType.Otoshidamashi;
                case OCG_CounterType.RisingSunAlternative:
                    return CounterType.RisingSunAlternative;
                case OCG_CounterType.Borrel:
                    return CounterType.Borrel;
                case OCG_CounterType.Summon:
                    return CounterType.Summon;
                case OCG_CounterType.Signal:
                    return CounterType.Signal;
                case OCG_CounterType.Venemy:
                    return CounterType.Venemy;
                case OCG_CounterType.Turntrooper:
                    return CounterType.Turntrooper;
                case OCG_CounterType.BattlewaspNest:
                    return CounterType.BattlewaspNest;
                case OCG_CounterType.FirewallDragonDarkfluid:
                    return CounterType.FirewallDragonDarkfluid;
                case OCG_CounterType.SeraphimPapillon:
                    return CounterType.SeraphimPapillon;
                case OCG_CounterType.CauldronOfTheOldMan:
                    return CounterType.CauldronOfTheOldMan;
                case OCG_CounterType.WorldLegacysContinuation:
                    return CounterType.WorldLegacysContinuation;
                case OCG_CounterType.PendulumOfSouls:
                    return CounterType.PendulumOfSouls;
                case OCG_CounterType.FireFist:
                    return CounterType.FireFist;
                case OCG_CounterType.Phantasm:
                    return CounterType.Phantasm;
                case OCG_CounterType.HinezumiHanabi:
                    return CounterType.HinezumiHanabi;
                case OCG_CounterType.UrsarcticBigDipper:
                    return CounterType.UrsarcticBigDipper;
                case OCG_CounterType.WarRockOrdeal:
                    return CounterType.WarRockOrdeal;
                case OCG_CounterType.SacredScrollsOfTheGizmekLegend:
                    return CounterType.SacredScrollsOfTheGizmekLegend;
                case OCG_CounterType.EmperorsKey:
                    return CounterType.EmperorsKey;
                case OCG_CounterType.Burnup:
                    return CounterType.Burnup;
                case OCG_CounterType.LifeShaver:
                    return CounterType.LifeShaver;
                case OCG_CounterType.UrsarcticRadiation:
                    return CounterType.UrsarcticRadiation;
                case OCG_CounterType.Piece:
                    return CounterType.Piece;
                case OCG_CounterType.PrisonerOfDestiny:
                    return CounterType.PrisonerOfDestiny;
                case OCG_CounterType.GGolem:
                    return CounterType.GGolem;
                case OCG_CounterType.Suship:
                    return CounterType.Suship;

                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }

        public static Location ToLocation(this OCG_CardLocation value)
        {
            return value switch
            {
                OCG_CardLocation.Unknown => Location.Unknown,
                _ => (Location)(uint)value
            };
        }

        public static CardPosition ToCardPosition(this OCG_CardPosition value)
        {
            return value switch
            {
                _ => (CardPosition)(uint)value
            };
        }

        public static DuelCardHint ToDuelCardHint(this OCG_CardHint value)
        {
            return (DuelCardHint)(int)value;
        }

        public static FieldZones ToFieldZone(this OCG_Zone value)
        {
            switch (value)
            {
                case OCG_Zone.Monster0:
                    return FieldZones.Monster0;

                case OCG_Zone.Monster1:
                    return FieldZones.Monster1;

                case OCG_Zone.Monster2:
                    return FieldZones.Monster2;

                case OCG_Zone.Monster3:
                    return FieldZones.Monster3;

                case OCG_Zone.Monster4:
                    return FieldZones.Monster4;

                case OCG_Zone.ExtraMonsterZone0:
                    return FieldZones.ExtraMonsterZone0;

                case OCG_Zone.ExtraMonsterZone1:
                    return FieldZones.ExtraMonsterZone1;

                case OCG_Zone.SpellTrap0:
                    return FieldZones.SpellTrap0;

                case OCG_Zone.SpellTrap1:
                    return FieldZones.SpellTrap1;

                case OCG_Zone.SpellTrap2:
                    return FieldZones.SpellTrap2;

                case OCG_Zone.SpellTrap3:
                    return FieldZones.SpellTrap3;

                case OCG_Zone.SpellTrap4:
                    return FieldZones.SpellTrap4;

                case OCG_Zone.FieldZone:
                    return FieldZones.FieldZone;

                case OCG_Zone.Pendulum0:
                    return FieldZones.Pendulum0;

                case OCG_Zone.Pendulum1:
                    return FieldZones.Pendulum1;

                case OCG_Zone.OpponentMonster0:
                    return FieldZones.OpponentMonster0;

                case OCG_Zone.OpponentMonster1:
                    return FieldZones.OpponentMonster1;

                case OCG_Zone.OpponentMonster2:
                    return FieldZones.OpponentMonster2;

                case OCG_Zone.OpponentMonster3:
                    return FieldZones.OpponentMonster3;

                case OCG_Zone.OpponentMonster4:
                    return FieldZones.OpponentMonster4;

                case OCG_Zone.OpponentExtraMonsterZone0:
                    return FieldZones.OpponentExtraMonsterZone0;

                case OCG_Zone.OpponentExtraMonsterZone1:
                    return FieldZones.OpponentExtraMonsterZone1;

                case OCG_Zone.OpponentSpellTrap0:
                    return FieldZones.OpponentSpellTrap0;

                case OCG_Zone.OpponentSpellTrap1:
                    return FieldZones.OpponentSpellTrap1;

                case OCG_Zone.OpponentSpellTrap2:
                    return FieldZones.OpponentSpellTrap2;

                case OCG_Zone.OpponentSpellTrap3:
                    return FieldZones.OpponentSpellTrap3;

                case OCG_Zone.OpponentSpellTrap4:
                    return FieldZones.OpponentSpellTrap4;

                case OCG_Zone.OpponentFieldZone:
                    return FieldZones.OpponentFieldZone;

                case OCG_Zone.OpponentPendulum0:
                    return FieldZones.OpponentPendulum0;

                case OCG_Zone.OpponentPendulum1:
                    return FieldZones.OpponentPendulum1;

                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }

        public static SystemStrings ToSystemStrings(this OCG_GameStrings value)
        {
            return (SystemStrings)(ulong)value;
        }

        public static SystemReason ToSystemReason(this OCG_Reason value)
        {
            return (SystemReason)(uint)value;
        }
    }
}