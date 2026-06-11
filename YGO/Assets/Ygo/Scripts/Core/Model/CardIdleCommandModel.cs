using System;
using Ygo.Scripts.Core.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Flag;

namespace Ygo.Scripts.Core.Model
{
    public class CardIdleCommandModel
    {
        public uint CardCode { get; }
        public PointOfView PointOfView { get; }
        public uint Sequence { get; }
        public Location Location { get; }
        
        public int NormalSummon { get; private set; }
        public int SpecialSummon { get; private set;}
        public int ChangeCardPosition { get; private set;}
        public int Set { get; private set;}
        public int SpellOrTrapSet { get; private set;}
        public int EffectActivation { get; private set;}
        public string EffectDescription { get; private set;}

        public CardIdleCommandModel(uint cardCode, PointOfView pointOfView, uint sequence, Location location)
        {
            CardCode = cardCode;
            PointOfView = pointOfView;
            Sequence = sequence;
            Location = location;

            NormalSummon = -1;
            SpecialSummon = -1;
            ChangeCardPosition = -1;
            Set = -1;
            SpellOrTrapSet = -1;
            EffectActivation = -1;
            EffectDescription = string.Empty;
        }

        public static void AddNormalSummon(CardIdleCommandModel model, int value)
        {
            if(model.NormalSummon != -1)
                throw new InvalidOperationException("NormalSummon is already set");
            model.NormalSummon = value;
        }

        public static void AddSpecialSummon(CardIdleCommandModel model, int value)
        {
            if(model.SpecialSummon != -1)
                throw new InvalidOperationException("SpecialSummon is already set");
            model.SpecialSummon = value;
        }

        public static void AddChangeCardPosition(CardIdleCommandModel model, int value)
        {
            if(model.ChangeCardPosition != -1)
                throw new InvalidOperationException("ChangeCardPosition is already set");
            model.ChangeCardPosition = value;
        }

        public static void AddSet(CardIdleCommandModel model, int value)
        {
            if(model.Set != -1)
                throw new InvalidOperationException("Set is already set");
            model.Set = value;
        }

        public static void AddSpellOrTrapSet(CardIdleCommandModel model, int value)
        {
            if(model.SpellOrTrapSet != -1)
                throw new InvalidOperationException("SpellOrTrapSet is already set");
            model.SpellOrTrapSet = value;
        }

        public static void AddEffectActivation(CardIdleCommandModel model, int value, string description)
        {
            if(model.EffectActivation != -1)
                throw new InvalidOperationException("EffectActivation is already set");
            model.EffectActivation = value;
            model.EffectDescription = description;
        }
    }
}


/*
 *
 *
        NormalSummon = 0,
        SpecialSummon = 1,
        ChangeCardPosition = 2,
        Set = 3,
        SpellOrTrapSet = 4,
        EffectActivation = 5,
        GoToBattlePhase = 6,
        GotoEndPhase = 7,
        ShuffleHand = 8
 *
 *
 */