using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using WebSiteElectronicMind.Rendering.Methods;
using WebSiteElectronicMind.Rendering.Methods.Input;
using WebSiteElectronicMind.Rendering.Methods.OL;
using WebSiteElectronicMind.Rendering.Methods.Shina;
using ColorImageSharp = SixLabors.ImageSharp.Color;

using Color = SixLabors.ImageSharp.Color;
using WebSiteElectronicMind.Core.Models.RenderingToPDF;

namespace WebSiteElectronicMind.Rendering.Repositories
{
    public class PdfGeneratorRepositories : IPdfGeneratorRepositories
    {
        private const int A4Width = 2480;
        private const int A4Height = 3508;

        private readonly Pattern _pattern;
        private readonly Input _automatP1;
        private readonly Shina _shina;
        private readonly OL _oL;
        private readonly Note _note;

        public PdfGeneratorRepositories(Input automatP1, Shina shina, OL ol, Pattern pattern, Note note)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            _pattern = pattern;
            _automatP1 = automatP1;
            _shina = shina;
            _oL = ol;
            _note = note;
        }

        public async Task GeneratePdfAsync(string outputPdfPath, int numberOfAutomats, List<TablePDF> tablePDF, Table1C table1C)
        {
            using (Image<Rgba32> canvas = new Image<Rgba32>(A4Width, A4Height))
            {
                // Заполняем фон белым цветом
                canvas.Mutate(x => x.Fill(ColorImageSharp.White));

                // Подготовка шрифта
                var fontCollection = new FontCollection();
                var fontFamily = fontCollection.Add("Files/Font/dinpro.otf");
                var font = fontFamily.CreateFont(38, FontStyle.Regular);

                // Горизонтальное размещение картинок _oL в линии
                int startX = 500; // Начальная позиция X для первой картинки
                int startY = 1100; // Начальная позиция Y
                int stepX = 350; // Шаг по X между изображениями

                // Добавление счетчика для элементов с одним полюсом
                int singlePolusCounter = 0;
                string[] defaultPhases = { "A", "B", "C" };

                // Флаг для отслеживания, если первый элемент имеет полюс = 1
                bool isFirstElementSinglePolus = tablePDF[0].Polus == 1;

                // Обработка элементов в tablePDF
                for (int i = 0; i < numberOfAutomats; i++)
                {
                    var data = tablePDF[i];
                    int posX = startX + ((i == 0) ? 0 : (i - 1) * stepX);


                    // Определение уровня и типа оборудования
                    switch (data.Level)
                    {
                        case 1: // Входной кабель
                            RenderInput(canvas, data.Type, data.Polus);
                            break;
                        case 2: // Обычные автоматы и УЗО
                            RenderAutomat(canvas, data.Type, data.Polus, posX, startY);
                            break;
                        default:
                            throw new ArgumentException("Неизвестный уровень оборудования");
                    }

                    

                    // Вычисление фазы только для однополюсных элементов
                    string phase = null;
                    if (isFirstElementSinglePolus & data.Polus == 1 & i >= 0)
                    {
                        phase = "L";
                    }
                    else if (data.Polus == 1)
                    {
                        phase = defaultPhases[singlePolusCounter % defaultPhases.Length];
                        singlePolusCounter++;
                    }

                    // Разделение названия схемы на строки
                    var schemeParts = data.NameOfScheme.Split(' ');

                    // Начальное положение по оси Y для текста
                    int startYSchem = 1430;
                    int lineHeight = 30; // Расстояние между строками
                    int textOffsetX = -150; // Сдвиг текста влево от изображения
                    int textOffsetFasa = -30; // Сдвиг для фазы


                    // Добавление текста для каждого элемента
                    // Условия для первого элемента
                    if (i == 0)
                    {
                        // Текст для первого элемента в другой позиции
                        canvas.Mutate(x =>
                        {
                            if (data.Polus == 1)
                            {
                                x.DrawText($"{phase}", font, Color.Black, new PointF(320, 230));
                            }

                            string numberingText = $"{data.NumberingLetter}{data.NumberingDigit}";
                            x.DrawText(numberingText, font, Color.Black, new PointF(230, 400));

                            for (int j = 0; j < schemeParts.Length; j++)
                            {
                                x.DrawText(schemeParts[j], font, Color.Black, new PointF(230, 430 + j * lineHeight));
                            }
                        });
                    }
                    else
                    {
                        // Текст для остальных элементов
                        canvas.Mutate(x =>
                        {
                            if (data.Polus == 1)
                            {
                                x.DrawText($"{phase}", font, Color.Black, new PointF(posX + textOffsetFasa, 1230));
                            }

                            string numberingText = $"{data.NumberingLetter}{data.NumberingDigit}";
                            x.DrawText(numberingText, font, Color.Black, new PointF(posX + textOffsetX, 1400));

                            for (int j = 0; j < schemeParts.Length; j++)
                            {
                                x.DrawText(schemeParts[j], font, Color.Black, new PointF(posX + textOffsetX, startYSchem + j * lineHeight));
                            }
                        });
                    }

                }

                // Добавление шины на основе данных из Table1C
                switch (table1C.Electrical.NominalVoltage)
                {
                    case 230:
                        _shina.CreateShina230B(canvas);
                        break;
                    case 400:
                        _shina.CreateShina400B(canvas);
                        break;
                    default:
                        throw new ArgumentException("Некорректное номинальное напряжение для шины");
                }

                _pattern.Decoration(canvas);
                _note.Comment(canvas, table1C);

                // Сохранение как PDF
                using (var imageStream = new MemoryStream())
                {
                    await canvas.SaveAsPngAsync(imageStream);
                    imageStream.Seek(0, SeekOrigin.Begin);

                    var document = Document.Create(container =>
                    {
                        container.Page(page =>
                        {
                            page.Size(PageSizes.A4);
                            page.Margin(0);
                            page.Content().Image(imageStream.ToArray());
                        });
                    });

                    document.GeneratePdf(outputPdfPath);
                }
            }
        }

