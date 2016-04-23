using System;

namespace B3MobileApp.Model
{
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

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public double Altitude { get; set; }

        public double Speed { get; set; }

        public DateTime Timestamp { get; set; }
    }
}