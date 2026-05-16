using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class DamageStepStartMessage : BaseMessage, IDamageStepStartMessage
    {
        public override string ToString()
        {
            return "Starting the Damage Step!";
        }
    }
}