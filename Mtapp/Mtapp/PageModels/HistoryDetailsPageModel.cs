using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreshMvvm;
using Mtapp.Data;
using Mtapp.Models;
using Mtapp.Services;
using PropertyChanged;
using Xamarin.Forms;

namespace Mtapp.PageModels
{
    [ImplementPropertyChanged]
    public class HistoryDetailsPageModel : FreshBasePageModel
    {
        private readonly IActivityDataService _activityDataService;
        private readonly IActivityRepository _activityRepository;

        public HistoryDetailsPageModel(IActivityDataService activityDataService, IActivityRepository activityRepository)
        {
            _activityDataService = activityDataService;
            _activityRepository = activityRepository;
        }

        private Activity _activity;
        public Activity Activity { get; set; }

        public override void Init(object activity)
        {
            base.Init(activity);
            _activity = (Activity) activity;
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            Activity = _activity;
        }

        public Command SendActivityToServerCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (Activity != null)
                    {
                        try
                        {
                            await _activityDataService.Add(Activity);
                        }
                        catch (Exception ex)
                        {
                            await CoreMethods.DisplayAlert("Error", ex.Message, "Ok");
                        }
                    }
                });
            }
        }
    }
}
