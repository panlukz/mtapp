using System;
using System.Collections.Generic;
using System.Linq;
using FreshMvvm;
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

        public IList<Activity> Activities { get; set; }

        public HistoryPageModel(IActivityLocalDataService activityLocalDataService)
        {
            _activityLocalDataService = activityLocalDataService;
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            Activities = _activityLocalDataService.GetAllActivities().OrderByDescending(a => a.Date).ToList();
        }


        public Command ShowActivityDetailsCommand
        {
            get
            {
                return new Command(async (activityId) =>
                {
                    await CoreMethods.PushPageModel<HistoryDetailsPageModel>(activityId);

                });
            }
        }

    }
}