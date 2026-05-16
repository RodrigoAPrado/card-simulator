using System.Collections.Generic;
using System.Text;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Query.Component;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Query
{
    public class FieldQuery
    {
        private FieldQuery()
        {
        }

        public IReadOnlyDictionary<byte, PlayerInfo> PlayerInfos { get; private set; }
        public IReadOnlyList<FieldQueryChain> Chain { get; private set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var p in PlayerInfos) sb.AppendLine($"Player={p.Key}, Info=[{p.Value}]");

            sb.AppendLine($"ChainSize={Chain.Count}");
            foreach (var c in Chain) sb.AppendLine($"Chain=[{c}]");

            return sb.ToString();
        }

        public class Builder
        {
            private readonly List<FieldQueryChain> _chains = new();

            private readonly Dictionary<byte, PlayerInfo> _playerInfo = new();

            private Builder()
            {
            }

            public static Builder Create()
            {
                return new Builder();
            }

            public Builder AddPlayerInfo(byte player, PlayerInfo info)
            {
                _playerInfo.Add(player, info);
                return this;
            }

            public Builder AddFieldQueryChain(List<FieldQueryChain> chain)
            {
                _chains.AddRange(chain);
                return this;
            }

            public FieldQuery Build()
            {
                var query = new FieldQuery
                {
                    PlayerInfos = _playerInfo,
                    Chain = _chains
                };
                return query;
            }
        }
    }
}