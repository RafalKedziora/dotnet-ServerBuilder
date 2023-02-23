using Domain;
using Domain.Paper;

namespace ApiConsumer
{
    public interface IApiOperator<T>
    {
        public Task<T> GetMinecraftVersionsAsync();

        public Task DownloadMinecraftServerInstance(string selectedServerType, string selectedServerVersionGroup, string selectedServerVersion, string pathToFile);
    }
}
