using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FreshMvvm;
using Mtapp.Helpers;
using Mtapp.Models;
using PropertyChanged;
using Xamarin.Forms;

namespace Mtapp.PageModels
{
    [ImplementPropertyChanged]
    public class SettingsPageModel : FreshBasePageModel
    {

        public int GpsMinDistance { get { return Settings.GpsMinDistance; } set { Settings.GpsMinDistance = value; } }

        public int GpsMinTime { get { return Settings.GpsMinTime; } set { Settings.GpsMinTime = value; } }

        public int GpsMinAccuracy { get { return Settings.GpsMinAccuracy; } set { Settings.GpsMinAccuracy = value; } }

        public int GpsDesiredAccuracy { get { return Settings.GpsDesiredAccuracy; } set { Settings.GpsDesiredAccuracy = value; } }

        public string ActivityRestUri { get { return Settings.ActivityRestUri; } set { Settings.ActivityRestUri = value; } }

        public string AuthApiEndpoint { get { return Settings.AuthApiEndpoint; } set { Settings.AuthApiEndpoint = value; } }


        public string ApiToken { get { return Settings.ApiToken; } set { Settings.ApiToken = value; } }

        public Command LogoutCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var decision = await CoreMethods.DisplayAlert("Logout?", "Are you sure?", "Yes", "No");

                    if (decision)
                    {
                        Settings.ApiToken = string.Empty;
                        CoreMethods.SwitchOutRootNavigation(App.NavigationContainerNames.AuthenticationContainer);
                    }
                });
            }
        }

    }
}
