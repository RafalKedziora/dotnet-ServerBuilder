using Domain.Paper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;

namespace ApiConsumer
{
    public class PaperApiConsumer : IApiOperator<ProjectVersions>
    {
        private readonly RestClient _client;
        private readonly IConfiguration _config;

        public PaperApiConsumer(IConfiguration config)
        {
            _config = config;
            _client = new RestClient(_config["PaperApi:BaseUrl"]);
        }

        public async Task<ProjectVersions> GetMinecraftVersionsAsync()
        {

            var request = new RestRequest(_config["PaperApi:GetVersions"]);

            var response = _client.Get(request);

            var content = JsonConvert.DeserializeObject<ProjectVersions>(response.Content);

            if(content is null)
            {
                return new ProjectVersions
                {
                    ProjectId = "paper",
                    ProjectName = "Paper",
                    VersionGroups = new List<string>() { "No version groups found" },
                    Versions = new List<string>() { "No versions found" }
                };
            }
            
            return content;

        }

        public async Task DownloadMinecraftServerInstance(string selectedServerType, string selectedServerVersionGroup, string selectedServerVersion, string pathToFile)
        {
            var request = new RestRequest(_config["PaperApi:GetBuildsData"]);
            request.AddUrlSegment("selectedServerVersion", selectedServerVersion);

            var response = _client.Get(request);

            var content = JsonConvert.DeserializeObject<PaperBuilds>(response.Content);

            var buildNumber = content.Builds.LastOrDefault().BuildNumber.ToString();
            request = new RestRequest(_config["PaperApi:GetServerInstance"]);
            request.AddUrlSegment("selectedServerVersion", selectedServerVersion);
            request.AddUrlSegment("buildNumber", buildNumber);

            var serverData = await _client.DownloadDataAsync(request);

            var fileName = _config["PaperApi:EngineFileName"];
            fileName = fileName.Replace("{buildNumber}", buildNumber).Replace("{selectedServerVersion}", selectedServerVersion);

            await File.WriteAllBytesAsync(pathToFile + fileName, serverData);
        }
    }
}
