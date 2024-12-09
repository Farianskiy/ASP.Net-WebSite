using System.Text.RegularExpressions;
using WebSiteElectronicMind.ML.Format.Abstractions;

namespace WebSiteElectronicMind.ML.Format.ClassFormat
{
    public class DifNameFormat : IDifNameFormat
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

        // Метод для замены кириллической(русских) буквы после числа и 'A'/'А' на соответствующую латиницу(английских)
        static string ReplaceCyrillicAfter2A(string input)
        {
            return Regex.Replace(input, @"(\d+(?:,\d+)?)(A|А)\s*([A-ZА-Я])", m =>
            {
                var digitA = m.Groups[1].Value + m.Groups[2].Value;
                var cyrillicLetter = m.Groups[3].Value;

                var cyrillicToLatin = new Dictionary<string, string>
            {
                { "А", "А" }, { "С", "C" }, { "В", "B" }, { "К", "K" }, { "Е", "E"}
            };

                var englishLetter = cyrillicToLatin.ContainsKey(cyrillicLetter) ? cyrillicToLatin[cyrillicLetter] : cyrillicLetter;
                return digitA + " " + englishLetter;
            });
        }

        // Заменяет 'mA' или 'мА' на "мА".
        static string ReplaceMA(string input)
        {
            return Regex.Replace(input, @"[mм][AА]", "мА");
        }

        // Заменяет 'A' на 'А' после символов 'кА' и заменяет 'A' на 'АС', если за 'A' следует 'C'.
        static string ReplaceACOrAAfterKA(string input)
        {
            input = Regex.Replace(input, @"(кА.*?)(A\b)", "$1А");
            return Regex.Replace(input, @"(кА.*?)(A\s*C)", "$1АС");
        }

        // Извлекает ту часть строки, которая идет до первой буквы 'п' или 'А'
        static string ExtractRelevantPart(string input)
        {
            var match = Regex.Match(input, @"^(.*?)(?=\s*\d+[,.\d]*[пА])");
            return match.Success ? match.Groups[1].Value.Trim() : input;
        }

        // Выполняет замену букв с использованием подходящих символов (русских или латинских) в зависимости от контекста.
        static string ReplaceS(string input)
        {
            var toEnglish = new Dictionary<char, char>
        {
            {'с', 'c'}, {'С', 'C'}, {'е', 'e'}, {'Е', 'E'}, {'а', 'a'}, {'А', 'A'},
            {'о', 'o'}, {'О', 'O'}, {'р', 'p'}, {'Р', 'P'}, {'х', 'x'}, {'у', 'y'},
            {'К', 'K'}, {'М', 'M'}, {'Т', 'T'}, {'В', 'B'}, {'Н', 'H'}, {'Х', 'X'}
        };

            var toRussian = new Dictionary<char, char>();
            foreach (var kvp in toEnglish)
            {
                toRussian[kvp.Value] = kvp.Key;
            }

            var relevantPart = ExtractRelevantPart(input);
            var replacements = Regex.IsMatch(relevantPart, @"^(АВДТ 32ML|B06S|DVA 6|NB1L 40|NB2LE|NBH8LE 40|NB1L 63|NB1L|HRO63P|HRO63M|VD63|D63|АД 2 S|ДИФ 101S)$")
                ? toEnglish
                : toRussian;


            var replacedPart = string.Concat(relevantPart.Select(c => replacements.ContainsKey(c) ? replacements[c] : c));

            string ReplaceBetween20AAnd6KA(Match m)
            {
                var prefix = m.Groups[1].Value;
                var current = m.Groups[2].Value;
                var suffix = m.Groups[3].Value;
                current = string.Concat(current.Select(c => toEnglish.ContainsKey(c) ? toEnglish[c] : c));
                return prefix + current + suffix;
            }

            var pattern = @"(\d+А\s*)([^\d\s]+)(\s*\d*кА)";
            input = Regex.Replace(replacedPart + input.Substring(relevantPart.Length), pattern, new MatchEvaluator(ReplaceBetween20AAnd6KA));

            return input;
        }

        // Метод для применения всех преобразований в указанном порядке
        public string ApplyFormatDifName(string input)
        {
            input = StripSpaces(input);
            input = ReplacePAfterDigit(input);
            input = ReplaceAAfterDigit(input);
            input = ReplaceSmallK(input);
            input = ReplaceCyrillicAfter2A(input);
            input = ReplaceMA(input);
            input = ReplaceACOrAAfterKA(input);
            input = ReplaceS(input);

            return input;
        }
    }
}
