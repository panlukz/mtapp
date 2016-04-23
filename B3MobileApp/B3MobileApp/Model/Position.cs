using System;
using Newtonsoft.Json;

namespace B3MobileApp.Model
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Position
    {
        public Position()
        {
            
        }

        public Position(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

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
    }
}