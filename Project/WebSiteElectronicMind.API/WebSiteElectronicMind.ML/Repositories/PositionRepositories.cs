using WebSiteElectronicMind.Core.Models;
using WebSiteElectronicMind.ML.Format;
using WebSiteElectronicMind.ML.Manager;

namespace WebSiteElectronicMind.ML.Repositories
{
    public class PositionRepositories : IPositionRepositories
    {
        private readonly IGetCharacteristicRepositories _getCharacteristicRepositories;
        private readonly TypeEquipmentAutomat _typeEquipmentAutomat;
        private readonly INameFormatter _nameFormatter;

        public PositionRepositories(IGetCharacteristicRepositories getCharacteristicRepositories, TypeEquipmentAutomat typeEquipmentAutomat, INameFormatter nameFormatter)
        {
            _getCharacteristicRepositories = getCharacteristicRepositories;
            _typeEquipmentAutomat = typeEquipmentAutomat;
            _nameFormatter = nameFormatter;
        }

        public async Task<Position> DetermineTypeAndFormatAsync(Position data)
        {
            var equipmentType = await _getCharacteristicRepositories.GetTypeEquipment(data.Name);

            var transformedName = _nameFormatter.FormatName(data.Name, equipmentType);

            var updateResultType = Position.Update(data, transformedName, equipmentType, data.Characteristic);
            if (updateResultType.IsFailure)
            {
                throw new Exception("Ошибка при обновлении объекта Position: " + updateResultType.Error);
            }
            data = updateResultType.Value;

            // Получаем обновленные характеристики
            var updatedCharacteristics = await DetermineCharacteristicsAsync(data.Name, equipmentType);

            // Обновляем существующий объект Position с помощью метода Update
            var updateResultCharacteristics = Position.Update(data, data.Name, data.Type, updatedCharacteristics);
            if (updateResultCharacteristics.IsFailure)
            {
                throw new Exception("Ошибка при обновлении объекта Position: " + updateResultCharacteristics.Error);
            }
            data = updateResultCharacteristics.Value;

            return data;
        }


        public async Task<Dictionary<string, string>> DetermineCharacteristicsAsync(string name, string type)
        {
            var characteristics = new Dictionary<string, string> { { "Тип оборудования", type } };

            if (_typeEquipmentAutomat._equipmentCharacteristics.ContainsKey(type))
            {
                foreach (var characteristic in _typeEquipmentAutomat._equipmentCharacteristics[type])
                {
                    var value = await _getCharacteristicRepositories.GetCharacteristicAsync(characteristic, name, type);
                    characteristics.Add(characteristic, value);
                }
            }

            return characteristics;
        }
    }
}