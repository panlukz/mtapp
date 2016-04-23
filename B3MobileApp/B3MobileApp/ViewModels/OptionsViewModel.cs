using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace B3MobileApp.ViewModels
{
    public class OptionsViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public OptionsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private int _minDistance;
        public int MinDistance
        {
            get { return _minDistance; }
            set
            {
                _minDistance = value;
                RaisePropertyChanged(() => MinDistance);
            }
        }

        private int _minTime;
        public int MinTime
        {
            get { return _minTime; }
            set
            {
                _minTime = value;
                RaisePropertyChanged(() => MinTime);
            }
        }

        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand = new RelayCommand(Save));
            }
        }

        private void Save()
        {
            _navigationService.NavigateTo(ViewModelLocator.ActivityView);
        }
    }
}
