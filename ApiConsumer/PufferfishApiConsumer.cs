using Domain.Pufferfish;
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
    public class PufferfishApiConsumer : IApiOperator<ProjectVersions>
    {
        private readonly RestClient _client;
        private readonly IConfiguration _config;

        public PufferfishApiConsumer(IConfiguration config)
        {
            _config = config;
            _client = new RestClient(_config["PufferfishApi:BaseUrl"]);
        }

        public async Task<ProjectVersions> GetMinecraftVersionsAsync()
        {
            var request = new RestRequest(_config["PufferfishApi:GetVersions"]);

            var response = _client.Get(request);

            var content = JsonConvert.DeserializeObject<Projects>(response.Content);

            if (content is null)
            {
                return new ProjectVersions
                {
                    ProjectName = "Pufferfish",
                    VersionGroups = new List<string>() { "No version groups found" },
                    Versions = new List<string>() { "No versions found" }
                };
            }
            var mapContent = MapContent(content);

            return mapContent;
        }

        private BuildInfo GetLatestBuild(string selectedServerVersion)
        {
            var request = new RestRequest(_config["PufferfishApi:GetBuildsData"]);
            request.AddUrlSegment("selectedServerVersion", selectedServerVersion);

            var response = _client.Get(request);

            var content = JsonConvert.DeserializeObject<Builds>(response.Content);

            if (content is null)
            {
                return new BuildInfo
                {
                    Number = 0,
                    Url = "No builds found"
                };
            }

            return content.ExistingsBuilds.Where(x => x.Number == content.ExistingsBuilds.Max(y => y.Number)).FirstOrDefault();
        }

        private BuildStatus GetBuildStatusInfo(string selectedServerVersion, int buildNumber)
        {
            var request = new RestRequest(_config["PufferfishApi:GetBuildStatusInfo"]);
            request.AddUrlSegment("selectedServerVersion", selectedServerVersion);
            request.AddUrlSegment("selectedBuildNumber", buildNumber);

            var response = _client.Get(request);
            
            var content = JsonConvert.DeserializeObject<BuildStatus>(response.Content);

            return content;

        }

        private ProjectVersions MapContent(Projects content)
        {
            var projectVersions = new ProjectVersions { RequestVersionGroups = new List<string>(), VersionGroups = new List<string>(), Versions = new List<string>() };

            foreach (var item in content.Jobs)
            {
                projectVersions.RequestVersionGroups.Add(item.Name);
                var temp = item.Name.Replace("-", " ");

                if (!projectVersions.VersionGroups.Contains(temp))
                {
                    projectVersions.VersionGroups.Add(temp);
                    projectVersions.Versions.Add($"{temp}-latest");
                }
            }

            projectVersions.Versions.Add("latest");

            return projectVersions;
        }

        public async Task DownloadMinecraftServerInstance(string selectedServerType, string selectedServerVersionGroup, string selectedServerVersion, string pathToFile)
        {
            var selectedServerVersionGroupOrigin = selectedServerVersionGroup.Replace(' ', '-');

            var latestBuild = GetLatestBuild(selectedServerVersionGroupOrigin);
            var latestBuildStatus = GetBuildStatusInfo(selectedServerVersionGroupOrigin, latestBuild.Number).Artifacts[0];
            
            var request = new RestRequest(_config["PufferfishApi:GetServerInstance"]);
            request.AddUrlSegment("selectedServerVersionGroup", selectedServerVersionGroupOrigin);
            request.AddUrlSegment("selectedBuildNumber", latestBuild.Number);
            request.AddUrlSegment("downloadEnginePath", latestBuildStatus.DownloadEnginePath);

            var serverData = await _client.DownloadDataAsync(request);

            await File.WriteAllBytesAsync(pathToFile + latestBuildStatus.FileName, serverData);
        }
    }
}
