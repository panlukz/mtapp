using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Mtapp.Droid.Helpers;
using Mtapp.Helpers;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidMethods))]
namespace Mtapp.Droid.Helpers
{
    public class AndroidMethods : IDeviceMethods
    {
        public void FinishApp()
        {
        }

        public string GetDataPath()
        {
            return System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        }
    }
}