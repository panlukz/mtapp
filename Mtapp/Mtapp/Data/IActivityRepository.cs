using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mtapp.Models;

namespace Mtapp.Data
{
    public interface IActivityRepository
    {
        IEnumerable<Activity> GetAllActivities();

        Activity GetActivityById(string activityId);

        bool SaveActivity(Activity activity);

        void SaveActivities(IEnumerable<Activity> activities);

        bool DeleteActivity(string id);

        bool DeleteActivity(Activity activity);

    }
}
