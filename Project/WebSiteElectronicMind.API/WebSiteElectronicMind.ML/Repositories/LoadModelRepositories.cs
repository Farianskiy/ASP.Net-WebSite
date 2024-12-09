using WebSiteElectronicMind.ML.Entities.Data;
using WebSiteElectronicMind.ML.Entities.Prediction;

namespace WebSiteElectronicMind.ML.Repositories
{
    public class LoadModelRepositories : ILoadModelRepositories
    {
        private readonly LoadModel _loadModel;

        public LoadModelRepositories(LoadModel loadModel)
        {
            _loadModel = loadModel;
        }

        public async Task LoadModelAsync()
        {
            var task = new List<Task>
            {
                _loadModel.LoadModelAsync<EquipmentData, EquipmentPrediction>("modelTypeOborudovanie.zip", "TypeEquipment"),
                _loadModel.LoadModelAsync<NumberPolesData, NumberPolesPrediction>("modelPolus.zip", "NumberPoles"),
                _loadModel.LoadModelAsync<NominalTokData, NominalTokPrediction>("modelNominalTok.zip", "NominalTok"),
                _loadModel.LoadModelAsync<CharacteristicData, CharacteristicPrediction>("modelXaracteristika.zip", "Characteristic"),
                _loadModel.LoadModelAsync<CurrentLeakageData, CurrentLeakagePrediction>("modelTokYtehki.zip", "CurrentLeakage"),
                _loadModel.LoadModelAsync<PKCData, PKCPrediction>("modelPKC.zip", "PKC"),
                _loadModel.LoadModelAsync<CharacteristicDIFData, CharacteristicDIFPrediction>("modelXaracteristikaDIF.zip", "CharacteristicDIF"),
                _loadModel.LoadModelAsync<ManufacturerData, ManufacturerPrediction>("modelProizvoditel.zip", "Manufacturer"),
                _loadModel.LoadModelAsync<SeriesData, SeriesPrediction>("modelSeria.zip", "Series"),
                _loadModel.LoadModelAsync<RascepitelData, RascepitelPrediction>("modelRascepitel.zip", "Rascepitel"),
                _loadModel.LoadModelAsync<RevNerevData, RevNerevPrediction>("modelRevNerev.zip", "RevNerev"),
                _loadModel.LoadModelAsync<InstallationMethodData, InstallationMethodPrediction>("modelSposobYstanovki.zip", "InstallationMethod")
            };

            await Task.WhenAll(task);
        }
    }
}
