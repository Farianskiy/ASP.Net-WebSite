using WebSiteElectronicMind.ML.Repositories;

namespace WebSiteElectronicMind.Rendering.Repositories
{
    public class SchemeDetailsRepositories : ISchemeDetailsRepositories
    {
        private readonly IGetCharacteristicRepositories _getCharacteristicRepositories;

        public SchemeDetailsRepositories(IGetCharacteristicRepositories getCharacteristicRepositories)
        {
            _getCharacteristicRepositories = getCharacteristicRepositories;
        }

        public async Task<(string type, string letter, string result, int polus)> SchemeDetailsfAsync(string name)
        {
            // Замена неразрывных пробелов на обычные
            name = name.Replace("\u00A0", " ");

            // Получаем характеристики из репозитория
            string type = (await _getCharacteristicRepositories.GetTypeEquipment(name)).Trim();

            string letter = await _getCharacteristicRepositories.GetRenderingOnDiagramAsync(type);
            int polus = await _getCharacteristicRepositories.GetPolus(name);

            int nominalTok = await _getCharacteristicRepositories.GetNominalTok(name);
            string characteristic = await _getCharacteristicRepositories.GetCharacteristic(name);
            string pkc = await _getCharacteristicRepositories.GetPkc(name);

            // Парсим часть названия для формирования результата
            string[] nameParts = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string equipmentName = $"{nameParts[0]} {nameParts[1]}"; // Получаем "ВА 4763"

            // Формируем строку результата
            string result = $"{equipmentName} In={nominalTok}А хар.{characteristic} Icu={pkc}";

            // Возвращаем кортеж
            return (type, letter, result, polus);
        }

        public async Task<string> GetEquipmentTypeAsync(string name)
        {
            // Замена неразрывных пробелов на обычные
            name = name.Replace("\u00A0", " ");

            // Получаем тип оборудования из репозитория
            string type = await _getCharacteristicRepositories.GetTypeEquipment(name);

            return type;
        }


    }
}
