using System.Text.RegularExpressions;
using WebSiteElectronicMind.ML.Format.Abstractions;

namespace WebSiteElectronicMind.ML.Format.ClassFormat
{
    public class RubNameFormat : IRubNameFormat
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
            var replacements = Regex.IsMatch(relevantPart, @"^(GТS|AVN|НSD100S|НSD63|MD|МSD|NН4|NХНВ 125|SWN|ВМ63Р|ВМ63РL|НGМ100NА|НGМ125NА|НGМ250NА|НGМ400NА|НGМ630NА|НGМ800NА|НGР125DNА|НGР160DNА|НGР250NА-G|НGР400NА|НGР630NА|НGР800NА|Сitу9 ВН)$")
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
        public string ApplyFormatRubName(string input)
        {
            input = StripSpaces(input);
            input = ReplacePAfterDigit(input);
            input = ReplaceAAfterDigit(input);
            input = ReplaceS(input);

            return input;
        }
    }
}
