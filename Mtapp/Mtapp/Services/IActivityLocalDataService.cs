using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mtapp.Models;

namespace Mtapp.Services
{
    public interface IActivityLocalDataService
    {
        void SaveActivity(Activity activity);

        Activity GetActivity(string id);

        IList<Activity> GetAllActivities();

        void DeleteActivity(Activity activity);
    }
}
