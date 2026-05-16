using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser.Abstr;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser
{
    public class UnknownParser : BaseParser
    {
        protected override IOcgMessage DoParse(byte[] buffer)
        {
            return new UnknownMessage(buffer);
        }
    }
}