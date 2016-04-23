using System.Collections.ObjectModel;
using B3MobileApp.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Position = B3MobileApp.Model.Position;

namespace B3MobileApp.ViewModels
{
    public class ActivityViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        public ActivityViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;

            Positions = new ObservableCollection<Model.Position>();

            activity = new Activity();
            activity.ActuallPositionChanged += PositionChanged;
        }

        private Activity activity;

        private void PositionChanged(object sender, Model.PositionEventArgs e)
        {
            var newPosition = new Position
            {
                Latitude = e.Position.Latitude,
                Longitude = e.Position.Longitude,
                Altitude = e.Position.Altitude,
                Speed = e.Position.Speed,
                Timestamp = e.Position.Timestamp
            };

            Positions.Add(newPosition);

            ActuallPosition = newPosition;
        }

        public ObservableCollection<Position> Positions
        {
            get { return _positions; }
            set
            {
                _positions = value;
                RaisePropertyChanged(() => Positions);
            }
        }

        private Position _actuallPosition;
        public Position ActuallPosition
        {
            get { return activity.ActuallPosition; }
            set
            {
                _actuallPosition = value;
                RaisePropertyChanged(() => ActuallPosition);
            }
        }


        public bool IsActivityStarted { get; set; }

        #region Commands

        private RelayCommand _showOptionsCommand;
        public RelayCommand ShowOptionsCommand
        {
            get {
                    return _showOptionsCommand 
                    ?? (_showOptionsCommand = new RelayCommand(
                    () =>
                    {
                        _navigationService.NavigateTo(ViewModelLocator.OptionsView);
                    }));
            }
        }

        private RelayCommand _startActivityCommand;
        public RelayCommand StartActivityCommand
        {
            get
            {
                return _startActivityCommand
                    ?? (_startActivityCommand = new RelayCommand(
                    () =>
                    {
                        IsActivityStarted = true;
                        StartActivityCommand.RaiseCanExecuteChanged();
                        StopActivityCommand.RaiseCanExecuteChanged();


                        //TODO remember to implement isListening, itsAvaiableOnDevice and etc exception handlers!
                        activity.Start();

                    },
                    () =>
                    {
                        return !IsActivityStarted;
                    }));
            }
        }

        private RelayCommand _stopActivityCommand;
        private ObservableCollection<Position> _positions;

        public RelayCommand StopActivityCommand
        {
            get
            {
                return _stopActivityCommand
                    ?? (_stopActivityCommand = new RelayCommand(
                    async () =>
                    {
                        await _dialogService.ShowMessage("Are you sure?", "Stop activity", "Yes", "No", 
                            decision =>
                            {
                                if(decision)
                                {
                                    IsActivityStarted = false;

                                    StartActivityCommand.RaiseCanExecuteChanged();
                                    StopActivityCommand.RaiseCanExecuteChanged();

                                    activity.Stop();
                                }
                            });

                    },
                    () =>
                    {
                        return IsActivityStarted;
                    }));
            }
        }

        #endregion

    }
}
