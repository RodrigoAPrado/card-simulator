using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class DamageStepEndMessage : BaseMessage, IDamageStepEndMessage
    {
        public override string ToString()
        {
            return "Ending the Damage Step!";
        }
    }
}