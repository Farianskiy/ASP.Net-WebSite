using CSharpFunctionalExtensions;

namespace WebSiteElectronicMind.Core.Models.RenderingToPDF.Subclasses
{
    public class InfoShield
    {
        private InfoShield(string nameShield, string fullNameShield, string typeShield)
        {
            NameShield = nameShield;
            FullNameShield = fullNameShield;
            TypeShield = typeShield;
        }

        public string NameShield { get; }             // Наименование щита сокращенное
        public string FullNameShield { get; }         // Наименование щита полное
        public string TypeShield { get; }             // Тип щита

        public static Result<InfoShield> Create(string nameShield, string fullNameShield, string typeShield)
        {
            if (string.IsNullOrEmpty(nameShield))
            {
                return Result.Failure<InfoShield>($"'{nameof(nameShield)}' cannot be null or empty");
            }

            if (string.IsNullOrEmpty(fullNameShield))
            {
                return Result.Failure<InfoShield>($"'{nameof(fullNameShield)}' cannot be null or empty");
            }

            if (string.IsNullOrEmpty(typeShield))
            {
                return Result.Failure<InfoShield>($"'{nameof(typeShield)}' cannot be null or empty");
            }

            var infoShield = new InfoShield(nameShield, fullNameShield, typeShield);

            return Result.Success(infoShield);
        }
    }
}
