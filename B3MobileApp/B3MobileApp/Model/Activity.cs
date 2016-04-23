using System;
using System.Collections.Generic;
using B3MobileApp.Services;
using GalaSoft.MvvmLight.Ioc;
using Newtonsoft.Json;
using Plugin.Geolocator.Abstractions;

namespace B3MobileApp.Model
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Activity
    {
        private readonly IGeolocator _geolocatorService;
        private readonly IActivityDataService _activityDataService;

        public Activity()
        {
            _geolocatorService = SimpleIoc.Default.GetInstance<IGeolocator>();
            _activityDataService = SimpleIoc.Default.GetInstance<IActivityDataService>();

            _geolocatorService.PositionChanged += OnPositionChanged;

            Positions = new List<Position>();
        }

        private void OnPositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            var newPosition = new Position
            {
                Latitude = e.Position.Latitude,
                Longitude = e.Position.Longitude,
                Altitude = e.Position.Altitude,
                Speed = e.Position.Speed,
                Timestamp = e.Position.Timestamp.DateTime
            };

            Positions.Add(newPosition);

            ActuallPosition = newPosition;
        }

        [JsonProperty("latlngs")]
        public List<Position> Positions { get; set; }

        private Position _actuallPosition;
        public Position ActuallPosition
        {
            get
            {
                if (_actuallPosition == null)
                    _actuallPosition = new Position();

                return _actuallPosition;
            }
            set
            {
                _actuallPosition = value;

                OnActualPositionChanged();
            }
        }

        public event EventHandler<Model.PositionEventArgs> ActuallPositionChanged;
        private void OnActualPositionChanged()
        {
            if (ActuallPositionChanged != null)
                ActuallPositionChanged(this, new Model.PositionEventArgs() { Position = _actuallPosition});
        }

        [JsonProperty("distance")]
        public double Distance { get; set; }

        public bool IsEnded { get; set; }

        public async void Start()
        {
            if(!IsEnded)
                await _geolocatorService.StartListeningAsync(1, 1);
        }

        public async void Stop()
        {
            await _geolocatorService.StopListeningAsync();

            //sending a post is disabled, beacause it doesn't work for unknow reason
            await _activityDataService.SaveActivity(this);

            IsEnded = true;
        }

    }
}