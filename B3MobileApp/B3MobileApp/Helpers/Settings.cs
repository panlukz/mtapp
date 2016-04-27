// Helpers/Settings.cs

using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace B3MobileApp.Helpers
{
    /// <summary>
    ///     This is the Settings static class that can be used in your Core solution or in any
    ///     of your client applications. All settings are laid out the same exact way with getters
    ///     and setters.
    /// </summary>
    public class Settings
    {
        private static ISettings AppSettings
        {
            get { return CrossSettings.Current; }
        }

        public static int GpsMinDistance
        {
            get { return AppSettings.GetValueOrDefault(GpsMinDistanceKey, GpsMinDistanceDefault); }
            set { AppSettings.AddOrUpdateValue(GpsMinDistanceKey, value); }
        }

        public static int GpsMinTime
        {
            get { return AppSettings.GetValueOrDefault(GpsMinTimeKey, GpsMinTimeDefault); }
            set { AppSettings.AddOrUpdateValue(GpsMinTimeKey, value); }
        }

        public static string ActivityRestUri {
            get { return AppSettings.GetValueOrDefault(ActivityRestUriKey, ActivityRestUriDefault); }
            set { AppSettings.AddOrUpdateValue(ActivityRestUriKey, value); }
        }

        #region Setting Constants

        private const string GpsMinDistanceKey = "gps_min_distance_key";
        private static readonly int GpsMinDistanceDefault = 3;

        private const string GpsMinTimeKey = "gps_min_time_key";
        private static readonly int GpsMinTimeDefault = 2;

        private const string ActivityRestUriKey = "activity_resturi_key";
        private static readonly string ActivityRestUriDefault = "";

        #endregion
    }
}