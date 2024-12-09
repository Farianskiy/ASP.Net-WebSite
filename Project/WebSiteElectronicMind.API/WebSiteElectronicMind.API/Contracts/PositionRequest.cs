using System.ComponentModel.DataAnnotations;

namespace WebSiteElectronicMind.API.Contracts
{
    public record PositionRequest(
        [Required] string TitelFileName,
        [Required] IFormFile FileExcel
        );
}
