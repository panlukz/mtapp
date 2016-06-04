using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PropertyChanged;

namespace Mtapp.Models
{
    [ImplementPropertyChanged]
    [JsonObject(MemberSerialization.OptIn)]
    public class Activity
    {
        public Activity()
        {
            Positions = new List<ActivityPosition>();
            Time = TimeSpan.Zero;
            Date = DateTime.UtcNow;
        }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("distance")]
        public double Distance { get; set; }

        [JsonProperty("duration")]
        public TimeSpan Time { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("coordinates")]
        public IList<ActivityPosition> Positions { get; set; }

        public ActivityStatus Status { get; set; }
    }

    public enum ActivityStatus
    {
        New,
        Started,
        Stopped,
        Ended
    }
}
