using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Mtapp.Models
{
    public class UserAuth
    {
        [JsonProperty("username")]
        public string Name { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
