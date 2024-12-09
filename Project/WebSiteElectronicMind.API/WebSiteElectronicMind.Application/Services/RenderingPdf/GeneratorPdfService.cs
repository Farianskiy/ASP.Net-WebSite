using WebSiteElectronicMind.Core.Models.RenderingToPDF;
using WebSiteElectronicMind.Rendering.Repositories;

namespace WebSiteElectronicMind.Application.Services.RenderingPdf
{
    public class GeneratorPdfService
    {
        private readonly IPdfGeneratorRepositories _pdfGeneratorRepositories;
        private readonly IPdfGeneratorMoreSevenRepositories _pdfGeneratorRepositoriesMoreSeven;

        public GeneratorPdfService(IPdfGeneratorRepositories pdfGeneratorRepositories, IPdfGeneratorMoreSevenRepositories pdfGeneratorMoreSevenRepositories)
        {
            _pdfGeneratorRepositories = pdfGeneratorRepositories;
            _pdfGeneratorRepositoriesMoreSeven = pdfGeneratorMoreSevenRepositories;
        }

        public async Task GeneratePdfAsync(string outputPdfPath, int numberOfAutomats, List<TablePDF> tablePDF, Table1C table1C)
        {
            if (string.IsNullOrWhiteSpace(outputPdfPath))
                throw new ArgumentException("Output path cannot be null or empty", nameof(outputPdfPath));

            await _pdfGeneratorRepositories.GeneratePdfAsync(outputPdfPath, numberOfAutomats, tablePDF, table1C);
        }

        public async Task GeneratePdfMoreSevenAsync(string outputPdfPath, int numberOfAutomats, List<TablePDF> tablePDF, Table1C table1C)
        {
            if (string.IsNullOrWhiteSpace(outputPdfPath))
                throw new ArgumentException("Output path cannot be null or empty", nameof(outputPdfPath));

            await _pdfGeneratorRepositoriesMoreSeven.GeneratePdfMoreSevenAsync(outputPdfPath, numberOfAutomats, tablePDF, table1C);
        }
    }
}
