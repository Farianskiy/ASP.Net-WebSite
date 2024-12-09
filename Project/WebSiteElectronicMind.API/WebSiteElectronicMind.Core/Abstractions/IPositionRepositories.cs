using WebSiteElectronicMind.Core.Models;

namespace WebSiteElectronicMind.ML.Repositories
{
    public interface IPositionRepositories
    {
        Task<Dictionary<string, string>> DetermineCharacteristicsAsync(string name, string type);
        Task<Position> DetermineTypeAndFormatAsync(Position data);
    }
}