using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using FreshMvvm;
using Mtapp.Helpers;
using Mtapp.PageModels;
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
            var masterDetailNav = new FreshMasterDetailNavigationContainer();
            masterDetailNav.Init("Menu");
            masterDetailNav.AddPage<MainPageModel>("Dashboard");
            masterDetailNav.AddPage<ActivityPageModel>("Activity");
            masterDetailNav.AddPage<HistoryPageModel>("History");
            masterDetailNav.AddPage<SettingsPageModel>("Settings");
            MainPage = masterDetailNav;
        }

        private void SetupIoc()
        {
            var containerBuilder = new ContainerBuilder();

            //Setup logger service
            var logger = DependencyService.Get<ILogger>();
            containerBuilder.RegisterInstance(logger).As<ILogger>();

            //Setup geolocator service
            var geolocator = CrossGeolocator.Current;
            geolocator.AllowsBackgroundUpdates = true;
            containerBuilder.RegisterInstance<IGeolocator>(geolocator);

            //Setup activity manager


            containerBuilder.Build();
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
