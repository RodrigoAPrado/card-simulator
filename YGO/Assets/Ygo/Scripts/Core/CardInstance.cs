using System;
using Ygo.Core.Abstract;
using Ygo.Core.Enums;
using Ygo.Data;
using Ygo.Data.Enums;

namespace Ygo.Core
{
    public class CardInstance : ICardInstance
    {
        public Guid Id { get; }
        public CardData Data { get; }
        public CardLocation Location { get; private set; }
        public int? CurrentLevel => Data.MonsterData?.Level + LevelModifier;
        public int? CurrentAtk => Data.MonsterData?.Atk + AtkModifier;
        public int? CurrentDef => Data.MonsterData?.Def + DefModifier;
        public bool IsValidMonster => Data.CardType == CardType.Monster && Data.MonsterData != null;
        public bool IsValidSpell => Data.CardType == CardType.Spell && Data.SpellData != null;
        public bool IsPendulum => Data.MonsterData?.Kinds?.Contains(MonsterKind.Pendulum) == true;
        public bool IsRitual => Data.MonsterData?.Kinds?.Contains(MonsterKind.Ritual) == true;
        public bool IsFusion => Data.MonsterData?.Kinds?.Contains(MonsterKind.Fusion) == true;
        public bool IsSynchro => Data.MonsterData?.Kinds?.Contains(MonsterKind.Synchro) == true;
        public bool IsXyz => Data.MonsterData?.Kinds?.Contains(MonsterKind.Xyz) == true;
        public bool IsLink => Data.MonsterData?.Kinds?.Contains(MonsterKind.Link) == true;
        public bool IsEffect => Data.MonsterData?.Kinds?.Contains(MonsterKind.Effect) == true;
        public string CardText => Data.MonsterData?.FlavorText;
        public bool TreatedAsSpell { get; private set; }
        public bool TreatedAsTrap { get; private set; }
        public bool TreatedAsMonster { get; private set; }
        public bool IsField => Data.CardType == CardType.Spell && Data.SpellData is { Type: SpellType.Field };
        private int LevelModifier { get; } = 0;
        private int AtkModifier { get; } = 0;
        private int DefModifier { get; } = 0;

        public CardInstance(CardData data)
        {
            Id = Guid.NewGuid();
            Data = data;
        }
        
        public void SetLocation(CardLocation location)
        {
            Location = location;
        }
    }
}