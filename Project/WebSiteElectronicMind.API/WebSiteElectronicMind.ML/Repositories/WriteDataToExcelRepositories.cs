using OfficeOpenXml;
using WebSiteElectronicMind.Core.Models;
using WebSiteElectronicMind.ML.Manager;

namespace WebSiteElectronicMind.ML.Repositories
{
    public class WriteDataToExcelRepositories : IWriteDataToExcelRepositories
    {
        private readonly TypeEquipmentAutomat _typeEquipmentAutomat;

        public WriteDataToExcelRepositories(TypeEquipmentAutomat typeEquipmentAutomat)
        {
            _typeEquipmentAutomat = typeEquipmentAutomat;
        }

        public async Task WriteDataToExcelRepositoriesAsync(List<Position> predictions, string filePath)
        {
            using (var package = new ExcelPackage())
            {
                var equipmentGroups = predictions.GroupBy(p => p.Type);

                foreach (var group in equipmentGroups)
                {
                    var worksheet = package.Workbook.Worksheets.Add(group.Key);

                    var columns = new List<string> { "Код", "Артикул", "Наименование", "Тип оборудования" };
                    if (_typeEquipmentAutomat._equipmentCharacteristics.ContainsKey(group.Key))
                    {
                        columns.AddRange(_typeEquipmentAutomat._equipmentCharacteristics[group.Key]);
                    }

                    // Запись заголовков
                    for (int i = 0; i < columns.Count; i++)
                    {
                        worksheet.Cells[1, i + 1].Value = columns[i];
                    }

                    int row = 2;
                    foreach (var prediction in group)
                    {
                        worksheet.Cells[row, 1].Value = prediction.Code;      // Запись кода
                        worksheet.Cells[row, 2].Value = prediction.Articul;  // Запись артикула
                        worksheet.Cells[row, 3].Value = prediction.Name;

                        for (int i = 3; i < columns.Count; i++)
                        {
                            var characteristicName = columns[i];
                            prediction.Characteristic.TryGetValue(characteristicName, out string characteristicValue);

                            if (characteristicValue == null)
                            {
                                worksheet.Cells[row, i + 1].Value = "Не определено";
                            }
                            else
                            {
                                if (int.TryParse(characteristicValue, out int intValue))
                                {
                                    worksheet.Cells[row, i + 1].Value = intValue;
                                }
                                else if (decimal.TryParse(characteristicValue, out decimal decimalValue))
                                {
                                    worksheet.Cells[row, i + 1].Value = decimalValue;
                                }
                                else
                                {
                                    worksheet.Cells[row, i + 1].Value = characteristicValue;
                                }
                            }
                        }

                        row++;
                    }
                }

                // Сохранение файла асинхронно
                var file = new FileInfo(filePath);
                await package.SaveAsAsync(file);
            }
        }
    }
}
