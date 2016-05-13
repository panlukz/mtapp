using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FreshMvvm;
using Mtapp.PageModels;
using Xamarin.Forms;

namespace Mtapp
{
    public class App : Application
    {
        public App()
        {
            SetupNavigation();
        }

        public void SetupNavigation()
        {
            var masterDetailNav = new FreshMasterDetailNavigationContainer();
            masterDetailNav.Init("Menu");
            masterDetailNav.AddPage<MainPageModel>("Dashboard");
            masterDetailNav.AddPage<ActivityPageModel>("Activity");
            masterDetailNav.AddPage<HistoryPageModel>("History");
            masterDetailNav.AddPage<SettingsPageModel>("Settings");
            MainPage = masterDetailNav;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
