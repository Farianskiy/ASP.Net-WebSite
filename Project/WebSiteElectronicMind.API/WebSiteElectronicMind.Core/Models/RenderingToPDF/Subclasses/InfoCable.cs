using CSharpFunctionalExtensions;

namespace WebSiteElectronicMind.Core.Models.RenderingToPDF.Subclasses
{
    public class InfoCable
    {
        private InfoCable(string supplyCable, string cableOL)
        {
            SupplyCable = supplyCable;
            CableOL = cableOL;
        }

        public string SupplyCable { get; }            // Ввод питающего кабеля
        public string CableOL { get; }                // Ввод кабелей ОЛ

        public static Result<InfoCable> Create(string supplyCable, string cableOL)
        {
            if (string.IsNullOrEmpty(supplyCable))
            {
                return Result.Failure<InfoCable>($"'{nameof(supplyCable)}' cannot be null or empty");
            }

            if (string.IsNullOrEmpty(cableOL))
            {
                return Result.Failure<InfoCable>($"'{nameof(cableOL)}' cannot be null or empty");
            }

            var infoCable = new InfoCable(supplyCable, cableOL);

            return Result.Success(infoCable);
        }
    }
}
