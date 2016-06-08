using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Mtapp.Models
{
    public class TokenAuth
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
