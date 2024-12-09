using CSharpFunctionalExtensions;

namespace WebSiteElectronicMind.Core.Models.RenderingToPDF.Subclasses
{
    public class InfoOmentum
    {
        private InfoOmentum(string typeOmentum, string quantityOmentum, string typeOmentumOL, string quantityOmentumOL)
        {
            TypeOmentum = typeOmentum;
            QuantityOmentum = quantityOmentum;
            TypeOmentumOL = typeOmentumOL;
            QuantityOmentumOL = quantityOmentumOL;
        }

        public string TypeOmentum { get; }              // Тип сальников для питающего кабеля
        public string QuantityOmentum { get; }          // Количество сальников для питающего кабеля
        public string TypeOmentumOL { get; }            // Тип сальников для кабелей ОЛ
        public string QuantityOmentumOL { get; }        // Количество сальников для кабелей ОЛ

        public static Result<InfoOmentum> Create(string typeOmentum, string quantityOmentum, string typeOmentumOL, string quantityOmentumOL)
        {
            if (string.IsNullOrEmpty(typeOmentum))
            {
                return Result.Failure<InfoOmentum>($"'{nameof(typeOmentum)}' cannot be null or empty");
            }

            if (string.IsNullOrEmpty(quantityOmentum))
            {
                return Result.Failure<InfoOmentum>($"'{nameof(quantityOmentum)}' cannot be null or empty");
            }

            if (string.IsNullOrEmpty(typeOmentumOL))
            {
                return Result.Failure<InfoOmentum>($"'{nameof(typeOmentumOL)}' cannot be null or empty");
            }

            if (string.IsNullOrEmpty(quantityOmentumOL))
            {
                return Result.Failure<InfoOmentum>($"'{nameof(quantityOmentumOL)}' cannot be null or empty");
            }

            var infoOmentum = new InfoOmentum(typeOmentum, quantityOmentum, typeOmentumOL, quantityOmentumOL);

            return Result.Success(infoOmentum);
        }
    }
}
