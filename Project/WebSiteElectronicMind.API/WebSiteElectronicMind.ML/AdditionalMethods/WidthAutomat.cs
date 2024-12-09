using System.Text.RegularExpressions;

namespace WebSiteElectronicMind.ML.AdditionalMethods
{
    public class WidthAutomat
    {
        public double CalculateWidth(int numberOfPoles, double nominalCurrent, string name)
        {
            double widthPerPole;
            string extractedName = ExtractNamePart(name);

            if (extractedName.Contains("ВА 47100") || extractedName.Contains("ВА 47150") || extractedName.Contains("ВА 47125"))
            {
                widthPerPole = 26.55;
            }
            else if (nominalCurrent <= 63)
            {
                widthPerPole = 17.7;
            }
            else
            {
                widthPerPole = 26.55;
            }

            return widthPerPole * numberOfPoles;
        }

        public string ExtractNamePart(string name)
        {
            Match match = Regex.Match(name, @"^ВА \d{5}");
            if (match.Success)
            {
                return match.Value;
            }
            return name;
        }
    }
}
