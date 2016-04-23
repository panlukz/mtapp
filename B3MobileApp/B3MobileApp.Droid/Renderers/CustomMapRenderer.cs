using System.ComponentModel;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using B3MobileApp.Droid.Renderers;
using B3MobileApp.Views.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Maps.Android;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace B3MobileApp.Droid.Renderers
{
    public class CustomMapRenderer : MapRenderer, IOnMapReadyCallback
    {
        GoogleMap _map;
        private Model.Position _lastPosition;

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
        }
            
        //TODO Clean up that mess :)
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == CustomMap.ActuallPositionProperty.PropertyName)
            {
                var customMap = ((CustomMap)sender);

                if (_lastPosition == null)
                {
                    _lastPosition = customMap.ActuallPosition;
                    return;
                }

                var actuallPosition = customMap.ActuallPosition;

                var po = new PolylineOptions();
                po.InvokeColor(0x66FF0000);
                po.Add(new LatLng(_lastPosition.Latitude, _lastPosition.Longitude));
                po.Add(new LatLng(actuallPosition.Latitude, actuallPosition.Longitude));
               
                _map.AddPolyline(po);
                

                _lastPosition = actuallPosition;

            }
        }
    }
}