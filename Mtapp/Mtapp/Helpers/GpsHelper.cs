using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mtapp.Helpers
{
    public class GpsHelper
    {
        public static double CalculateDistanceBetweenPoints(double lat1, double lon1, double lat2, double lon2)
        {
            double theta = lon1 - lon2;
            double dist = Math.Sin(DegressToRadians(lat1)) * Math.Sin(DegressToRadians(lat2)) + Math.Cos(DegressToRadians(lat1)) * Math.Cos(DegressToRadians(lat2)) * Math.Cos(DegressToRadians(theta));
            dist = Math.Acos(dist);
            dist = RadiansToDegress(dist);
            dist = dist * 60 * 1.1515;
            dist = dist * 1609.344;
            return (dist);
        }

        private static double DegressToRadians(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        private static double RadiansToDegress(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
    }
}
