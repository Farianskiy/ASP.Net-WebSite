using WebSiteElectronicMind.ML.Repositories;

namespace WebSiteElectronicMind.Application.Services
{
    public class LoadModelService
    {
        private readonly ILoadModelRepositories _loadModellRepositories;

        public LoadModelService(ILoadModelRepositories loadModellRepositories)
        {
            _loadModellRepositories = loadModellRepositories;
        }

        public async Task StartupMLModel()
        {
            await _loadModellRepositories.LoadModelAsync();
        }
    }
}
