using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.Fonts;

namespace WebSiteElectronicMind.Rendering.Methods.Shina
{
    public class Shina
    {
        private readonly string _shina230BPath =
            Path.Combine(Directory.GetCurrentDirectory(), "Files/Schemes/Shina/Shina230B.png");

        private readonly string _shina400BPath =
            Path.Combine(Directory.GetCurrentDirectory(), "Files/Schemes/Shina/Shina400B.png");


        public void CreateShina230B(Image<Rgba32> canvas)
        {
            var solidPen = Pens.Solid(Color.Black, 5); // Сплошная линия
            var thinPen = Pens.Solid(Color.Black, 3);  // Тонкая линия
            var dashPen = Pens.Solid(Color.Black, 2); // Используем для пунктирной линии

            float dashLength = 10; // Длина штриха
            float gapLength = 5;   // Длина промежутка
            float startX = 250;
            float endX = 2405;
            float yPosition = 1182;

            // Определение шрифта для текста
            var fontCollection = new FontCollection();
            var fontFamily = fontCollection.Add("Files/Font/arialmt.ttf"); // Путь к файлу шрифта
            var font = fontFamily.CreateFont(24, FontStyle.Regular); // Заданный размер и стиль шрифта

            canvas.Mutate(x =>
            {
                // Сплошные линии
                x.DrawLine(solidPen, new PointF(250, 1112), new PointF(2405, 1112));
                x.DrawLine(thinPen, new PointF(250, 1147), new PointF(2405, 1147));

                // Пунктирная линия, созданная вручную
                float currentX = startX;
                while (currentX < endX)
                {
                    float nextX = Math.Min(currentX + dashLength, endX);
                    x.DrawLine(dashPen, new PointF(currentX, yPosition), new PointF(nextX, yPosition));
                    currentX = nextX + gapLength;
                }
                // Границы слева
                x.DrawLine(solidPen, new PointF(250, 1097), new PointF(250, 1127));
                x.DrawLine(thinPen, new PointF(250, 1135), new PointF(250, 1160));
                x.DrawLine(dashPen, new PointF(250, 1173), new PointF(250, 1192));

                // Границы справа
                x.DrawLine(solidPen, new PointF(2405, 1097), new PointF(2405, 1127));
                x.DrawLine(thinPen, new PointF(2405, 1135), new PointF(2405, 1160));
                x.DrawLine(dashPen, new PointF(2405, 1173), new PointF(2405, 1192));
                
                // Черточки на линии
                x.DrawLine(thinPen, new PointF(280, 1097), new PointF(310, 1127));
                x.DrawLine(thinPen, new PointF(305, 1097), new PointF(335, 1127));
                x.DrawLine(thinPen, new PointF(330, 1097), new PointF(360, 1127));

                // Добавление текста
                x.DrawText("L", font, Color.Black, new PointF(240, 1035)); // Указание позиции текста
                x.DrawText("~230B, 50Гц", font, Color.Black, new PointF(240, 1060)); // Указание позиции текста
                x.DrawText("N", font, Color.Black, new PointF(220, 1140)); // Указание позиции текста
                x.DrawText("PE", font, Color.Black, new PointF(212, 1173)); // Указание позиции текста
            });
        }


        public void CreateShina400B(Image<Rgba32> canvas)
        {
            var solidPen = Pens.Solid(Color.Black, 5); // Сплошная линия
            var thinPen = Pens.Solid(Color.Black, 3);  // Тонкая линия
            var dashPen = Pens.Solid(Color.Black, 2); // Используем для пунктирной линии

            float dashLength = 10; // Длина штриха
            float gapLength = 5;   // Длина промежутка
            float startX = 250;
            float endX = 2355;
            float yPosition = 1182;

            // Определение шрифта для текста
            var fontCollection = new FontCollection();
            var fontFamily = fontCollection.Add("Files/Font/arialmt.ttf"); // Путь к файлу шрифта
            var font = fontFamily.CreateFont(24, FontStyle.Regular); // Заданный размер и стиль шрифта

            canvas.Mutate(x =>
            {
                // Сплошные линии
                x.DrawLine(solidPen, new PointF(250, 1112), new PointF(2355, 1112));
                x.DrawLine(thinPen, new PointF(250, 1147), new PointF(2355, 1147));

                // Пунктирная линия, созданная вручную
                float currentX = startX;
                while (currentX < endX)
                {
                    float nextX = Math.Min(currentX + dashLength, endX);
                    x.DrawLine(dashPen, new PointF(currentX, yPosition), new PointF(nextX, yPosition));
                    currentX = nextX + gapLength;
                }
                // Границы слева
                x.DrawLine(solidPen, new PointF(250, 1097), new PointF(250, 1127));
                x.DrawLine(thinPen, new PointF(250, 1135), new PointF(250, 1160));
                x.DrawLine(dashPen, new PointF(250, 1173), new PointF(250, 1192));

                // Границы справа
                x.DrawLine(solidPen, new PointF(2355, 1097), new PointF(2355, 1127));
                x.DrawLine(thinPen, new PointF(2355, 1135), new PointF(2355, 1160));
                x.DrawLine(dashPen, new PointF(2355, 1173), new PointF(2355, 1192));

                // Черточки на линии
                x.DrawLine(thinPen, new PointF(280, 1097), new PointF(310, 1127));
                x.DrawLine(thinPen, new PointF(305, 1097), new PointF(335, 1127));
                x.DrawLine(thinPen, new PointF(330, 1097), new PointF(360, 1127));

                // Добавление текста
                x.DrawText("A, B, C", font, Color.Black, new PointF(240, 1035)); // Указание позиции текста
                x.DrawText("~0,4кB, 50Гц", font, Color.Black, new PointF(240, 1060)); // Указание позиции текста
                x.DrawText("N", font, Color.Black, new PointF(220, 1140)); // Указание позиции текста
                x.DrawText("PE", font, Color.Black, new PointF(212, 1173)); // Указание позиции текста
            });
        }
    }
}
