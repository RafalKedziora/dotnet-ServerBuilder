using Domain.Purpur;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ApiConsumer
{
    public class PurpurApiConsumer : IApiOperator<ProjectVersions>
    {
        private readonly RestClient _client;
        private readonly IConfiguration _config;

        public PurpurApiConsumer(IConfiguration config)
        {
            _config = config;
            _client = new RestClient(_config["PurpurApi:BaseUrl"]);
        }

        private Task<ProjectVersions> AddVersionGroups(ProjectVersions content)
        {
            content.VersionGroups = new List<string>();
            foreach (var version in content.Versions)
            {
                int count = version.Count(c => c == '.');
                int index = version.LastIndexOf('.');
                var newVersionGroup = index > 1 ? version.Substring(0, index) : version;
                
                if (!content.VersionGroups.Contains(newVersionGroup))
                {
                    content.VersionGroups.Add(newVersionGroup);
                }
            }

            return Task.FromResult(content);
        }
        
        public async Task<ProjectVersions> GetMinecraftVersionsAsync()
        {
            var request = new RestRequest(_config["PurpurApi:GetVersions"]);

            var response = _client.Get(request);

            var content = JsonConvert.DeserializeObject<ProjectVersions>(response.Content);

            if (content is null)
            {
                return new ProjectVersions
                {
                    Project = "Purpur",
                    Versions = new List<string>() { "No versions found" },
                    VersionGroups = new List<string>() { "No version groups found" }
                };
            }
            else
            {
                content = await AddVersionGroups(content);
            }

            return content;
        }

        public async Task DownloadMinecraftServerInstance(string selectedServerType, string selectedServerVersionGroup, string selectedServerVersion, string pathToFile)
        {
            var request = new RestRequest(_config["PurpurApi:GetBuildsData"]);
            request.AddUrlSegment("selectedServerVersion", selectedServerVersion);

            var response = _client.Get(request);

            var content = JsonConvert.DeserializeObject<Build>(response.Content);

            var buildNumber = content.BuildNumber;
            request = new RestRequest(_config["PurpurApi:GetServerInstance"]);
            request.AddUrlSegment("selectedServerVersion", selectedServerVersion);

            var serverData = await _client.DownloadDataAsync(request);

            var fileName = _config["PurpurApi:EngineFileName"];
            fileName = fileName.Replace("{buildNumber}", buildNumber).Replace("{selectedServerVersion}", selectedServerVersion);

            await File.WriteAllBytesAsync(pathToFile + fileName, serverData);
        }
    }
}
