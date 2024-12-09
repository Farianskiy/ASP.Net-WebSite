using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.Fonts;
using System.Numerics;

namespace WebSiteElectronicMind.Rendering.Methods
{
    public class Pattern
    {
        public void Decoration(Image<Rgba32> image)
        {
            var pen = Pens.Solid(Color.Black, 5);
            var boldPen = Pens.Solid(Color.Black, 10);  // Жирная линия

            // Определение шрифта для текста
            var fontCollection = new FontCollection();
            var fontFamily = fontCollection.Add("Files/Font/dinpro.otf"); // Путь к файлу шрифта
            var font = fontFamily.CreateFont(34, FontStyle.Regular); // Заданный размер и стиль шрифта

            // Путь к изображению, которое нужно вставить
            string imagePath = "Files/RenderingPDF/Logo.png";

            image.Mutate(x =>
            {
                // Вставка изображения
                using (var overlayImage = Image.Load<Rgba32>(imagePath))
                {
                    // Изменение размера вставляемого изображения (если необходимо)
                    overlayImage.Mutate(o => o.Resize(500, 150)); // Измените размеры по вашему желанию

                    // Координаты вставки изображения (например, 150, 100)
                    var position = new Point(1860, 3300);

                    // Вставка изображения в холст
                    x.DrawImage(overlayImage, position, 1.0f); // 1.0f — это непрозрачность (от 0 до 1)
                }

                // Вертикальные линии
                x.DrawLine(boldPen, new PointF[]
                {
                    new PointF(50, 50),  // Начальная точка (верхняя точка вертикальной линии)
                    new PointF(50, 800)  // Конечная точка (нижняя точка вертикальной линии)
                });

                x.DrawLine(boldPen, new PointF[]
                {
                    new PointF(100, 50),  // Начальная точка (верхняя точка вертикальной линии)
                    new PointF(100, 800)  // Конечная точка (нижняя точка вертикальной линии)
                });

                x.DrawLine(boldPen, new PointF[]
                {
                    new PointF(170, 50),  // Начальная точка (верхняя точка вертикальной линии)
                    new PointF(170, 3458)  // Конечная точка (нижняя точка вертикальной линии)
                });

                x.DrawLine(boldPen, new PointF[]
                {
                    new PointF(2430, 50),  // Начальная точка (верхняя точка вертикальной линии)
                    new PointF(2430, 3458)  // Конечная точка (нижняя точка вертикальной линии)
                });




                // Вертикальные линии
                x.DrawLine(boldPen, new PointF[]
                {
                    new PointF(50, 2000),  // Начальная точка (верхняя точка вертикальной линии)
                    new PointF(50, 3458)  // Конечная точка (нижняя точка вертикальной линии)
                });

                x.DrawLine(boldPen, new PointF[]
                {
                    new PointF(100, 2000),  // Начальная точка (верхняя точка вертикальной линии)
                    new PointF(100, 3458)  // Конечная точка (нижняя точка вертикальной линии)
                });









                // Горизонтальные линии
                x.DrawLine(boldPen, new PointF[]
                {
                    new PointF(50, 50),  // Начальная точка (левая точка горизонтальной линии)
                    new PointF(2430, 50)   // Конечная точка (правая точка горизонтальной линии)
                });

                x.DrawLine(boldPen, new PointF[]
                {
                    new PointF(50, 400),  // Начальная точка (левая точка горизонтальной линии)
                    new PointF(170, 400)   // Конечная точка (правая точка горизонтальной линии)
                });

                x.DrawLine(boldPen, new PointF[]
                {
                    new PointF(50, 800),  // Начальная точка (левая точка горизонтальной линии)
                    new PointF(170, 800)   // Конечная точка (правая точка горизонтальной линии)
                });


                x.DrawLine(boldPen, new PointF[]
                {
                    new PointF(50, 2000),  // Начальная точка (левая точка горизонтальной линии)
                    new PointF(170, 2000)   // Конечная точка (правая точка горизонтальной линии)
                });

                x.DrawLine(boldPen, new PointF[]
                {
                    new PointF(50, 2300),  // Начальная точка (левая точка горизонтальной линии)
                    new PointF(170, 2300)   // Конечная точка (правая точка горизонтальной линии)
                });

                x.DrawLine(boldPen, new PointF[]
                {
                    new PointF(50, 2600),  // Начальная точка (левая точка горизонтальной линии)
                    new PointF(170, 2600)   // Конечная точка (правая точка горизонтальной линии)
                });

                x.DrawLine(boldPen, new PointF[]
                {
                      new PointF(50, 2900),  // Начальная точка (левая точка горизонтальной линии)
                      new PointF(170, 2900)   // Конечная точка (правая точка горизонтальной линии)
                });

                x.DrawLine(boldPen, new PointF[]
               {
                      new PointF(50, 3200),  // Начальная точка (левая точка горизонтальной линии)
                      new PointF(170, 3200)   // Конечная точка (правая точка горизонтальной линии)
               });

                // Горизонтальные линии
                x.DrawLine(boldPen, new PointF[]
                {
                    new PointF(50, 3458),  // Начальная точка (левая точка горизонтальной линии)
                    new PointF(2430, 3458)   // Конечная точка (правая точка горизонтальной линии)
                });


                // Горизонтальные линии
                x.DrawLine(boldPen, new PointF[]
                {
                    new PointF(170, 2783),  // Начальная точка (левая точка горизонтальной линии)
                    new PointF(2430, 2783)   // Конечная точка (правая точка горизонтальной линии)
                });













                // Горизонтальные линии
                x.DrawLine(boldPen, new PointF[]
                {
                    new PointF(400, 2783),  // Начальная точка (левая точка горизонтальной линии)
                    new PointF(400, 3458)   // Конечная точка (правая точка горизонтальной линии)
                });

                // Горизонтальные линии
                x.DrawLine(boldPen, new PointF[]
                {
                    new PointF(770, 2783),  // Начальная точка (левая точка горизонтальной линии)
                    new PointF(770, 3458)   // Конечная точка (правая точка горизонтальной линии)
                });

                // Горизонтальные линии
                x.DrawLine(boldPen, new PointF[]
                {
                    new PointF(1000, 2783),  // Начальная точка (левая точка горизонтальной линии)
                    new PointF(1000, 3458)   // Конечная точка (правая точка горизонтальной линии)
                });

                // Горизонтальные линии
                x.DrawLine(boldPen, new PointF[]
                {
                    new PointF(1270, 2783),  // Начальная точка (левая точка горизонтальной линии)
                    new PointF(1270, 3458)   // Конечная точка (правая точка горизонтальной линии)
                });













                

                // Горизонтальные линии
                x.DrawLine(boldPen, new PointF[]
                {
                    new PointF(170, 2858),  // Начальная точка (левая точка горизонтальной линии)
                    new PointF(1270, 2858)   // Конечная точка (правая точка горизонтальной линии)
                });

                // Горизонтальные линии
                x.DrawLine(boldPen, new PointF[]
                {
                    new PointF(170, 2933),  // Начальная точка (левая точка горизонтальной линии)
                    new PointF(1270, 2933)   // Конечная точка (правая точка горизонтальной линии)
                });

                // Горизонтальные линии
                x.DrawLine(boldPen, new PointF[]
                {
                    new PointF(170, 3008),  // Начальная точка (левая точка горизонтальной линии)
                    new PointF(1270, 3008)   // Конечная точка (правая точка горизонтальной линии)
                });

                // Горизонтальные линии
                x.DrawLine(boldPen, new PointF[]
                {
                    new PointF(170, 3083),  // Начальная точка (левая точка горизонтальной линии)
                    new PointF(1270, 3083)   // Конечная точка (правая точка горизонтальной линии)
                });

                // Горизонтальные линии
                x.DrawLine(boldPen, new PointF[]
                {
                    new PointF(170, 3158),  // Начальная точка (левая точка горизонтальной линии)
                    new PointF(1270, 3158)   // Конечная точка (правая точка горизонтальной линии)
                });

                // Горизонтальные линии
                x.DrawLine(boldPen, new PointF[]
                {
                    new PointF(170, 3233),  // Начальная точка (левая точка горизонтальной линии)
                    new PointF(1270, 3233)   // Конечная точка (правая точка горизонтальной линии)
                });

                // Горизонтальные линии
                x.DrawLine(boldPen, new PointF[]
                {
                    new PointF(170, 3308),  // Начальная точка (левая точка горизонтальной линии)
                    new PointF(1270, 3308)   // Конечная точка (правая точка горизонтальной линии)
                });

                // Горизонтальные линии
                x.DrawLine(boldPen, new PointF[]
                {
                    new PointF(170, 3383),  // Начальная точка (левая точка горизонтальной линии)
                    new PointF(1270, 3383)   // Конечная точка (правая точка горизонтальной линии)
                });

                // Горизонтальные линии
                x.DrawLine(boldPen, new PointF[]
                {
                    new PointF(170, 3458),  // Начальная точка (левая точка горизонтальной линии)
                    new PointF(1270, 3458)   // Конечная точка (правая точка горизонтальной линии)
                });







                // Горизонтальные линии
                x.DrawLine(boldPen, new PointF[]
                {
                    new PointF(1270, 2933),   // Конечная точка (правая точка горизонтальной линии)
                    new PointF(2430, 2933)
                });

                // Горизонтальные линии
                x.DrawLine(boldPen, new PointF[]
                {
                    new PointF(1270, 3308),   // Конечная точка (правая точка горизонтальной линии)
                    new PointF(2430, 3308)
                });

                // Горизонтальные линии
                x.DrawLine(boldPen, new PointF[]
                {
                    new PointF(1870, 2933),   // Конечная точка (правая точка горизонтальной линии)
                    new PointF(1870, 3458)
                });

                // Горизонтальные линии
                x.DrawLine(boldPen, new PointF[]
                {
                    new PointF(2070, 2933),   // Конечная точка (правая точка горизонтальной линии)
                    new PointF(2070, 3233)
                });

                // Горизонтальные линии
                x.DrawLine(boldPen, new PointF[]
                {
                    new PointF(2270, 2933),   // Конечная точка (правая точка горизонтальной линии)
                    new PointF(2270, 3233)
                });

                // Горизонтальные линии
                x.DrawLine(boldPen, new PointF[]
                {
                    new PointF(1870, 3233),   // Конечная точка (правая точка горизонтальной линии)
                    new PointF(2430, 3233)
                });

                // Горизонтальные линии
                x.DrawLine(boldPen, new PointF[]
                {
                    new PointF(1870, 3008),   // Конечная точка (правая точка горизонтальной линии)
                    new PointF(2430, 3008)
                });

                // Горизонтальные линии
                x.DrawLine(boldPen, new PointF[]
                {
                    new PointF(2160, 3233),   // Конечная точка (правая точка горизонтальной линии)
                    new PointF(2160, 3308)
                });



                


                x.DrawText("Лист", font, Color.Black, new PointF(1925, 2965)); // Указание позиции текста
                x.DrawText("Масса", font, Color.Black, new PointF(2120, 2965));
                x.DrawText("Масштаб", font, Color.Black, new PointF(2285, 2965));



                // Горизонтальные линии
                x.DrawLine(boldPen, new PointF[]
                {
                    new PointF(270, 2933),   // Конечная точка (правая точка горизонтальной линии)
                    new PointF(270, 3008)
                });

                x.DrawText("Изм.", font, Color.Black, new PointF(190, 2965));
                x.DrawText("Лист", font, Color.Black, new PointF(300, 2965));
                x.DrawText("№ Докум.", font, Color.Black, new PointF(500, 2965));
                x.DrawText("Подпись", font, Color.Black, new PointF(815, 2965));
                x.DrawText("Дата", font, Color.Black, new PointF(1090, 2965));


                x.DrawText("Разраб.", font, Color.Black, new PointF(190, 3040));
                x.DrawText("Провер.", font, Color.Black, new PointF(190, 3115));
                x.DrawText("Т. Контр.", font, Color.Black, new PointF(190, 3190));
                x.DrawText("Н. Контр.", font, Color.Black, new PointF(190, 3340));
                x.DrawText("Утв.", font, Color.Black, new PointF(190, 3415));

                x.DrawText(
                    new DrawingOptions
                    {
                        GraphicsOptions = new GraphicsOptions { Antialias = true },
                        Transform = Matrix3x2.CreateRotation((float)(-Math.PI / 2), new Vector2(60, 350)) // Поворот на 90 градусов
                    },
                    "Перв. примен.",
                    font,
                    Color.Black,
                    new PointF(60, 350)
                );

                x.DrawText(
                    new DrawingOptions
                    {
                        GraphicsOptions = new GraphicsOptions { Antialias = true },
                        Transform = Matrix3x2.CreateRotation((float)(-Math.PI / 2), new Vector2(60, 750)) // Поворот на 90 градусов
                    },
                    "Справ. №",
                    font,
                    Color.Black,
                    new PointF(60, 750)
                );

                x.DrawText(
                    new DrawingOptions
                    {
                        GraphicsOptions = new GraphicsOptions { Antialias = true },
                        Transform = Matrix3x2.CreateRotation((float)(-Math.PI / 2), new Vector2(60, 2250)) // Поворот на 90 градусов
                    },
                    "Подп. и Дата",
                    font,
                    Color.Black,
                    new PointF(60, 2250)
                );

                x.DrawText(
                    new DrawingOptions
                    {
                        GraphicsOptions = new GraphicsOptions { Antialias = true },
                        Transform = Matrix3x2.CreateRotation((float)(-Math.PI / 2), new Vector2(60, 2550)) // Поворот на 90 градусов
                    },
                    "Инв. № дубл.",
                    font,
                    Color.Black,
                    new PointF(60, 2550)
                );

                x.DrawText(
                    new DrawingOptions
                    {
                        GraphicsOptions = new GraphicsOptions { Antialias = true },
                        Transform = Matrix3x2.CreateRotation((float)(-Math.PI / 2), new Vector2(60, 2850)) // Поворот на 90 градусов
                    },
                    "Взам. инв. №",
                    font,
                    Color.Black,
                    new PointF(60, 2850)
                );

                x.DrawText(
                    new DrawingOptions
                    {
                        GraphicsOptions = new GraphicsOptions { Antialias = true },
                        Transform = Matrix3x2.CreateRotation((float)(-Math.PI / 2), new Vector2(60, 3150)) // Поворот на 90 градусов
                    },
                    "Подп. и дата",
                    font,
                    Color.Black,
                    new PointF(60, 3150)
                );

                x.DrawText(
                    new DrawingOptions
                    {
                        GraphicsOptions = new GraphicsOptions { Antialias = true },
                        Transform = Matrix3x2.CreateRotation((float)(-Math.PI / 2), new Vector2(60, 3425)) // Поворот на 90 градусов
                    },
                    "Инв. № подл.",
                    font,
                    Color.Black,
                    new PointF(60, 3425)
                );

            });
        }
    }
}
