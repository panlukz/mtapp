using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mtapp.Helpers;
using Mtapp.Models;
using SQLite;
using SQLite.Net;

namespace Mtapp.Data
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();

        T GetById(string id);

        bool Save(T item);

        void SaveAll(IEnumerable<T> items);

        bool Delete(string id);

        bool Delete(T item);
    }
}
