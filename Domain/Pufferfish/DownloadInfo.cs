﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Pufferfish
{
    public class DownloadInfo
    {
        [JsonProperty("relativePath")]
        public string FilePath { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
