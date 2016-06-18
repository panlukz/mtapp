using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mtapp.Models;

namespace Mtapp.Data
{
    public interface IActivityRepository : IRepository<Activity>
    {
        IEnumerable<Activity> GetActivitiesFromMonth(int month, int year);
    }
}
