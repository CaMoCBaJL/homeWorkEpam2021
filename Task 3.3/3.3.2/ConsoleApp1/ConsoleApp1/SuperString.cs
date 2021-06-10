
namespace ConsoleApp1
{
    static class SuperString
    {
        readonly public static (int firstSymbol, int lastSymbol) russianSymbolsLowerCase = (1040, 1105);

        readonly public static (int firstSymbol, int lastSymbol) englishSymbolsLowerCase = (65, 122);


        static public Language CheckLanguage(this string s)
        {
            Language result = Language.None;

            foreach (char item in s.ToCharArray())
            {
                if (item.IsEnglish(result))
                    result = Language.English;

                else if (item.IsRussian(result))
                    result = Language.Russsian;

                else if (item.IsNumber(result))
                    result = Language.Number;

                else return Language.Mixed;

            }

            return result;
        }

        static bool IsNumber(this char c, Language result)
        {
            if (char.IsDigit(c))
            {
                if (result == Language.None || result == Language.Russsian)
                    return true;
            }

            return false;
        }

        static bool IsEnglish(this char c, Language result)
        {
            if (CharInGap(c, englishSymbolsLowerCase))
            {
                if (result == Language.None || result == Language.English)
                    return true;
            }

            return false;
        }

        static bool IsRussian(this char c, Language result)
        {
            if (CharInGap(c, russianSymbolsLowerCase))
            {
                if (result == Language.None || result == Language.Russsian)
                    return true;
            }

            return false;
        }

        static bool CharInGap (char c, (int first, int last) gap)
        {
            if (c >= gap.first && c <= gap.last)
                return true;

            return false;
        }
    }
}
