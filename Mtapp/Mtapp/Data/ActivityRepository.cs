using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mtapp.Helpers;
using Mtapp.Models;
using SQLite;

namespace Mtapp.Data
{
    class ActivityRepository : Repository, IActivityRepository
    {
        public ActivityRepository(ISQLite sqlite) : base(sqlite)
        {
        }

        public IEnumerable<Activity> GetAllActivities()
        {
            return Connection.Table<Activity>().ToList();
        }

        public Activity GetActivityById(string activityId)
        {
            return Connection.Table<Activity>().FirstOrDefault(a => a.Id.Equals(activityId));
        }

        public bool SaveActivity(Activity activity)
        {
            int result = 0;
            if (Connection.Table<Activity>().First(a => a.Id.Equals(activity.Id)) != null)
                result = Connection.Update(activity);
            else
                result = Connection.Insert(activity);

            return result != 0;
        }

        public void SaveActivities(IEnumerable<Activity> activities)
        {
            Connection.BeginTransaction();

            foreach (Activity activity in activities)
            {
                SaveActivity(activity);
            }

            Connection.Commit();
        }

        public bool DeleteActivity(string id)
        {
            int result = 0;
            var activityToDelete = Connection.Table<Activity>().FirstOrDefault(a => a.Id.Equals(id));
            if (activityToDelete != null)
                result = Connection.Delete<Activity>(activityToDelete);

            return result != 0;
        }

        public bool DeleteActivity(Activity activity)
        {
            int result = 0;
            result = Connection.Delete<Activity>(activity);

            return result != 0;
        }
    }
}
