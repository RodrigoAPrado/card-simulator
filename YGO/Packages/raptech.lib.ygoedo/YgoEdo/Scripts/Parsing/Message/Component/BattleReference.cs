using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component
{
    public class BattleReference : IBattleReference
    {
        private readonly FullLocationReference _locationReference;

        public BattleReference(FullLocationReference locationReference, uint atk, uint def, bool destroyed)
        {
            _locationReference = locationReference;
            Atk = atk;
            Def = def;
            Destroyed = destroyed;
        }

        public IFullLocationReference LocationReference => _locationReference;
        public uint Atk { get; }
        public uint Def { get; }
        public bool Destroyed { get; }

        public override string ToString()
        {
            return $"[LocationReference={_locationReference}, Atk={Atk}, Def={Def}, Destroyed={Destroyed}]";
        }
    }
}