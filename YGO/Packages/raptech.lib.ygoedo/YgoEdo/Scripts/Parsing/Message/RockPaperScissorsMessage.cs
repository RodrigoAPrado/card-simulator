using System;
using System.Collections.Generic;
using System.Text;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.System.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using IRockPaperScissorsMessage = YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.IRockPaperScissorsMessage;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class RockPaperScissorsMessage : ISelectionOcgMessage, IRockPaperScissorsMessage
    {
        public InputType Input => InputType.Value;

        public int InputCount => 3;
        public byte Player { get; }

        public RockPaperScissorsMessage(byte player)
        {
            Player = player;
        }
        public byte[] GetResponse(List<int> input)
        {
            if (input.Count != 1)
                return Array.Empty<byte>();

            var id = input[0];
            
            return !Enum.IsDefined(typeof(RockPaperScissors), id) 
                ? Array.Empty<byte>() 
                : BitConverter.GetBytes(id);
        }
        
        public bool CanCancel => false;
        public byte[] Cancel()
        {
            return Array.Empty<byte>();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"(RockPapperScissors) Player: {Player}:");
            foreach (RockPaperScissors value in Enum.GetValues(typeof(RockPaperScissors)))
            {
                sb.AppendLine($"[{(int)value}] => {value.ToString()}");
            }
            return sb.ToString();
        }
    }
}