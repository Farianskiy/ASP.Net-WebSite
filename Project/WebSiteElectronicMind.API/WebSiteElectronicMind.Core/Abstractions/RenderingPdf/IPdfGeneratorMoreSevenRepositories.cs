﻿using WebSiteElectronicMind.Core.Models.RenderingToPDF;

namespace WebSiteElectronicMind.Rendering.Repositories
{
    public interface IPdfGeneratorMoreSevenRepositories
    {
        Task GeneratePdfMoreSevenAsync(string outputPdfPath, int numberOfAutomats, List<TablePDF> tablePDF, Table1C table1C);
    }
}