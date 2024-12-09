using CSharpFunctionalExtensions;
using System.Xml.Linq;
using WebSiteElectronicMind.Core.Models.RenderingToPDF.Subclasses;

namespace WebSiteElectronicMind.Core.Models.RenderingToPDF
{
    public class Table1C
    {
        private Table1C(InfoShield shield, InfoElectrical electrical, InfoCable cable, string degreeProtection, InfoOmentum omentum, string powerCable, string comment, InfoBuild build)
        {
            Shield = shield;
            Electrical = electrical;
            Cable = cable;
            DegreeProtection = degreeProtection;
            Omentum = omentum;
            Comment = comment;
            Build = build;
            PowerCable = powerCable;
        }

        public InfoShield Shield { get; }                   // Информация о щите
        public InfoElectrical Electrical { get; }           // Электрические характеристики
        public InfoCable Cable { get; }                     // Информация о кабелях
        public string DegreeProtection { get; }             // Степень защиты оболочки 
        public InfoOmentum Omentum { get; }                 // Сальники (оментумы)
        public string PowerCable { get; }                   // Питающий кабель
        public string Comment { get; } = string.Empty;      // Комментарий
        public InfoBuild Build { get; }                     // Информация по сборке


        public static Result<Table1C> Create(InfoShield shield, InfoElectrical electrical, InfoCable cable, string degreeProtection, InfoOmentum omentum, 
            string powerCable, string comment, InfoBuild build)
        {
            if (string.IsNullOrEmpty(degreeProtection))
            {
                return Result.Failure<Table1C>($"'{nameof(degreeProtection)}' cannot be null or empty");
            }

            var table1C = new Table1C(shield, electrical, cable, degreeProtection, omentum, powerCable, comment, build);
            
            return Result.Success(table1C);
        }
    }
}
