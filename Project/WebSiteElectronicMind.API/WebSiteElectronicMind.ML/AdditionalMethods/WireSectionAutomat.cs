namespace WebSiteElectronicMind.ML.AdditionalMethods
{
    public class WireSectionAutomat
    {
        // Метод для расчета сечения провода на основе номинального тока
        public double CalculateSection(double nominalCurrent)
        {
            return nominalCurrent switch
            {
                <= 20 => 2.5,
                <= 25 => 4,
                <= 32 => 4,
                <= 40 => 6,
                <= 50 => 10,
                <= 63 => 10,
                <= 80 => 16,
                <= 100 => 25,
                <= 125 => 25,
                <= 160 => 50,
                <= 200 => 70,
                <= 250 => 95,
                <= 320 => 120,
                _ => 0, // Значение по умолчанию
            };
        }
    }
}
