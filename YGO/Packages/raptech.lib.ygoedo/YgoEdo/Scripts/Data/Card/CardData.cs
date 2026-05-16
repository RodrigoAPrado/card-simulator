using System;
using System.Collections.Generic;
using System.Linq;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Struct;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Data.Card
{
    public class CardData : ICardData
    {
        private static readonly CardData EmptyData = new(new OCG_CardData(), "Monster", "", new List<string>(), 0);

        public CardData(OCG_CardData data, string name, string description, List<string> strings, ulong category)
        {
            Data = data;
            Name = name;
            Description = description;
            Strings = strings;
            Category = category;
            Type = ((OCG_MonsterRaces)data.race).ToMonsterType();
            LinkArrows = SetupLinkArrows();
            Types = SetupTypes();
            Categories = SetupCategories();
            Frame = SetupCardFrame();
            CardAttribute = SetupCardIcon();
        }

        public OCG_CardData Data { get; }
        public ulong Category { get; }
        public string Name { get; }
        public string Description { get; }
        public IReadOnlyList<string> Strings { get; }
        public uint Code => Data.code;
        public uint Alias => Data.alias;
        public uint Level => Data.level;
        public MonsterType Type { get; }
        public Frame Frame { get; }
        public CardAttribute CardAttribute { get; }
        public int OriginalAttack => Data.attack;
        public int OriginalDefense => Data.defense;
        public uint LeftScale => Data.lscale;
        public uint RightScale => Data.rscale;
        public IReadOnlyList<LinkArrow> LinkArrows { get; }
        public IReadOnlyList<CardType> Types { get; }
        public IReadOnlyList<SearchCategory> Categories { get; }

        private CardAttribute SetupCardIcon()
        {
            if (Data.attribute != 0)
                return ((OCG_MonsterAttributes)Data.attribute).ToCardAttribute();
            return Frame == Frame.Spell ? CardAttribute.Spell : CardAttribute.Trap;
        }

        private Frame SetupCardFrame()
        {
            if (Types.Contains(CardType.Spell))
                return Frame.Spell;
            if (Types.Contains(CardType.Trap))
                return Frame.Trap;
            if (Types.Contains(CardType.Link))
                return Frame.Link;
            if (Types.Contains(CardType.Token))
                return Frame.Token;
            return GetMonsterFrame(Types.Contains(CardType.Pendulum));
        }

        private Frame GetMonsterFrame(bool isPendulum)
        {
            if (Types.Contains(CardType.Fusion))
                return isPendulum ? Frame.FusionPendulum : Frame.Fusion;
            if (Types.Contains(CardType.Synchro))
                return isPendulum ? Frame.SynchroPendulum : Frame.Synchro;
            if (Types.Contains(CardType.Xyz))
                return isPendulum ? Frame.XyzPendulum : Frame.Xyz;
            if (Types.Contains(CardType.Ritual))
                return isPendulum ? Frame.RitualPendulum : Frame.Ritual;
            if (Types.Contains(CardType.Effect))
                return isPendulum ? Frame.EffectPendulum : Frame.Effect;
            return isPendulum ? Frame.NormalPendulum : Frame.Normal;
        }

        private List<LinkArrow> SetupLinkArrows()
        {
            var links = new List<LinkArrow>();

            foreach (OCG_LinkMarker l in Enum.GetValues(typeof(OCG_LinkMarker)))
            {
                if (l == OCG_LinkMarker.None)
                    continue;
                if (((uint)l & Data.link_marker) == 1)
                    links.Add(l.ToLinkArrow());
            }

            return links;
        }

        private List<CardType> SetupTypes()
        {
            var types = new List<CardType>();

            foreach (OCG_CardType t in Enum.GetValues(typeof(OCG_CardType)))
                if (((uint)t & Data.type) == 1)
                    types.Add(t.ToCardDataType());

            return types;
        }

        private List<SearchCategory> SetupCategories()
        {
            var categories = new List<SearchCategory>();

            foreach (OCG_CardCategory c in Enum.GetValues(typeof(OCG_CardCategory)))
                if (((ulong)c & Category) == 1)
                    categories.Add(c.ToCardSearchCategory());

            if (categories.Count == 0)
                categories.Add(SearchCategory.None);

            return categories;
        }

        public static CardData GetEmpty()
        {
            return EmptyData;
        }
    }
}