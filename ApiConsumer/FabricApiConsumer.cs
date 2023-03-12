using Domain.Fabric;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiConsumer
{
    public class FabricApiConsumer : IApiOperator<ProjectVersions>
    {
        private readonly RestClient _client;
        private readonly IConfiguration _config;

        public FabricApiConsumer(IConfiguration config)
        {
            _config = config;
            _client = new RestClient(_config["FabricApi:BaseUrl"]);
        }

        public async Task<ProjectVersions> GetMinecraftVersionsAsync()
        {
            var request = new RestRequest(_config["FabricApi:GetVersions"]);

            var response = _client.Get(request);

            var requestContent = JsonConvert.DeserializeObject<List<GameVersion>>(response.Content);

            var content = new ProjectVersions();

            (content.VersionGroups,content.Versions) =  MapVersions(requestContent);

            return content;
        }

        private (List<string>, List<string>) MapVersions(List<GameVersion> content)
        {
            var gameVersions = new List<string>();
            var versionGroups = new List<string>();

            foreach(var item in content)
            {
                if (item.Stable)
                {
                    var tempParts = item.Version.Split('.');
                    var tempItem = string.Join(".", tempParts.Take(2));

                    gameVersions.Add(item.Version);
                    if (!versionGroups.Contains(tempItem))
                    {
                        versionGroups.Add(tempItem);
                    }
                }
            }
            
            return (versionGroups, gameVersions);
        }

        public async Task<List<LoaderVersion>> AddLoaderData()
        {
            var request = new RestRequest(_config["FabricApi:GetLoaderData"]);

            var response = _client.Get(request);

            var content = JsonConvert.DeserializeObject<List<LoaderVersion>>(response.Content);

            return content;
        }

        public async Task<List<InstallerVersion>> AddInstallerData()
        {
            var request = new RestRequest(_config["FabricApi:GetInstallerData"]);

            var response = _client.Get(request);

            var content = JsonConvert.DeserializeObject<List<InstallerVersion>>(response.Content);

            return content;
        }

        public async Task DownloadMinecraftServerInstance(string selectedServerType, string selectedServerVersionGroup, string selectedServerVersion, string pathToFile)
        {
            var latestLoader = AddLoaderData().Result.Where(x => x.Stable).Max(x => x.Version);
            var latestInstaller = AddInstallerData().Result.Where(x => x.Stable).Max(x => x.Version);

            var request = new RestRequest(_config["FabricApi:GetServerInstance"]);
            request.AddUrlSegment("selectedServerVersion", selectedServerVersion);
            request.AddUrlSegment("selectedLoaderVersion", latestLoader);
            request.AddUrlSegment("selectedInstallerVersion", latestInstaller);

            var serverData = await _client.DownloadDataAsync(request);

            await File.WriteAllBytesAsync(pathToFile + $"server-{selectedServerVersion}-{latestInstaller}-{latestLoader}.jar", serverData);
        }
    }
}
