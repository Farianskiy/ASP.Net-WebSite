using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.Fonts;
using WebSiteElectronicMind.Core.Models.RenderingToPDF;

namespace WebSiteElectronicMind.Rendering.Methods
{
    public class NoteMoreSeven
    {
        public void Comment(Image<Rgba32> image, Table1C table1C, int currentPage, int totalPages)
        {
            var pen = Pens.Solid(Color.Black, 5);
            var boldPen = Pens.Solid(Color.Black, 10); // Жирная линия

            // Определение шрифта для текста
            var fontCollection = new FontCollection();
            var fontFamily = fontCollection.Add("Files/Font/dinpro.otf"); // Путь к файлу шрифта
            var font = fontFamily.CreateFont(34, FontStyle.Regular); // Заданный размер и стиль шрифта
            var fontOL = fontFamily.CreateFont(46, FontStyle.Regular); // Заданный размер и стиль шрифта

            image.Mutate(x =>
            {
                // Наименование
                x.DrawText($"ЭЛЩ.{table1C.Shield.NameShield}-{table1C.Build.NumberOrderCustomer}-{table1C.Build.NumberBuild} Э3.1", fontOL, Color.Black, new PointF(1400, 2850));

                // Наименование схемы
                x.DrawText($"Схема электрическая \r\n    однолинейная", fontOL, Color.Black, new PointF(1350, 3100));

                // Масштаб
                x.DrawText($"1:1", fontOL, Color.Black, new PointF(2320, 3100));

                // Наименование щита полное
                x.DrawText($"{table1C.Shield.FullNameShield}", fontOL, Color.Black, new PointF(1370, 3370));

                // Имя инженера
                x.DrawText($"{table1C.Build.FullNameEngineer}", font, Color.Black, new PointF(500, 3030));

                // Дата
                x.DrawText($"{DateTime.Now:dd.MM.yyyy}", font, Color.Black, new PointF(1050, 3030));

                // Отображение текущей страницы и общего количества страниц
                x.DrawText($"Лист {currentPage}", font, Color.Black, new PointF(1965, 3260));
                x.DrawText($"Листов {totalPages}", font, Color.Black, new PointF(2240, 3260));

                // Проверка, является ли это первая страница
                if (currentPage == 1)
                {
                    // Вставка значений из table1C
                    x.DrawText("Примечание:", font, Color.Black, new PointF(250, 2200));

                    // Электрические параметры
                    x.DrawText($"1. Номинальное напряжение: {table1C.Electrical.NominalVoltage}B", font, Color.Black, new PointF(250, 2250));
                    x.DrawText($"2. Номинальный ток щита: {table1C.Electrical.NominalShield}A", font, Color.Black, new PointF(250, 2300));
                    x.DrawText($"3. Тип системы заземления: {table1C.Electrical.TypeGrounding}", font, Color.Black, new PointF(250, 2350));

                    // Кабельные параметры
                    x.DrawText($"4. Ввод питающего кабеля: {table1C.Cable.SupplyCable}", font, Color.Black, new PointF(250, 2400));
                    x.DrawText($"5. Ввод кабелей ОЛ: {table1C.Cable.CableOL}", font, Color.Black, new PointF(250, 2450));

                    // Степень защиты
                    x.DrawText($"6. Степень защиты оболочки: {table1C.DegreeProtection}", font, Color.Black, new PointF(250, 2500));

                    // Сальники
                    x.DrawText($"7. В нижней части щита предусмотреть PG: {table1C.Omentum.QuantityOmentum} шт. для ввода питающего кабеля.", font, Color.Black, new PointF(250, 2550));
                    x.DrawText($"8. В нижней части щита предусмотреть PG: {table1C.Omentum.QuantityOmentumOL} шт. для ввода отходящих линий.", font, Color.Black, new PointF(250, 2600));

                    // Комментарий (если есть)
                    if (!string.IsNullOrEmpty(table1C.Comment))
                    {
                        var richTextOptions = new RichTextOptions(font)
                        {
                            Origin = new PointF(250, 2650),
                            WrappingLength = 2165,
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Top,
                            LineSpacing = 1.2f
                        };

                        x.DrawText(richTextOptions, $"Комментарий: {table1C.Comment}", Color.Black);
                    }

                    // Питающий кабель (только на первой странице)
                    x.DrawText($"{table1C.PowerCable}", font, Color.Black, new PointF(450, 300));
                }
            });
        }
    }
}
