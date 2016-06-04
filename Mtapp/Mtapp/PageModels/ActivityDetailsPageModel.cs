using System;
using System.Collections.Generic;
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
    public class ActivityDetailsPageModel : FreshBasePageModel
    {
        private readonly IActivityLocalDataService _activityLocalDataService;
        private Activity _activity;

        public ActivityDetailsPageModel(IActivityLocalDataService activityLocalDataService)
        {
            _activityLocalDataService = activityLocalDataService;
        }

        #region Properties

        public Activity Activity { get; set; }

        #endregion

        #region Override base methods

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            Activity = _activity;
        }

        public override void Init(object initData)
        {
            _activity = (Activity) initData;

            base.Init(initData);

        }

        #endregion

        #region Commands

        public Command SaveActivityCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (string.IsNullOrWhiteSpace(Activity.Name))
                        Activity.Name = string.Format("Fajna wycieczka {0}", DateTime.Now);

                    _activityLocalDataService.SaveActivity(Activity);
                    await CoreMethods.PopPageModel();
                });
            }
        }

        public Command RejectActivityCommand
        {
            get
            {
                return new Command( async () =>
                {
                    var results = await CoreMethods.DisplayAlert("Are you sure?", "Discard current activity?", "Yes", "No");

                    if (results)
                        await CoreMethods.PopPageModel();
                });
            }
        }

        #endregion

    }
}
