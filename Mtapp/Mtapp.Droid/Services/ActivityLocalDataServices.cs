using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Mtapp.Droid.Services;
using Mtapp.Models;

using Mtapp.Services;
using Newtonsoft.Json;

[assembly: Xamarin.Forms.Dependency(typeof(ActivityLocalDataService))]
namespace Mtapp.Droid.Services
{
    public class ActivityLocalDataService : IActivityLocalDataService
    {
        private string _dataDirPath;

        public ActivityLocalDataService()
        {
            var appDataDirPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            _dataDirPath = Path.Combine(appDataDirPath, "data");

            //TODO catch some IOexcepitons in here?
            if (!Directory.Exists(_dataDirPath))
                Directory.CreateDirectory(_dataDirPath);
        }

        public void SaveActivity(Activity activity)
        {
            var fileName = string.Format("{0}.dat", activity.Id);
            var activityFilePath = Path.Combine(_dataDirPath, fileName);

            var activityJson = JsonConvert.SerializeObject(activity);

            //TODO catch ioexceptions
            File.WriteAllText(activityFilePath, activityJson);
        }

        public Activity GetActivity(string id)
        {
            throw new NotImplementedException();
        }

        public IList<Activity> GetAllActivities()
        {
            var activityFiles = Directory.EnumerateFiles(_dataDirPath);
            IList<Activity> activities = new List<Activity>();

            foreach (var activityFile in activityFiles)
            {
                try
                {
                    var activityJson = File.ReadAllText(activityFile);
                    var activity = JsonConvert.DeserializeObject<Activity>(activityJson);
                    activities.Add(activity);
                }
                catch (Exception ex)
                {
                    //TODO :)
                }


            }

            return activities;
        }

        public void DeleteActivity(Activity activity)
        {
            var fileName = string.Format("{0}.dat", activity.Id);
            var activityFilePath = Path.Combine(_dataDirPath, fileName);

            //TODO Surround with try/catch
            File.Delete(activityFilePath);
        }
    }
}