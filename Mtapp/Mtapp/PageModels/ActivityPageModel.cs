using System;
using System.Collections.Generic;
using FreshMvvm;
using Mtapp.Models;
using Plugin.Geolocator;
using PropertyChanged;
using Xamarin.Forms;

namespace Mtapp.PageModels
{
    [ImplementPropertyChanged]
    public class ActivityPageModel : FreshBasePageModel
    {
        //TODO for tests only!!!

        public ActivityPageModel(IActivityManager activityManager)
        {
            ActivityManager = activityManager;
        }

        public IActivityManager ActivityManager { get; set; }

        #region Properties

        public bool IsActivityStarted { get; set; }

        #endregion

        #region Commands

        public Command StartActivityCommand
        {
            get
            {
                return new Command(
                    async () =>
                    {
                        try
                        {
                            ActivityManager.StartActivity();
                            IsActivityStarted = true;
                        }
                        catch (Exception ex)
                        {
                            await CoreMethods.DisplayAlert("Error", ex.Message, "Ok");
                        }
                    },
                    () => !IsActivityStarted);
            }
        }


        public Command EndActivityCommand
        {
            get
            {
                return new Command(async () =>
                    {
                        var decision = await CoreMethods.DisplayAlert("Stop activity", "Are you sure?", "Yes", "No");

                        if (decision)
                        {
                            ActivityManager.StopActivity();
                            await CoreMethods.PushPageModel<ActivityDetailsPageModel>(ActivityManager.CurrentActivity);
                            IsActivityStarted = false;
                        }
                    },
                    () => IsActivityStarted );
            }
        }

        #endregion
    }
}