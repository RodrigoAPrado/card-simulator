using System;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Constant;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser.Abstr
{
    public abstract class BaseParser : IParser
    {
        public IOcgMessage SafeParse(byte[] buffer)
        {
            try
            {
                Console.WriteLine("");
                Console.WriteLine($"Raw: {(OCG_GameMessage)buffer[0]} {BitConverter.ToString(buffer)}");
                return DoParse(buffer);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new UnknownMessage(buffer);
            }
        }

        public IOcgMessage Parse(byte[] buffer)
        {
            Console.WriteLine($"Raw: {(OCG_GameMessage)buffer[0]} {BitConverter.ToString(buffer)}");
            return DoParse(buffer);
        }

        protected abstract IOcgMessage DoParse(byte[] buffer);
    }
}