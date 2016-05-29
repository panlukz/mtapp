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
        }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        public DateTime Date
        {
            get
            {
                if (Positions.Count > 0)
                    return Positions[0].Timestamp;
                else
                    //TODO for tests
                    return new DateTime();
            }
        }

        public TimeSpan TotalTime
        {
            get
            {
                if (Positions.Count > 0)
                    return (Positions.Last().Timestamp - Positions.First().Timestamp);

                return TimeSpan.Zero;
            }
        }

        [JsonProperty("desc")]
        public string Description { get; set; }

        [JsonProperty("distance")]
        public double Distance { get; set; }

        [JsonProperty("latlngs")]
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
