using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FreshMvvm;
using FreshTinyIoC;
using Mtapp.Helpers;
using Mtapp.Models;
using Mtapp.PageModels;
using Mtapp.Pages.Containers;
using Mtapp.Services;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms;

namespace Mtapp
{
    public class App : Application
    {
        public App()
        {
            SetupIoc();
            SetupNavigation();
        }

        private void SetupNavigation()
        {
            var masterDetailNav = new CustomMasterDetailNavigationContainer();
            masterDetailNav.Init("Menu", "hamburger.png");
            masterDetailNav.AddPage<MainPageModel>("Dashboard");
            masterDetailNav.AddPage<ActivityPageModel>("Activity");
            masterDetailNav.AddPage<HistoryPageModel>("History");
            masterDetailNav.AddPage<SettingsPageModel>("Settings");
            MainPage = masterDetailNav;
        }

        private void SetupIoc()
        {
            //Setup logger service
            var logger = DependencyService.Get<ILogger>();
            FreshIOC.Container.Register<ILogger>(logger);

            //Setup geolocator service
            var geolocator = CrossGeolocator.Current;
            geolocator.AllowsBackgroundUpdates = true;
            FreshIOC.Container.Register<IGeolocator>(geolocator);

            //Setup activity local data service
            var activityLocalDs = DependencyService.Get<IActivityLocalDataService>();
            FreshIOC.Container.Register<IActivityLocalDataService>(activityLocalDs);

            FreshIOC.Container.Register<IActivityManager, ActivityManager>(); // Singleton 

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
