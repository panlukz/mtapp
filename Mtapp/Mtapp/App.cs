using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FreshMvvm;
using FreshTinyIoC;
using Mtapp.Data;
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
            var mainNavigation = new CustomMasterDetailNavigationContainer(NavigationContainerNames.MainContainer);
            mainNavigation.Init("Menu", "hamburger.png");
            mainNavigation.AddPage<MainPageModel>("Dashboard");
            mainNavigation.AddPage<ActivityPageModel>("Activity");
            mainNavigation.AddPage<HistoryPageModel>("History");
            mainNavigation.AddPage<SettingsPageModel>("Settings");

            var loginPage = FreshPageModelResolver.ResolvePageModel<LoginPageModel>();
            var loginContainer = new FreshNavigationContainer(loginPage, NavigationContainerNames.AuthenticationContainer);

            if (string.IsNullOrWhiteSpace(Settings.ApiToken))
                MainPage = loginContainer;
            else
                MainPage = mainNavigation;
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

            var deviceMethods = DependencyService.Get<IDeviceMethods>();
            FreshIOC.Container.Register<IDeviceMethods>(deviceMethods);

            var sqlite = DependencyService.Get<ISQLite>();
            FreshIOC.Container.Register<ISQLite>(sqlite);

            //Setup activity local data service
            var activityLocalDs = DependencyService.Get<IActivityLocalDataService>();
            FreshIOC.Container.Register<IActivityLocalDataService>(activityLocalDs);

            FreshIOC.Container.Register<IActivityManager, ActivityManager>(); // Singleton 
            FreshIOC.Container.Register<IActivityDataService, ActivityDataService>(); // Singleton 
            FreshIOC.Container.Register<IAuthService, AuthService>(); // Singleton 
            FreshIOC.Container.Register<IActivityRepository, ActivityRepository>();//singleton

        }

        public class NavigationContainerNames
        {
            public const string AuthenticationContainer = "AuthenticationContainer";
            public const string MainContainer = "MainContainer";
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
