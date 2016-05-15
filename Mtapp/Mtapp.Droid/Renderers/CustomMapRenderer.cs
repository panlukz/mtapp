using System.Collections.Generic;
using System.ComponentModel;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Mtapp.Droid.Renderers;
using Mtapp.Models;
using Mtapp.Pages.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace Mtapp.Droid.Renderers
{
    public class CustomMapRenderer : MapRenderer, IOnMapReadyCallback
    {
        GoogleMap _map;
        private ActivityPosition _lastPosition;
        private List<Polyline> _mapPolylines = new List<Polyline>();
        private List<LatLng> positions = new List<LatLng>(); 

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                //TODO Unsubscribe ??
            }

            if (e.NewElement != null)
            {
                ((MapView)Control).GetMapAsync(this);
            }
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            _map = googleMap;
            UpdatePolylines();
        }

        private void UpdatePolylines()
        {
            //TODO add here posibility to bind custom polylines in order to view historical activities
            foreach (var polyline in _mapPolylines)
            {
                polyline.Remove();
            }
            _mapPolylines.Clear();

            var newPo = new PolylineOptions();
            newPo.InvokeColor(0x66FF0000);
            newPo.InvokeWidth(5);
            foreach (var position in positions)
            {
                newPo.Add(new LatLng(position.Latitude, position.Longitude));
            }

            _map.AddPolyline(newPo);
        }

        //TODO Clean up that mess :)
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var customMap = ((CustomMap)sender);

            if (e.PropertyName == CustomMap.ActuallPositionProperty.PropertyName)
            {
                if (_lastPosition == null)
                {
                    _lastPosition = customMap.ActuallPosition;
                    return;
                }

                var actuallPosition = customMap.ActuallPosition;

                var po = new PolylineOptions();
                po.InvokeColor(0x66FF0000);
                po.InvokeWidth(5);
                po.Add(new LatLng(_lastPosition.Latitude, _lastPosition.Longitude));
                po.Add(new LatLng(actuallPosition.Latitude, actuallPosition.Longitude));

                var polyline = _map.AddPolyline(po);
                _mapPolylines.Add(polyline);

                _lastPosition = actuallPosition;

            }

            if (e.PropertyName == CustomMap.PositionsProperty.PropertyName)
            {

                

                positions.Clear();

                foreach (var position in customMap.Positions)
                {
                    positions.Add(new LatLng(position.Latitude, position.Longitude));
                }

                if (_map == null)
                    return;
                UpdatePolylines();
            }
        }
    }
}