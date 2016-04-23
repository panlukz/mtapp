using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace B3MobileApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        public MainViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
        }

        private string _login;
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                RaisePropertyChanged(() => Login);
                LogInCommand.RaiseCanExecuteChanged();
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
                LogInCommand.RaiseCanExecuteChanged();
            }
        }

        private RelayCommand _logInCommand;
        public RelayCommand LogInCommand
        {
            get
            {
                return _logInCommand ?? (_logInCommand = new RelayCommand(LogIn, CanExecLogIn));
            }
        }

        private async void LogIn()
        {
            if(Login == "test" && Password == "test")
                _navigationService.NavigateTo(ViewModelLocator.ActivityView);
            else
                await _dialogService.ShowMessage("Login or password incorrect", "Login");
        }

        private bool CanExecLogIn()
        {
            if(string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password)) return true;//false;

            return true;
        }
    }
}