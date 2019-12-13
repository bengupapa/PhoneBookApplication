using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBookService.Models
{
    [JsonObject("appSettings")]
    public class AppSettings
    {
        [JsonProperty("ConnectionStrings")]
        public ConnectionStrings ConnectionStrings { get; set; }
    }

    [JsonObject("ConnectionStrings")]
    public class ConnectionStrings : Dictionary<string, string>
    {
        public string DefaultConnnectionString => this.GetValueOrDefault("ContactsInformationDatabase");
    }
}
