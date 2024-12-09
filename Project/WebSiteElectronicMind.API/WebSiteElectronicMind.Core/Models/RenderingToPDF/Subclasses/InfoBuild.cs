using CSharpFunctionalExtensions;

namespace WebSiteElectronicMind.Core.Models.RenderingToPDF.Subclasses
{
    public class InfoBuild
    {
        private InfoBuild(string fullNameEngineer, int numberOrderCustomer, string numberBuild)
        {
            FullNameEngineer = fullNameEngineer;
            NumberOrderCustomer = numberOrderCustomer;
            NumberBuild = numberBuild;
        }

        public string FullNameEngineer { get; }             // ФИО ответственного инженера
        public int NumberOrderCustomer { get; }             // Номер заказа клиента 
        public string NumberBuild { get; }                  // Номер сборки

        public static Result<InfoBuild> Create(string fullNameEngineer, int numberOrderCustomer, string numberBuild)
        {
            if (string.IsNullOrEmpty(fullNameEngineer))
            {
                return Result.Failure<InfoBuild>($"'{nameof(fullNameEngineer)}' cannot be null or empty");
            }

            if (numberOrderCustomer <= 0)
            {
                return Result.Failure<InfoBuild>($"'{nameof(numberOrderCustomer)}' cannot be null or empty");
            }

            if (string.IsNullOrEmpty(numberBuild))
            {
                return Result.Failure<InfoBuild>($"'{nameof(numberBuild)}' cannot be null or empty");
            }

            var infoBuild = new InfoBuild(fullNameEngineer, numberOrderCustomer, numberBuild);

            return Result.Success(infoBuild);
        }
    }
}
