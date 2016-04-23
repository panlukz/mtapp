﻿
using Android.App;
using Android.Content.PM;
using Android.OS;

namespace B3MobileApp.Droid
{
    [Activity(Label = "B3MobileApp", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            //Maps initialization
            Xamarin.FormsMaps.Init(this, bundle);

            LoadApplication(new App());
        }
    }
}
