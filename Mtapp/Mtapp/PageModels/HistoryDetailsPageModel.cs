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

        public HistoryDetailsPageModel(IActivityLocalDataService activityLocalDataService)
        {
            _activityLocalDataService = activityLocalDataService;
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
    }
}
