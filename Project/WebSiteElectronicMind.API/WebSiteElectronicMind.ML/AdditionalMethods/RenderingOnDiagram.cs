namespace WebSiteElectronicMind.ML.AdditionalMethods
{
    public class RenderingOnDiagram
    {
        private Dictionary<string, string> _elementColors;

        public RenderingOnDiagram()
        {
            // Инициализируем словарь с элементами и их цветами на диаграмме
            _elementColors = new Dictionary<string, string>
            {
                { "Модульный автомат", "QF" },
                { "Выключатель нагрузки", "QS" },
                { "Дифференциальный автомат", "QFD" },
                { "УЗО", "QSD" },
                { "Трансформатор тока", "TA" },
                { "Трансформатор напряжения", "TV" },
                { "Счетчик ЭЭ", "Wh" },
                { "Ограничители перенапряжения", "FV" },
                { "Конденсатор", "C" },
                { "Лампа", "HL" },
                { "Кнопка", "SB" },
                { "Переключатель", "SA" },
                { "Контактор", "KM" },
                { "Преобразователь частоты", "UZ" }
            };
        }

        public string GetRendering(string elementType)
        {
            if (_elementColors.ContainsKey(elementType))
            {
                return _elementColors[elementType];
            }
            return "Цвет не определен"; // Если элемент не найден
        }
    }
}
