// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Mtapp.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
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

        public static int GpsMinAccuracy
        {
            get { return AppSettings.GetValueOrDefault(GpsMinAccuracyKey, GpsMinAccuracyDefault); }
            set { AppSettings.AddOrUpdateValue(GpsMinAccuracyKey, value); }
        }

        public static string ActivityRestUri
        {
            get { return AppSettings.GetValueOrDefault(ActivityRestUriKey, ActivityRestUriDefault); }
            set { AppSettings.AddOrUpdateValue(ActivityRestUriKey, value); }
        }

        public static int GpsDesiredAccuracy
        {
            get { return AppSettings.GetValueOrDefault(GpsDesiredAccuracyKey, GpsDesiredAccuracyDefault); }
            set { AppSettings.AddOrUpdateValue(GpsDesiredAccuracyKey, value); }
        }

        public static string ApiToken
        {
            get { return AppSettings.GetValueOrDefault(ApiTokenKey, ApiTokenDefault); }
            set { AppSettings.AddOrUpdateValue(ApiTokenKey, value); }
        }

        public static string AuthApiEndpoint
        {
            get { return AppSettings.GetValueOrDefault(AuthApiEndpointKey, AuthApiEndpointDefault); }
            set { AppSettings.AddOrUpdateValue(AuthApiEndpointKey, value); }
        }

        #region Setting Constants

        private const string GpsMinDistanceKey = "gps_min_distance_key";
        private static readonly int GpsMinDistanceDefault = 3;

        private const string GpsMinTimeKey = "gps_min_time_key";
        private static readonly int GpsMinTimeDefault = 800;

        private const string GpsMinAccuracyKey = "min_accuracy_key";
        private static readonly int GpsMinAccuracyDefault = 10;

        private const string ActivityRestUriKey = "activity_resturi_key";
        private static readonly string ActivityRestUriDefault = "https://104.40.177.83/api/v1/activity/";

        private const string GpsDesiredAccuracyKey = "min_accuracy_key";
        private static readonly int GpsDesiredAccuracyDefault = 10;

        private const string ApiTokenKey = "api_token_key";
        private static readonly string ApiTokenDefault = string.Empty;

        private const string AuthApiEndpointKey = "api_auth_key";
        private static readonly string AuthApiEndpointDefault = "https://104.40.177.83/api/v1/api-token-auth/";

        #endregion
    }
}