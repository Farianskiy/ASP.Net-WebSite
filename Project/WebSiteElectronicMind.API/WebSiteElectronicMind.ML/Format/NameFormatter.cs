using WebSiteElectronicMind.ML.Format.Abstractions;

namespace WebSiteElectronicMind.ML.Format
{
    public class NameFormatter : INameFormatter
    {
        private readonly IDifNameFormat _difNameFormat;
        private readonly IModNameFormat _modNameFormat;
        private readonly IRubNameFormat _rubNameFormat;
        private readonly IUZONameFormat _uZONameFormat;
        private readonly IPowerNameFormat _powerNameFormat;

        public NameFormatter(IDifNameFormat difNameFormat, IModNameFormat modNameFormat, IRubNameFormat rubNameFormat, IUZONameFormat uZONameFormat, IPowerNameFormat powerNameFormat)
        {
            _difNameFormat = difNameFormat;
            _modNameFormat = modNameFormat;
            _rubNameFormat = rubNameFormat;
            _uZONameFormat = uZONameFormat;
            _powerNameFormat = powerNameFormat;
        }

        public string FormatName(string name, string equipmentType)
        {
            return equipmentType switch
            {
                "Модульный автомат" => _modNameFormat.ApplyFormatModName(name),
                "Дифференциальный автомат" => _difNameFormat.ApplyFormatDifName(name),
                "Рубильник" => _rubNameFormat.ApplyFormatRubName(name),
                "УЗО" => _uZONameFormat.ApplyFormatUZOName(name),
                "Силовые автоматы" => _powerNameFormat.ApplyFormatPowerName(name),
                _ => throw new ArgumentException("Неизвестный тип оборудования")
            };
        }

    }
}
