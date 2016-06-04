using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreshMvvm;
using Mtapp.Models;
using Mtapp.Services;
using PropertyChanged;
using Xamarin.Forms;

namespace Mtapp.PageModels
{
    [ImplementPropertyChanged]
    public class HistoryDetailsPageModel : FreshBasePageModel
    {
        private readonly IActivityLocalDataService _activityLocalDataService;
        private readonly IActivityDataService _activityDataService;

        public HistoryDetailsPageModel(IActivityLocalDataService activityLocalDataService, IActivityDataService activityDataService)
        {
            _activityLocalDataService = activityLocalDataService;
            _activityDataService = activityDataService;
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
