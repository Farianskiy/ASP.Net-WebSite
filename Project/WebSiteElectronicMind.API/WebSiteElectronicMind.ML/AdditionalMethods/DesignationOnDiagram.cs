namespace WebSiteElectronicMind.ML.AdditionalMethods
{
    public class DesignationOnDiagram
    {
        private Dictionary<string, string> _equipmentSymbols;

        public DesignationOnDiagram()
        {
            // Инициализируем словарь с типами оборудования и их обозначениями на схеме
            _equipmentSymbols = new Dictionary<string, string>
            {
                { "Модульный автомат", "Автомат" },
                { "Выключатель нагрузки", "Выключатель нагрузки" },
                { "Дифференциальный автомат", "Диф автомат" },
                { "УЗО", "УЗО" },
                { "Трансформатор тока", "Трансформатор тока" },
                { "Трансформатор напряжения", "Трансформатор напряжения" },
                { "Счетчик ЭЭ", "Счетчик ЭЭ" },
                { "Ограничители перенапряжения", "Ограничители перенапряжения" },
                { "Конденсатор", "Конденсатор" },
                { "Лампа", "Лампа" },
                { "Кнопка", "Кнопка" },
                { "Переключатель", "Переключатель" },
                { "Контактор", "Контактор" },
                { "Преобразователь частоты", "Преобразователь частоты" }
            };
        }

        public string GetSymbol(string equipmentType)
        {
            if (_equipmentSymbols.ContainsKey(equipmentType))
            {
                return _equipmentSymbols[equipmentType];
            }
            return "Неизвестно"; // На случай, если тип оборудования не найден
        }
    }
}
