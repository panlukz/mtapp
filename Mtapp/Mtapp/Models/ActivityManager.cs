using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator.Abstractions;
using PropertyChanged;

namespace Mtapp.Models
{
    [ImplementPropertyChanged]
    public class ActivityManager
    {
        private readonly IGeolocator _geolocatorService;

        public ActivityManager(IGeolocator geolocatorService)
        {
            _geolocatorService = geolocatorService;
            _geolocatorService.PositionChanged += OnPositionChanged;

        }

        private void ActivityInit()
        {
            CurrentActivity = new Activity {Status = ActivityStatus.New};
        }

        private void OnPositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            if (e.Position.Accuracy <= 20)
            {
                var newPosition = new ActivityPosition
                {
                    Latitude = e.Position.Latitude,
                    Longitude = e.Position.Longitude,
                    Altitude = e.Position.Altitude,
                    Speed = e.Position.Speed,
                    Timestamp = e.Position.Timestamp.DateTime
                };

                CurrentActivity.Positions.Add(newPosition);
                ActualPosition = newPosition;
            }
        }

        #region Properties

        /// <summary>
        /// Property of current activity.
        /// </summary>
        public Activity CurrentActivity { get; set; }

        /// <summary>
        /// Property represents actual user position.
        /// </summary>
        public ActivityPosition ActualPosition { get; set; }

        #endregion


        /// <summary>
        /// This method starts a new activity.
        /// </summary>
        public async void StartActivity()
        {
            ActivityInit();
            try
            {
                _geolocatorService.DesiredAccuracy = 10;
                await _geolocatorService.StartListeningAsync(500, 3);
            }
            catch (GeolocationException ex)
            {
                //TODO log here
                throw ex;
            }

            CurrentActivity.Status = ActivityStatus.Started;
        }

        /// <summary>
        /// This method ends current activity.
        /// </summary>
        public async void StopActivity()
        {
            await _geolocatorService.StopListeningAsync();
            CurrentActivity.Status = ActivityStatus.Stopped;

        }
    }
}