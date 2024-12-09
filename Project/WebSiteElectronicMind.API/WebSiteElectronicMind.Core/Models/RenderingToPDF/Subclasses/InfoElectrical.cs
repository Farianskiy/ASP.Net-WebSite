using CSharpFunctionalExtensions;

namespace WebSiteElectronicMind.Core.Models.RenderingToPDF.Subclasses
{
    public class InfoElectrical
    {
        private InfoElectrical(int nominalVoltage, int nominalShield, string typeGrounding)
        {
            NominalVoltage = nominalVoltage;
            NominalShield = nominalShield;
            TypeGrounding = typeGrounding;
        }

        public int NominalVoltage { get; }            // Номинальное напряжение
        public int NominalShield { get; }             // Номинальный ток щита
        public string TypeGrounding { get; }          // Тип системы заземления

        public static Result<InfoElectrical> Create(int nominalVoltage, int nominalShield, string typeGrounding)
        {
            if (nominalVoltage <= 0)
            {
                return Result.Failure<InfoElectrical>($"'{nameof(nominalVoltage)}' cannot be null or empty");
            }

            if (nominalShield <= 0)
            {
                return Result.Failure<InfoElectrical>($"'{nameof(nominalShield)}' cannot be null or empty");
            }

            if (string.IsNullOrEmpty(typeGrounding))
            {
                return Result.Failure<InfoElectrical>($"'{nameof(typeGrounding)}' cannot be null or empty");
            }

            var infoElectrical = new InfoElectrical(nominalVoltage, nominalShield, typeGrounding);

            return Result.Success(infoElectrical);
        }
    }
}
