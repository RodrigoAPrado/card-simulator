using System;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Enum;

namespace Ygo.Scripts.Data
{
    public class DuelData
    {
        public DuelMode DuelMode { get; }
        public DuelistData Duelist0 { get; }
        public DuelistData Duelist1 { get; }


        private DuelData(DuelMode duelMode, DuelistData duelist0, DuelistData duelist1)
        {
            DuelMode = duelMode;
            Duelist0 = duelist0;
            Duelist1 = duelist1;
        }
        
        public static DuelDataBuilder CreateBuilder() => new DuelDataBuilder();
        
        public class DuelDataBuilder
        {
            public DuelMode DuelMode { get; private set; }
            public DuelistData Duelist0 { get; private set; }
            public DuelistData Duelist1 { get; private set; }
            
            public DuelData Build()
            {
                if (Duelist0 == null)
                    throw new InvalidOperationException("Duelist0 cannot be null");
                if (Duelist1 == null)
                    throw new InvalidOperationException("Duelist1 cannot be null");
                
                return new DuelData(DuelMode, Duelist0, Duelist1);
            }
            
            public DuelDataBuilder WithDuelMode(DuelMode value)
            {
                DuelMode = value;
                return this;
            }

            public DuelDataBuilder WithDuelist0(DuelistData value)
            {
                Duelist0 = value;
                return this;
            }

            public DuelDataBuilder WithDuelist1(DuelistData value)
            {
                Duelist1 = value;
                return this;
            }
        }
    }
}