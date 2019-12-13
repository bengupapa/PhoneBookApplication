using Newtonsoft.Json;
using System.Collections.Generic;

namespace ContactManagementService.Models
{
    [JsonObject("appSettings")]
    public class AppSettings
    {
        [JsonProperty(nameof(MicroServiceSettings))]
        public MicroServiceSettings MicroServiceSettings { get; set; }
    }

    [JsonObject(nameof(MicroServiceSettings))]
    public class MicroServiceSettings : Dictionary<string, string> 
    { }
}
