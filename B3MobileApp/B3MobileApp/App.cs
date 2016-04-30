using B3MobileApp.Helpers;
using B3MobileApp.Services;
using B3MobileApp.ViewModels;
using B3MobileApp.Views;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms;

namespace B3MobileApp
{
    public class App : Application
    {
        private static readonly ViewModelLocator _locator = new ViewModelLocator();
        public static ViewModelLocator Locator
        {
            get { return _locator; }
        }

        public App()
        {
            //Setup logger service
            var logger = DependencyService.Get<ILogger>();
            SimpleIoc.Default.Register<ILogger>(() => logger);

            logger.Log("Starting application", "App");

            logger.Log("Wrapping up navigation service", "App");
            //Setup navigation service
            var nav = new NavigationService();
            nav.Configure(ViewModelLocator.MainView, typeof(MainView));
            nav.Configure(ViewModelLocator.ActivityView, typeof(ActivityView));
            nav.Configure(ViewModelLocator.OptionsView, typeof(OptionsView));
            
            SimpleIoc.Default.Register<INavigationService>(() => nav);

            logger.Log("Wrapping up dialog service", "App");
            //Setup dialog service
            var dialog = new DialogService();
            SimpleIoc.Default.Register<IDialogService>(() => dialog);

            logger.Log("Wrapping up geolocator service", "App");
            //Setup geolocator service
            var geolocator = CrossGeolocator.Current;
            SimpleIoc.Default.Register<IGeolocator>(() => geolocator);

            //Setup activity data service
            SimpleIoc.Default.Register<IActivityDataService, ActivityDataService>();

            var mainPage = new NavigationPage(new MainView());

            nav.Initialize(mainPage);
            dialog.Initialize(mainPage);

            // The root page of application
            MainPage = mainPage;
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
