using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreshMvvm;
using FreshTinyIoC;
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

        private string _activityId;
        public Activity Activity { get; set; }

        public override void Init(object activityId)
        {
            base.Init(activityId);
            _activityId = ((string)activityId);
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            Activity = _activityRepository.GetById(_activityId);
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

        public Command DeleteActivityCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (Activity != null)
                    {
                        var decision = await CoreMethods.DisplayAlert("Are you sure?", "Delete activity?", "Yes", "No");
                        if (decision)
                        {
                            _activityRepository.Delete(Activity);
                            await CoreMethods.PopPageModel();
                        }
                    }
                });
            }
        }

        public Command EditActivityInfoCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await CoreMethods.PushPageModel<EditActivityInfoPageModel>(Activity.Id);
                });
            }
        }


    }
}
