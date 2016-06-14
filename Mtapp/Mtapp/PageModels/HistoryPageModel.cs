using System;
using System.Collections.Generic;
using System.Linq;
using FreshMvvm;
using Mtapp.Data;
using Mtapp.Models;
using Mtapp.Services;
using PropertyChanged;
using Xamarin.Forms;

namespace Mtapp.PageModels
{
    [ImplementPropertyChanged]
    public class HistoryPageModel : FreshBasePageModel
    {
        private readonly IActivityLocalDataService _activityLocalDataService;
        private readonly IActivityRepository _activityRepository;

        public IList<Activity> Activities { get; set; }

        public HistoryPageModel(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            Activities = _activityRepository.GetAllActivities().OrderByDescending(a => a.Date).ToList();
        }


        public Command ShowActivityDetailsCommand
        {
            get
            {
                return new Command(async (activity) =>
                {
                    var activityId = ((Activity) activity).Id;
                    await CoreMethods.PushPageModel<HistoryDetailsPageModel>(activityId);
                });
            }
        }

    }
}