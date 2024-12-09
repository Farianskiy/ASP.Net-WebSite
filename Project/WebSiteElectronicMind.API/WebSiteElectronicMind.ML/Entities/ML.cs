using Microsoft.ML;
using System.Collections.Concurrent;

namespace WebSiteElectronicMind.ML.Entities
{
    public class ML
    {
        public readonly MLContext _mlContext;
        public readonly ConcurrentDictionary<string, (ITransformer model, object predictionEngine)> _models;

        public ML()
        {
            _mlContext = new MLContext();
            _models = new ConcurrentDictionary<string, (ITransformer model, object predictionEngine)>();
        }
    }
}
