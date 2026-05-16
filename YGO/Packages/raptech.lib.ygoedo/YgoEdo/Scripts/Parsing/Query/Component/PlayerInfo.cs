using System.Collections.Generic;
using System.Text;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Query.Component
{
    public class PlayerInfo
    {
        public PlayerInfo(
            uint lp,
            Dictionary<int, FieldQueryCard> cards,
            uint mainSize,
            uint handSize,
            uint graveSize,
            uint removeSize,
            uint extraSize,
            uint extraCount
        )
        {
            Lp = lp;
            Cards = cards;
            MainSize = mainSize;
            HandSize = handSize;
            GraveyardSize = graveSize;
            BanishedSize = removeSize;
            ExtraSize = extraSize;
            ExtraCount = extraCount;
        }

        public uint Lp { get; }
        public Dictionary<int, FieldQueryCard> Cards { get; }
        public uint MainSize { get; }
        public uint HandSize { get; }
        public uint GraveyardSize { get; }
        public uint BanishedSize { get; }
        public uint ExtraSize { get; }
        public uint ExtraCount { get; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"Lp={Lp},");
            foreach (var c in Cards) sb.Append($"Card=[Index={c.Key}, Info=[{c.Value}]],");
            sb.Append($"MainSize={MainSize}, " +
                      $"HandSize={HandSize}, " +
                      $"GraveSize={GraveyardSize}, " +
                      $"BanishedSize={BanishedSize}," +
                      $" ExtraSize={ExtraSize}, " +
                      $"ExtraCount={ExtraCount}");

            return sb.ToString();
        }
    }
}