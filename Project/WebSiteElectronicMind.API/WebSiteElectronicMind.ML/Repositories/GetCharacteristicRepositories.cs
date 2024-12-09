using WebSiteElectronicMind.ML.AdditionalMethods;
using WebSiteElectronicMind.ML.Entities.Data;
using WebSiteElectronicMind.ML.Entities.Prediction;

namespace WebSiteElectronicMind.ML.Repositories
{
    public class GetCharacteristicRepositories : IGetCharacteristicRepositories
    {
        private readonly GetCharacteristic _getCharacteristic;
        private readonly DesignationOnDiagram _designationOnDiagram;
        private readonly WidthAutomat _widthAutomat;
        private readonly WireSectionAutomat _wireSectionAutomat;
        private readonly RenderingOnDiagram _renderingOnDiagram;

        public GetCharacteristicRepositories(GetCharacteristic getCharacteristic, DesignationOnDiagram designationOnDiagram, WidthAutomat widthAutomat, WireSectionAutomat wireSectionAutomat, RenderingOnDiagram renderingOnDiagram)
        {
            _getCharacteristic = getCharacteristic;
            _designationOnDiagram = designationOnDiagram;
            _widthAutomat = widthAutomat;
            _wireSectionAutomat = wireSectionAutomat;
            _renderingOnDiagram = renderingOnDiagram;
        }

        public async Task<string> GetCharacteristicAsync(string characteristic, string name, string type)
        {
            switch (characteristic)
            {
                case "Количество Полюсов":
                    return await _getCharacteristic.GetCharacteristicAsync<NumberPolesData, NumberPolesPrediction>("NumberPoles", name);
                case "Номинальный Ток":
                    return await _getCharacteristic.GetCharacteristicAsync<NominalTokData, NominalTokPrediction>("NominalTok", name);
                case "Характеристика срабатывания":
                    return await _getCharacteristic.GetCharacteristicAsync<CharacteristicData, CharacteristicPrediction>("Characteristic", name);
                case "ПКС":
                    return await _getCharacteristic.GetCharacteristicAsync<PKCData, PKCPrediction>("PKC", name);
                case "Наличие теплового расцепителя":
                    return await _getCharacteristic.GetCharacteristicAsync<RascepitelData, RascepitelPrediction>("Rascepitel", name);
                case "Производитель":
                    return await _getCharacteristic.GetCharacteristicAsync<ManufacturerData, ManufacturerPrediction>("Manufacturer", name);
                case "Серия":
                    return await _getCharacteristic.GetCharacteristicAsync<SeriesData, SeriesPrediction>("Series", name);
                case "Ток утечки":
                    return await _getCharacteristic.GetCharacteristicAsync<CurrentLeakageData, CurrentLeakagePrediction>("CurrentLeakage", name);
                case "Характеристика срабатывания по диф.току":
                    return await _getCharacteristic.GetCharacteristicAsync<CharacteristicDIFData, CharacteristicDIFPrediction>("CharacteristicDIF", name);
                case "Способ установки":
                    return await _getCharacteristic.GetCharacteristicAsync<InstallationMethodData, InstallationMethodPrediction>("InstallationMethod", name);
                case "Исполнение Рев Нерев":
                    return await _getCharacteristic.GetCharacteristicAsync<RevNerevData, RevNerevPrediction>("RevNerev", name);
                /*case "Высота":
                    return await _getCharacteristic.GetCharacteristicAsync<LockerHeightData, LockerHeightPrediction>("Высота", name);
                case "Ширина":
                    return await _getCharacteristic.GetCharacteristicAsync<LockerWidthData, LockerWidthPrediction>("Ширина", name);
                case "Глубина":
                    return await _getCharacteristic.GetCharacteristicAsync<LockerDepthData, LockerDepthPrediction>("Глубина", name);*/
                case "Обозначение на схеме":
                    return await GetDesignationOnDiagramAsync(type);
                case "Ширина автомата":
                    return await WidthAutomatAsync(characteristic, name);
                case "Сечение провода":
                    return await WireSectionAutomatAsync(characteristic, name);

                default:
                    return await Task.FromResult("Не определено");
            };
        }

        public async Task<string> GetTypeEquipment(string name)
        {
            return await _getCharacteristic.GetCharacteristicAsync<EquipmentData, EquipmentPrediction>("TypeEquipment", name);
        }

        public async Task<string> GetDesignationOnDiagramAsync(string type)
        {
            return await Task.FromResult(_designationOnDiagram.GetSymbol(type));
        }

        public async Task<string> GetRenderingOnDiagramAsync(string type)
        {
            return await Task.FromResult(_renderingOnDiagram.GetRendering(type));
        }

        public async Task<int> GetPolus(string name)
        {
            var result = await _getCharacteristic.GetCharacteristicAsync<NumberPolesData, NumberPolesPrediction>("NumberPoles", name);

            if (int.TryParse(result, out int polus))
            {
                return polus;
            }

            return 0;
        }

        public async Task<int> GetNominalTok(string name)
        {
            var result = await _getCharacteristic.GetCharacteristicAsync<NominalTokData, NominalTokPrediction>("NominalTok", name);

            if (int.TryParse(result, out int NominalTok))
            {
                return NominalTok;
            }

            return 0;
        }

        public async Task<string> GetCharacteristic(string name)
        {
            return await _getCharacteristic.GetCharacteristicAsync<CharacteristicData, CharacteristicPrediction>("Characteristic", name);
        }

        public async Task<string> GetPkc(string name)
        {
            return await _getCharacteristic.GetCharacteristicAsync<PKCData, PKCPrediction>("PKC", name);
        }


        private async Task<string> WidthAutomatAsync(string characteristic, string name)
        {
            // Получаем количество полюсов и номинальный ток асинхронно
            int numberOfPoles = int.Parse(await GetCharacteristicAsync("Количество Полюсов", name, ""));
            double nominalCurrent = double.Parse(await GetCharacteristicAsync("Номинальный Ток", name, ""));

            double width = _widthAutomat.CalculateWidth(numberOfPoles, nominalCurrent, name);

            return width.ToString("F1"); // Возвращаем ширину в виде строки
        }

        private async Task<string> WireSectionAutomatAsync(string characteristic, string name)
        {
            // Получаем номинальный ток асинхронно
            double nominalCurrent = double.Parse(await GetCharacteristicAsync("Номинальный Ток", name, ""));

            double wireSection = _wireSectionAutomat.CalculateSection(nominalCurrent);

            return wireSection.ToString(); // Возвращаем сечение провода в виде строки
        }



    }
}
