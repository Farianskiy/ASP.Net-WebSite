
namespace WebSiteElectronicMind.Rendering.Repositories
{
    public interface ISchemeDetailsRepositories
    {
        Task<(string type, string letter, string result, int polus)> SchemeDetailsfAsync(string name);
        Task<string> GetEquipmentTypeAsync(string name);
    }
}