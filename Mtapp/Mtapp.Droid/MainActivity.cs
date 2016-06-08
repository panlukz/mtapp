using System;
using System.Net;
using Android.App;
using Android.Content.PM;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Mtapp.Droid
{
    [Activity(Label = "Mtapp", Icon = "@drawable/icon", Theme = "@android:style/Theme.Holo.Light", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            ServicePointManager.ServerCertificateValidationCallback += (o, certificate, chain, errors) => true;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            Xamarin.FormsMaps.Init(this, bundle);

            ActionBar.SetIcon(new ColorDrawable(Android.Graphics.Color.Transparent));

            LoadApplication(new App());
        }

        public override void OnBackPressed()
        {
            
        }

    }
}

