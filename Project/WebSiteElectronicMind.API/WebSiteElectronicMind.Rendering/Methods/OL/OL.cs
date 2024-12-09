using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace WebSiteElectronicMind.Rendering.Methods.OL
{
    public class OL
    {
        private readonly string _automatP1Path =
            Path.Combine(Directory.GetCurrentDirectory(), "Files/Schemes/OL/AutomatP1.png");

        private readonly string _automatP3Path =
            Path.Combine(Directory.GetCurrentDirectory(), "Files/Schemes/OL/AutomatP3.png");

        private readonly string _difAutomatP1Path =
            Path.Combine(Directory.GetCurrentDirectory(), "Files/Schemes/OL/DifAutomatP1.png");

        private readonly string _difAutomatP3Path =
            Path.Combine(Directory.GetCurrentDirectory(), "Files/Schemes/OL/DifAutomatP3.png");

        private readonly string _yZoP1Path =
            Path.Combine(Directory.GetCurrentDirectory(), "Files/Schemes/OL/YZoP1.png");

        private readonly string _yZoP3Path =
            Path.Combine(Directory.GetCurrentDirectory(), "Files/Schemes/OL/YZoP3.png");

        public void CreateAutomatP1(Image<Rgba32> canvas, Point position)
        {
            using (var detailImage = Image.Load<Rgba32>(_automatP1Path))
            {
                // Увеличение размера изображения (например, на 50%)
                int newWidth = (int)(detailImage.Width * 1.5);
                int newHeight = (int)(detailImage.Height * 1.5);
                detailImage.Mutate(x => x.Resize(newWidth, newHeight));

                // Вставка масштабированного изображения в холст по заданной позиции
                canvas.Mutate(x => x.DrawImage(detailImage, position, 1f));
            }
        }

        public void CreateAutomatP3(Image<Rgba32> canvas, Point position)
        {
            using (var detailImage = Image.Load<Rgba32>(_automatP3Path))
            {
                // Увеличение размера изображения (например, на 50%)
                int newWidth = (int)(detailImage.Width * 1.5);
                int newHeight = (int)(detailImage.Height * 1.5);
                detailImage.Mutate(x => x.Resize(newWidth, newHeight));

                // Вставка масштабированного изображения в холст по заданной позиции
                canvas.Mutate(x => x.DrawImage(detailImage, position, 1f));
            }
        }

        public void CreateDifAutomatP1(Image<Rgba32> canvas, Point position)
        {
            using (var detailImage = Image.Load<Rgba32>(_difAutomatP1Path))
            {
                // Увеличение размера изображения (например, на 50%)
                int newWidth = (int)(detailImage.Width * 1.5);
                int newHeight = (int)(detailImage.Height * 1.5);
                detailImage.Mutate(x => x.Resize(newWidth, newHeight));

                // Вставка масштабированного изображения в холст по заданной позиции
                canvas.Mutate(x => x.DrawImage(detailImage, position, 1f));
            }
        }

        public void CreateDifAutomatP3(Image<Rgba32> canvas, Point position)
        {
            using (var detailImage = Image.Load<Rgba32>(_difAutomatP3Path))
            {
                // Увеличение размера изображения (например, на 50%)
                int newWidth = (int)(detailImage.Width * 1.5);
                int newHeight = (int)(detailImage.Height * 1.5);
                detailImage.Mutate(x => x.Resize(newWidth, newHeight));

                // Вставка масштабированного изображения в холст по заданной позиции
                canvas.Mutate(x => x.DrawImage(detailImage, position, 1f));
            }
        }

        public void CreateYZoP1(Image<Rgba32> canvas, Point position)
        {
            using (var detailImage = Image.Load<Rgba32>(_yZoP1Path))
            {
                // Увеличение размера изображения (например, на 50%)
                int newWidth = (int)(detailImage.Width * 1.5);
                int newHeight = (int)(detailImage.Height * 1.5);
                detailImage.Mutate(x => x.Resize(newWidth, newHeight));

                // Вставка масштабированного изображения в холст по заданной позиции
                canvas.Mutate(x => x.DrawImage(detailImage, position, 1f));
            }
        }

        public void CreateYZoP3(Image<Rgba32> canvas, Point position)
        {
            using (var detailImage = Image.Load<Rgba32>(_yZoP3Path))
            {
                // Увеличение размера изображения (например, на 50%)
                int newWidth = (int)(detailImage.Width * 1.5);
                int newHeight = (int)(detailImage.Height * 1.5);
                detailImage.Mutate(x => x.Resize(newWidth, newHeight));

                // Вставка масштабированного изображения в холст по заданной позиции
                canvas.Mutate(x => x.DrawImage(detailImage, position, 1f));
            }
        }



    }
}
