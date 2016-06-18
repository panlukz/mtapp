using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mtapp.Helpers;
using Mtapp.Models;
using SQLite;
using SQLite.Net;
using SQLiteNetExtensions.Extensions;

namespace Mtapp.Data
{
    class ActivityRepository : IActivityRepository
    {
        private readonly SQLiteConnection _db;

        public ActivityRepository(ISQLite sqlite)
        {
            _db = sqlite.GetConnection();
            _db.CreateTable<Activity>();
            _db.CreateTable<ActivityPosition>();
        }

        public IEnumerable<Activity> GetAll()
        {
            return _db.Table<Activity>();
        }

        public Activity GetById(string activityId)
        {
            return _db.GetWithChildren<Activity>(activityId);
        }

        public bool Save(Activity activity)
        {
            int result = 0;
            if (_db.Table<Activity>().FirstOrDefault(a => a.Id.Equals(activity.Id)) != null)
                _db.UpdateWithChildren(activity);
            else
                _db.InsertWithChildren(activity);

            //TODO consider about this returns!
            return result != 0;
        }

        public void SaveAll(IEnumerable<Activity> activities)
        {
            _db.BeginTransaction();

            foreach (Activity activity in activities)
            {
                Save(activity);
            }

            _db.Commit();
        }

        public bool Delete(string id)
        {
            int result = 0;
            var activityToDelete = _db.Table<Activity>().FirstOrDefault(a => a.Id.Equals(id));
            if (activityToDelete != null)
                result = _db.Delete<Activity>(id);

            return result != 0;
        }

        public bool Delete(Activity activity)
        {
            int result = 0;
            result = _db.Delete<Activity>(activity.Id);

            return result != 0;
        }

        public IEnumerable<Activity> GetActivitiesFromMonth(int month, int year)
        {
            return _db.Table<Activity>().Where(a => a.Date.Year == year && a.Date.Month == month).ToList();
        }
    }
}
