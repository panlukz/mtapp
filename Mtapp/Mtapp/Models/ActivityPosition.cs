using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PropertyChanged;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace Mtapp.Models
{
    [ImplementPropertyChanged]
    [JsonObject(MemberSerialization.OptIn)]
    public class ActivityPosition
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [JsonProperty("lng")]
        public double Longitude { get; set; }

        [JsonProperty("lat")]
        public double Latitude { get; set; }

        [JsonProperty("alt")]
        public double Altitude { get; set; }

        [JsonProperty("speed")]
        public double Speed { get; set; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [ForeignKey(typeof(Activity))]
        public string ActivityId { get; set; }
        
        [ManyToOne]
        public Activity Activity { get; set; }
    }
}
