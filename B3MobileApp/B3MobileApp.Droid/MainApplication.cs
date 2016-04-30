using System;

using Android.App;
using Android.OS;
using Android.Runtime;
using B3MobileApp.Helpers;
using Plugin.CurrentActivity;
using Xamarin.Forms;
using Application = Android.App.Application;

namespace B3MobileApp.Droid
{
	//You can specify additional application information in this attribute
    [Application]
    public class MainApplication : Application, Application.IActivityLifecycleCallbacks
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transer)
          :base(handle, transer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            RegisterActivityLifecycleCallbacks(this);
            //A great place to initialize Xamarin.Insights and Dependency Services!

            AndroidEnvironment.UnhandledExceptionRaiser += AndroidEnvironmentExceptionHandler;
            AppDomain.CurrentDomain.UnhandledException += AppDomainExceptionHandler;
        }

        private void AppDomainExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            DependencyService.Get<ILogger>().Log(((Exception)e.ExceptionObject).Message, "AppDomain", LogType.Error);
        }

        private void AndroidEnvironmentExceptionHandler(object sender, RaiseThrowableEventArgs e)
        {
            DependencyService.Get<ILogger>().Log(e.Exception.Message, "AndroidEnviroment", LogType.Error);
        }

        public override void OnTerminate()
        {
            base.OnTerminate();
            UnregisterActivityLifecycleCallbacks(this);
        }

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityDestroyed(Activity activity)
        {
        }

        public void OnActivityPaused(Activity activity)
        {
        }

        public void OnActivityResumed(Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
        }

        public void OnActivityStarted(Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityStopped(Activity activity)
        {
        }
    }
}