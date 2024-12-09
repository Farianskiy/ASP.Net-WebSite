using CSharpFunctionalExtensions;
using System.Text.Json.Serialization;

namespace WebSiteElectronicMind.Core.Models.RenderingToPDF
{
    public class TablePDF
    {
        [JsonConstructor]
        private TablePDF(string name, string nameOfScheme, string type, string numberingLetter, string numberingDigit, string phase, int polus, int level, string numberBuild)
        {
            Name = name;
            NameOfScheme = nameOfScheme;
            Type = type;
            NumberingLetter = numberingLetter;
            NumberingDigit = numberingDigit;
            Phase = phase;
            Polus = polus;
            Level = level;
            NumberBuild = numberBuild;
        }

        public string Name { get; }
        public string NameOfScheme { get; }
        public string Type { get; }
        public string NumberingLetter { get; }
        public string NumberingDigit { get; } = string.Empty;
        public string Phase { get; } = string.Empty;
        public int Polus { get; }
        public int Level { get; }
        public string NumberBuild { get; }

        public static Result<TablePDF> Create(string name, string nameOfScheme, string type, string numberingLetter, string numberingDigit, string phase, int polus, int level, string numberBuild)
        {
            if (string.IsNullOrEmpty(name))
            {
                return Result.Failure<TablePDF>($"'{nameof(name)}' cannot be null or empty");
            }

            if (string.IsNullOrEmpty(nameOfScheme))
            {
                return Result.Failure<TablePDF>($"'{nameof(nameOfScheme)}' cannot be null or empty");
            }

            if (string.IsNullOrEmpty(type))
            {
                return Result.Failure<TablePDF>($"'{nameof(type)}' cannot be null or empty");
            }

            if (string.IsNullOrEmpty(numberingLetter))
            {
                return Result.Failure<TablePDF>($"'{nameof(numberingLetter)}' cannot be null or empty");
            }

            if (string.IsNullOrEmpty(numberBuild))
            {
                return Result.Failure<TablePDF>($"'{nameof(numberBuild)}' cannot be null or empty");
            }


            if (level<0)
            {
                return Result.Failure<TablePDF>($"'{nameof(level)}' cannot be null or empty");
            }

            var tablePDF = new TablePDF(name, nameOfScheme, type, numberingLetter, numberingDigit, phase, polus, level, numberBuild);

            return Result.Success(tablePDF);
        }
    }
}
