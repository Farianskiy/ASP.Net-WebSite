using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebSiteElectronicMind.ML.Format.Abstractions;

namespace WebSiteElectronicMind.ML.Format.ClassFormat
{
    public class PowerNameFormat : IPowerNameFormat
    {
        // Удаляет лишние пробелы между словами, оставляя только один пробел.
        static string StripSpaces(string input)
        {
            return string.Join(" ", input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
        }

        // Заменяет букву 'P', 'p' или 'П' только после одной цифры (включая пробелы) на русскую букву 'п'.
        static string ReplacePAfterDigit(string input)
        {
            return Regex.Replace(input, @"(?<!\d)(\d)\s*[pPП]", m => m.Groups[1].Value + "п");
        }

        // Заменяет латинскую букву 'A', стоящую после цифры (1A, 2A, 3A,), на русскую 'А'.
        static string ReplaceAAfterDigit(string input)
        {
            return Regex.Replace(input, @"(\d+)[Aaа]", m => m.Groups[1].Value + "А");
        }

        // Заменяет маленькую латинскую или русскую букву 'k'/'к' (4,5кА, 6кА), стоящую перед 'A'/'А', на "кА".
        static string ReplaceSmallK(string input)
        {
            return Regex.Replace(input, @"[kк][AА]", "кА");
        }

        // Метод для применения всех преобразований в указанном порядке
        public string ApplyFormatPowerName(string input)
        {
            input = StripSpaces(input);
            input = ReplacePAfterDigit(input);
            input = ReplaceAAfterDigit(input);
            input = ReplaceSmallK(input);

            return input;
        }
    }
}
