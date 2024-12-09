using Microsoft.ML.Data;

namespace WebSiteElectronicMind.ML.Entities.Prediction
{
    public class CurrentLeakagePrediction
    {
        [ColumnName("PredictedLabel")]
        public string PredictedLabel { get; set; } = string.Empty;
    }
}
