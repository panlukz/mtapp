using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreshMvvm;
using Mtapp.Data;
using Mtapp.Models;
using PropertyChanged;

namespace Mtapp.PageModels
{
    [ImplementPropertyChanged]
    public class MainPageModel : FreshBasePageModel
    {
        private readonly IActivityRepository _activityRepository;
        private IEnumerable<Activity> _allActivities; 

        public int TotalActivities => _allActivities.Count();

        public double TotalDistance => _allActivities.Sum(a => a.Distance);

        public TimeSpan TotalTime => new TimeSpan(_allActivities.Sum(a => a.Time.Ticks));

        public double AverageSpeed => _allActivities.Sum(a => a.AverageSpeed) / _allActivities.Count();

        public MainPageModel(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
            _allActivities = _activityRepository.GetAllActivities();
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            _allActivities = _activityRepository.GetAllActivities();

            //TODO i don't like this :(
            RaisePropertyChanged("TotalActivities");
            RaisePropertyChanged("TotalDistance");
            RaisePropertyChanged("TotalTime");
            RaisePropertyChanged("AverageSpeed");
        }
    }
}