        private void RenderInput(Image<Rgba32> canvas, string type, int polus)
        {
            switch (type)
            {
                case "Автомат":
                    switch (polus)
                    {
                        case 1:
                            _automatP1.CreateAutomatP1(canvas, new Point(360, 200));
                            break;
                        case 3:
                            _automatP1.CreateAutomatP3(canvas, new Point(360, 200));
                            break;
                        default:
                            throw new ArgumentException("Некорректное количество полюсов для входного автомата");
                    }
                    break;

                case "Диф автомат":
                    switch (polus)
                    {
                        case 1:
                            _automatP1.CreateDifAutomatP1(canvas, new Point(360, 200));
                            break;
                        case 3:
                            _automatP1.CreateDifAutomatP3(canvas, new Point(360, 200));
                            break;
                        default:
                            throw new ArgumentException("Некорректное количество полюсов для входного диф автомата");
                    }
                    break;

                case "УЗО":
                    switch (polus)
                    {
                        case 1:
                            _automatP1.CreateYZoP1(canvas, new Point(360, 200));
                            break;
                        case 3:
                            _automatP1.CreateYZoP3(canvas, new Point(360, 200));
                            break;
                        default:
                            throw new ArgumentException("Некорректное количество полюсов для УЗО");
                    }
                    break;

                case "Выключатель нагрузки":
                    switch (polus)
                    {
                        case 1:
                            _automatP1.CreateVHP1(canvas, new Point(360, 200));
                            break;
                        case 3:
                            _automatP1.CreateVHP3(canvas, new Point(360, 200));
                            break;
                        default:
                            throw new ArgumentException("Некорректное количество полюсов для ВН");
                    }
                    break;
            }
        }

        private void RenderAutomat(Image<Rgba32> canvas, string type, int polus, int posX, int posY)
        {
            switch (type)
            {
                case "Автомат":
                    switch (polus)
                    {
                        case 1:
                            _oL.CreateAutomatP1(canvas, new Point(posX, posY));
                            break;
                        case 3:
                            _oL.CreateAutomatP3(canvas, new Point(posX, posY));
                            break;
                    }
                    break;

                case "Диф автомат":
                    switch (polus)
                    {
                        case 1:
                            _oL.CreateDifAutomatP1(canvas, new Point(posX, posY));
                            break;
                        case 3:
                            _oL.CreateDifAutomatP3(canvas, new Point(posX, posY));
                            break;
                    }
                    break;

                case "УЗО":
                    switch (polus)
                    {
                        case 1:
                            _oL.CreateYZoP1(canvas, new Point(posX, posY));
                            break;
                        case 3:
                            _oL.CreateYZoP3(canvas, new Point(posX, posY));
                            break;
                    }
                    break;
            }
        }
    }
}
