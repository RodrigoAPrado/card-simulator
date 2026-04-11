using System;
using Ygo.Core.Abstract;
using Ygo.Core.Board.Abstract;
using Ygo.Core.Enums;
using Ygo.Data;
using Ygo.Data.Enums;

namespace Ygo.Core
{
    public class CardInstance : ICardInstance
    {
        public Guid Id { get; }
        public Guid OwnerId { get; }
        public Guid ControllerId { get; private set; }
        public CardData Data { get; }
        public CardLocation Location { get; private set; }
        public IBoardZone Zone { get; private set; }
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
        public bool CanNormalSummon { get; private set; }
        public bool CanNormalSet { get; private set; }
        public bool CanAttack => !_hasAttacked && IsValidMonster && !IsInDefense;
        public bool IsSummoned => _isSummoned;
        public bool IsFaceDown { get; private set; }
        public bool CanChangePosition => _isSummoned && !_hasChangedPosition;
        public bool IsInDefense => IsFaceDown || _isInDefense;
        public bool IsDestroyed => IsDestroyedByBattle || IsDestroyedByCardEffect;
        public bool IsDestroyedByBattle { get; private set; }
        public bool IsDestroyedByCardEffect { get; private set; }

        private int LevelModifier;
        private int AtkModifier;
        private int DefModifier;
        private bool _isInDefense;
        private bool _hasChangedPosition;
        private bool _isSummoned;
        private bool _hasAttacked;

        public CardInstance(CardData data, Guid ownerId)
        {
            Id = Guid.NewGuid();
            OwnerId = ownerId;
            ControllerId = ownerId;
            Data = data;
        }

        public void Summon(IBoardZone zone)
        {
            Zone = zone;
            _isSummoned = true;
            _hasChangedPosition = true;
            _hasAttacked = false;
            IsFaceDown = false;
            _isInDefense = false;
            Location = zone.Position.ToMonsterCardLocation();
            CanNormalSummon = false;
            CanNormalSet = false;
        }

        public void Set(IBoardZone zone)
        {
            Zone = zone;
            _isSummoned = false;
            _hasChangedPosition = true;
            _hasAttacked = false;
            IsFaceDown = true;
            _isInDefense = true;
            Location = zone.Position.ToMonsterCardLocation();
            CanNormalSummon = false;
            CanNormalSet = false;
        }

        public void AddToHand()
        {
            Location = CardLocation.Hand;
            _isSummoned = false;
            _hasChangedPosition = false;
            _hasAttacked = false;
            IsFaceDown = false;
            _isInDefense = false;
            IsDestroyedByBattle = false;
            IsDestroyedByCardEffect = false;
            if (Data.CardType == CardType.Monster)
            {
                CanNormalSummon = true;
                CanNormalSet = true;
            }
        }

        public void AddToMainDeck()
        {
            Location = CardLocation.MainDeck;
            _isSummoned = false;
            _hasChangedPosition = false;
            _hasAttacked = false;
            IsFaceDown = false;
            _isInDefense = false;
            IsDestroyedByBattle = false;
            IsDestroyedByCardEffect = false;
            CanNormalSummon = false;
            CanNormalSet = false;
        }

        public void SetAttacked()
        {
            _hasAttacked = true;
        }

        public void Flip()
        {
            if (!_isInDefense || !IsFaceDown)
            {
                throw new InvalidOperationException($"Cannot flip card because of FaceDown:{IsFaceDown} or Defense:{IsInDefense}");
            }
            _isSummoned = true;
            _hasChangedPosition = true;
            _hasAttacked = false;
            IsFaceDown = false;
        }

        public void PassTurn()
        {
            _hasAttacked = false;
            _hasChangedPosition = false;
        }

        public void ChangePosition(bool defense)
        {
            if(_hasChangedPosition)
                throw new InvalidOperationException("Cannot change position.");
            
            _isInDefense = defense;
            _hasChangedPosition = true;
        }

        public void DestroyByBattle()
        {
            IsDestroyedByBattle = true;
        }

        public void DestroyByCardEffect()
        {
            IsDestroyedByCardEffect = true;
        }

        public void SendToGraveyard()
        {
            Location = CardLocation.Graveyard;
            Zone = null;
            _isSummoned = false;
            _hasChangedPosition = false;
            _hasAttacked = false;
            IsFaceDown = false;
            _isInDefense = false;
            CanNormalSummon = false;
            CanNormalSet = false;
        }

        public void ClearDestroyed()
        {
            IsDestroyedByBattle = false;
            IsDestroyedByCardEffect = false;
        }
    }
}