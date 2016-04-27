using B3MobileApp.Helpers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Plugin.Settings.Abstractions;

namespace B3MobileApp.ViewModels
{
    public class OptionsViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public OptionsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            MinDistance = Settings.GpsMinDistance;
            MinTime = Settings.GpsMinTime;
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

        private string _postWebServiceUri;
        public string PostWebServiceUri
        {
            get { return _postWebServiceUri; }
            set
            {
                _postWebServiceUri = value;
                RaisePropertyChanged(() => PostWebServiceUri);
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
            Settings.GpsMinDistance = MinDistance;
            Settings.GpsMinTime = MinTime;
            _navigationService.NavigateTo(ViewModelLocator.ActivityView);
        }
    }
}
