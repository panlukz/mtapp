using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using FreshMvvm;
using Mtapp.Helpers;
using Mtapp.Models;
using Mtapp.Services;
using Xamarin.Forms;

namespace Mtapp.PageModels
{
    public class LoginPageModel : FreshBasePageModel
    {
        private readonly IAuthService _authService;

        public LoginPageModel(IAuthService authService)
        {
            _authService = authService;
            UserAuth = new UserAuth() {Name = string.Empty, Password = string.Empty};
        }

        public UserAuth UserAuth { get; set; }

        public Command LoginCommand
        {
            get
            {
                return new Command(async() =>
                {
                    if(string.IsNullOrWhiteSpace(UserAuth.Name) || string.IsNullOrWhiteSpace(UserAuth.Password))
                        return;

                    try
                    {
                        var token = await _authService.GetToken(UserAuth);
                        Settings.ApiToken = token;
                        CoreMethods.SwitchOutRootNavigation(App.NavigationContainerNames.MainContainer);
                    }
                    catch (Exception ex)
                    {
                        await CoreMethods.DisplayAlert("Error", ex.Message, "Ok");
                    }

                });
            }
        }

    }
}
