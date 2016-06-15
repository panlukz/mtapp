using System;
using FreshMvvm;
using Mtapp.Data;
using Mtapp.Models;
using Xamarin.Forms;

namespace Mtapp.PageModels
{
    public class EditActivityInfoPageModel : FreshBasePageModel
    {
        private readonly IActivityRepository _activityRepository;
        private Activity _activity;

        public string Name
        {
            get { return _activity.Name; }
            set { _activity.Name = value; }
        }

        public string Description
        {
            get { return _activity.Description; }
            set { _activity.Description = value; }
        }

        public EditActivityInfoPageModel(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public override void Init(object activityId)
        {
            base.Init(activityId);

            _activity = _activityRepository.GetActivityById((string) activityId);

            if (_activity == null)
            {
                CoreMethods.DisplayAlert("Error", "Can't load an activity", "Ok");
                CoreMethods.PopPageModel();
            }
        }

        public Command UpdateActivityInfoCommand
        {
            get
            {
                return new Command( async () =>
                {
                    _activityRepository.SaveActivity(_activity);
                    await CoreMethods.PopPageModel();
                });
            }
        }

        public Command CancelUpdateCommand
        {
            get
            {
                return new Command( async () =>
                {
                    await CoreMethods.PopPageModel();
                });
            }
        }

    }
}