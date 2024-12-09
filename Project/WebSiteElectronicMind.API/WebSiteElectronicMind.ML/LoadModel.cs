namespace WebSiteElectronicMind.ML
{
    public class LoadModel
    {
        private static readonly string _modelDirectory =
            Path.Combine(Directory.GetCurrentDirectory(), "Files", "Models");

        private readonly ML.Entities.ML _ml;

        public LoadModel(ML.Entities.ML ml)
        {
            _ml = ml;
        }

        public async Task LoadModelAsync<TData, TPrediction>(string modelFileName, string modelTypePrediction)
            where TData : class, new()
            where TPrediction : class, new()
        {
            // Формирование полного пути к файлу модели
            var modelPath = Path.Combine(_modelDirectory, modelFileName);

            // Проверка существования файла модели
            if (!File.Exists(modelPath))
            {
                throw new FileNotFoundException($"Модель '{modelFileName}' не найдена по пути: {modelPath}");
            }

            // Асинхронная загрузка модели
            var model = await Task.Run(() => _ml._mlContext.Model.Load(modelPath, out var _));
            var predictionEngine = _ml._mlContext.Model.CreatePredictionEngine<TData, TPrediction>(model);

            // Сохранение модели и prediction engine
            _ml._models[modelTypePrediction] = (model, predictionEngine);
        }
    }
}
