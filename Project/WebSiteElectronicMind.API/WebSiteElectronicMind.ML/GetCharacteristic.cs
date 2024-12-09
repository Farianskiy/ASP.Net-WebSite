using Microsoft.ML;
using WebSiteElectronicMind.ML.Entities;

namespace WebSiteElectronicMind.ML
{
    public class GetCharacteristic
    {
        private readonly ML.Entities.ML _ml;

        public GetCharacteristic(ML.Entities.ML ml)
        {
            _ml = ml;
        }

        public async Task<string> GetCharacteristicAsync<TData, TPrediction>(string modelName, string name)
            where TData : AbsoluteData, new()
            where TPrediction : class, new()
        {
            try
            {
                var predictionEngine = (PredictionEngine<TData, TPrediction>)_ml._models[modelName].predictionEngine;
                var data = new TData { NamePosition = name };
                var prediction = await Task.Run(() => predictionEngine.Predict(data));
                var predictedLabelProperty = typeof(TPrediction).GetProperty("PredictedLabel");
                return predictedLabelProperty?.GetValue(prediction)?.ToString() ?? "Не определено";
            }
            catch (Exception ex)
            {
                return $"Ошибка выполнения предсказания: {ex.Message}";
            }
        }

    }
}
