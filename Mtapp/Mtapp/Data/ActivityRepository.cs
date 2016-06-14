using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mtapp.Helpers;
using Mtapp.Models;
using SQLite;
using SQLiteNetExtensions.Extensions;

namespace Mtapp.Data
{
    class ActivityRepository : Repository, IActivityRepository
    {
        public ActivityRepository(ISQLite sqlite) : base(sqlite)
        {
            Connection.CreateTable<Activity>();
            Connection.CreateTable<ActivityPosition>();
        }

        public IEnumerable<Activity> GetAllActivities()
        {
            return Connection.Table<Activity>();
        }

        public Activity GetActivityById(string activityId)
        {
            return Connection.GetWithChildren<Activity>(activityId);
        }

        public bool SaveActivity(Activity activity)
        {
            int result = 0;
            if (Connection.Table<Activity>().FirstOrDefault(a => a.Id.Equals(activity.Id)) != null)
                Connection.UpdateWithChildren(activity);
            else
                Connection.InsertWithChildren(activity);

            //TODO consider about this returns!
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
                result = Connection.Delete<Activity>(id);

            return result != 0;
        }

        public bool DeleteActivity(Activity activity)
        {
            int result = 0;
            result = Connection.Delete<Activity>(activity.Id);

            return result != 0;
        }
    }
}
