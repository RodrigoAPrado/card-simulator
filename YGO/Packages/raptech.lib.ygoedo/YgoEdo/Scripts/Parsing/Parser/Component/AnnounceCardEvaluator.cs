using System.Collections.Generic;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Struct;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser.Component
{
    public class AnnounceCardEvaluator
    {
        // Pilha para armazenar resultados booleanos (para AND, OR, NOT)
        private readonly Stack<bool> _boolStack = new();

        // Pilha para armazenar valores numéricos (para ISCODE, ISTYPE, etc)
        private readonly Stack<ulong> _valueStack = new();

        public bool IsCardValid(OCG_CardData card, List<ulong> options)
        {
            _boolStack.Clear();
            _valueStack.Clear();

            foreach (var opt in options)
                // Verifica se é um Opcode (tem o bit 0x4000... ligado)
                if ((opt & 0x4000000000000000) != 0)
                    ExecuteOpcode((OCG_AnnounceCardOpcode)opt, card);
                else
                    // Se não for opcode, é um valor puro (ID, Atributo, etc)
                    _valueStack.Push(opt);

            // No final, o resultado da expressão lógica está no topo da pilha de bools
            return _boolStack.Count > 0 ? _boolStack.Pop() : true;
        }

        private void ExecuteOpcode(OCG_AnnounceCardOpcode op, OCG_CardData card)
        {
            switch (op)
            {
                // --- Verificadores (Tiram um valor e comparam com a carta) ---
                case OCG_AnnounceCardOpcode.IsCode:
                    _boolStack.Push(card.code == (uint)_valueStack.Pop());
                    break;
                case OCG_AnnounceCardOpcode.IsType:
                    _boolStack.Push((card.type & (uint)_valueStack.Pop()) != 0);
                    break;
                case OCG_AnnounceCardOpcode.IsAttribute:
                    _boolStack.Push((card.attribute & (uint)_valueStack.Pop()) != 0);
                    break;
                case OCG_AnnounceCardOpcode.IsRace:
                    _boolStack.Push((card.race & _valueStack.Pop()) != 0);
                    break;

                // --- Operadores Lógicos (Combinam resultados da boolStack) ---
                case OCG_AnnounceCardOpcode.Or:
                    if (_boolStack.Count >= 2)
                        _boolStack.Push(_boolStack.Pop() | _boolStack.Pop());
                    break;
                case OCG_AnnounceCardOpcode.And:
                    if (_boolStack.Count >= 2)
                        _boolStack.Push(_boolStack.Pop() & _boolStack.Pop());
                    break;
                case OCG_AnnounceCardOpcode.Not:
                    if (_boolStack.Count >= 1)
                        _boolStack.Push(!_boolStack.Pop());
                    break;

                // --- Getters (Pegam dados da carta e põem na valueStack) ---
                case OCG_AnnounceCardOpcode.GetCode:
                    _valueStack.Push(card.code);
                    break;
                case OCG_AnnounceCardOpcode.GetType:
                    _valueStack.Push(card.type);
                    break;

                // Flags de permissão geralmente são tratadas antes do loop de filtragem
                case OCG_AnnounceCardOpcode.AllowAliases:
                case OCG_AnnounceCardOpcode.AllowTokens:
                    break;
            }
        }
    }
}