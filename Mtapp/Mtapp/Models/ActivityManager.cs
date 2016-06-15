using System;
using System.Linq;
using System.Threading.Tasks;
using Mtapp.Helpers;
using Mtapp.Services;
using Plugin.Geolocator.Abstractions;
using PropertyChanged;
using Xamarin.Forms;

namespace Mtapp.Models
{
    [ImplementPropertyChanged]
    public class ActivityManager : IActivityManager
    {
        private readonly IActivityLocalDataService _activityLocalDataService;
        private readonly IGeolocator _geolocatorService;

        public ActivityManager(IGeolocator geolocatorService, IActivityLocalDataService activityLocalDataService)
        {
            _geolocatorService = geolocatorService;
            _activityLocalDataService = activityLocalDataService;
            _geolocatorService.PositionChanged += OnPositionChanged;
        }


        /// <summary>
        ///     This method starts a new activity.
        /// </summary>
        public async Task StartActivityAsync()
        {
            ActivityInit();
            try
            {
                _geolocatorService.DesiredAccuracy = Settings.GpsDesiredAccuracy;
                await _geolocatorService.StartListeningAsync(Settings.GpsMinTime, Settings.GpsMinDistance);

                Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                {
                    CurrentActivity.Time += TimeSpan.FromSeconds(1);

                    return CurrentActivity.Status == ActivityStatus.Started;
                });

                CurrentActivity.Status = ActivityStatus.Started;
            }
            catch (GeolocationException ex)
            {
                //TODO log here
                throw;
            }
        }

        /// <summary>
        ///     This method ends current activity.
        /// </summary>
        public async Task StopActivityAsync()
        {
            await _geolocatorService.StopListeningAsync();
            CurrentActivity.Status = ActivityStatus.Stopped;
        }

        private void ActivityInit()
        {
            CurrentActivity = new Activity
            {
                Id = Guid.NewGuid().ToString(),
                Status = ActivityStatus.New
            };
        }

        private void OnPositionChanged(object sender, PositionEventArgs e)
        {
            if (e.Position.Accuracy <= Settings.GpsMinAccuracy)
            {
                var newPosition = new ActivityPosition
                {
                    Latitude = e.Position.Latitude,
                    Longitude = e.Position.Longitude,
                    Altitude = e.Position.Altitude,
                    Speed = e.Position.Speed,
                    Timestamp = e.Position.Timestamp.DateTime
                };

                if (CurrentActivity.Positions.Count > 0)
                {
                    var lastPosition = CurrentActivity.Positions.Last();
                    CurrentActivity.Distance += GpsHelper.CalculateDistanceBetweenPoints(lastPosition.Latitude,
                        lastPosition.Longitude, newPosition.Latitude, newPosition.Longitude);
                    CurrentActivity.AverageSpeed = CurrentActivity.Positions.Sum(p => p.Speed) /
                                                   CurrentActivity.Positions.Count;
                }

                CurrentActivity.Positions.Add(newPosition);
                ActualPosition = newPosition;
            }
        }

        #region Properties

        /// <summary>
        ///     Property of current activity.
        /// </summary>
        public Activity CurrentActivity { get; set; }

        /// <summary>
        ///     Property represents actual user position.
        /// </summary>
        public ActivityPosition ActualPosition { get; set; }

        //public TimeSpan ActualTimeSpan { get; set; }

        #endregion
    }
}