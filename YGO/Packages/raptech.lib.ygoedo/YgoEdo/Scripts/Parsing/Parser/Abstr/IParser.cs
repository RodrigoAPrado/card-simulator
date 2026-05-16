using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser.Abstr
{
    public interface IParser
    {
        IOcgMessage SafeParse(byte[] buffer);
        IOcgMessage Parse(byte[] buffer);
    }
}