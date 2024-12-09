using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.Fonts;
using WebSiteElectronicMind.Core.Models.RenderingToPDF;

namespace WebSiteElectronicMind.Rendering.Methods
{
    public class Note
    {
        public void Comment(Image<Rgba32> image, Table1C table1C)
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
                // Вставка значений из table1C
                x.DrawText("Примечание:", font, Color.Black, new PointF(250, 2200));

                // Исправлено: Используем Electrical вместо InfoElectrical
                x.DrawText($"1. Номинальное напряжение: {table1C.Electrical.NominalVoltage}B", font, Color.Black, new PointF(250, 2250));
                x.DrawText($"2. Номинальный ток щита: {table1C.Electrical.NominalShield}A", font, Color.Black, new PointF(250, 2300));
                x.DrawText($"3. Тип системы заземления: {table1C.Electrical.TypeGrounding}", font, Color.Black, new PointF(250, 2350));

                // Исправлено: Используем Cable вместо InfoCable
                x.DrawText($"4. Ввод питающего кабеля: {table1C.Cable.SupplyCable}", font, Color.Black, new PointF(250, 2400));
                x.DrawText($"5. Ввод кабелей ОЛ: {table1C.Cable.CableOL}", font, Color.Black, new PointF(250, 2450));

                // Степень защиты
                x.DrawText($"6. Степень защиты оболочки: {table1C.DegreeProtection}", font, Color.Black, new PointF(250, 2500));

                // Исправлено: Используем Omentum вместо InfoOmentum
                x.DrawText($"7. В нижней части щита предусмотреть PG: {table1C.Omentum.QuantityOmentum}шт. для ввода кабелей питающего кабеля.", font, Color.Black, new PointF(250, 2550));
                x.DrawText($"8. В нижней части щита предусмотреть PG: {table1C.Omentum.QuantityOmentumOL}шт. для ввода кабелей отходящих линий.", font, Color.Black, new PointF(250, 2600));

                if (!string.IsNullOrEmpty(table1C.Comment))
                {
                    // Настройки для переноса текста
                    var richTextOptions = new RichTextOptions(font)
                    {
                        Origin = new PointF(250, 2650),            // Начальная точка текста
                        WrappingLength = 2165,                     // Учитывая отступы (2480 - 250 - 65)
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Top,
                        LineSpacing = 1.2f                         // Межстрочный интервал для улучшения читаемости
                    };

                    // Рисуем текст с учётом переноса
                    image.Mutate(ctx => ctx.DrawText(richTextOptions, $"Комментарий: {table1C.Comment}", Color.Black));
                }

                // Отображение текущей страницы и общего количества страниц
                x.DrawText($"Лист 1", font, Color.Black, new PointF(1965, 3260));
                x.DrawText($"Листов 1", font, Color.Black, new PointF(2240, 3260));

                // Наименование
                x.DrawText($"ЭЛЩ.{table1C.Shield.NameShield}-{table1C.Build.NumberOrderCustomer}-{table1C.Build.NumberBuild} Э3.1", fontOL, Color.Black, new PointF(1400, 2850));

                // Наименование схемы
                x.DrawText($"Схема электрическая \r\n    однолинейная", fontOL, Color.Black, new PointF(1350, 3100));

                // Масштаб
                x.DrawText($"1:1", fontOL, Color.Black, new PointF(2320, 3100));

                // Наименование щита полное
                x.DrawText($"{table1C.Shield.FullNameShield}", fontOL, Color.Black, new PointF(1370, 3370));

                // Имя инжинера
                x.DrawText($"{table1C.Build.FullNameEngineer}", font, Color.Black, new PointF(500, 3030));

                // Дата
                x.DrawText($"{DateTime.Now:dd.MM.yyyy}", font, Color.Black, new PointF(1050, 3030));

                // Питающий кабель
                x.DrawText($"{table1C.PowerCable}", font, Color.Black, new PointF(450, 300));
            });
        }
    }
}
