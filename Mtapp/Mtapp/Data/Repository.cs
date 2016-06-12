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
    abstract class Repository
    {

        protected SQLiteConnection Connection;

        protected Repository(ISQLite sqLite)
        { 
            Connection = sqLite.GetConnection();
        }

    }
}
