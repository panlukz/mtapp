using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreshMvvm;
using Mtapp.Models;
using PropertyChanged;
using Xamarin.Forms;

namespace Mtapp.PageModels
{
    [ImplementPropertyChanged]
    public class ActivityDetailsPageModel : FreshBasePageModel
    {
        private Activity _activity;

        public ActivityDetailsPageModel()
        {

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
                return new Command(() =>
                {
                    //TODO Save Activity here!
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
