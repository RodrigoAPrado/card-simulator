using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser.Abstr;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser
{
    public class RetryParser : BaseParser
    {
        protected override IOcgMessage DoParse(byte[] buffer)
        {
            return new RetryMessage("Invalid command! Please, try again!");
        }
    }
}