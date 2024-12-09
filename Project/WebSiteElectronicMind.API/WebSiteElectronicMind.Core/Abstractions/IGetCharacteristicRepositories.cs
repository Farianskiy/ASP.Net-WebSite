
namespace WebSiteElectronicMind.ML.Repositories
{
    public interface IGetCharacteristicRepositories
    {
        Task<string> GetCharacteristic(string name);
        Task<string> GetCharacteristicAsync(string characteristic, string name, string type);
        Task<string> GetDesignationOnDiagramAsync(string type);
        Task<string> GetRenderingOnDiagramAsync(string type);
        Task<int> GetNominalTok(string name);
        Task<string> GetPkc(string name);
        Task<int> GetPolus(string name);
        Task<string> GetTypeEquipment(string name);
    }
}