using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Mtapp.Data;
using Mtapp.Droid.Data;
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;
using Xamarin.Forms;

[assembly:Dependency(typeof(Mtapp.Droid.Data.SQLite))]
namespace Mtapp.Droid.Data
{
    public class SQLite : ISQLite
    {
        private const string DbFileName = "db.sqlite";
        private readonly string dbFilePath;

        public SQLiteConnection GetConnection()
        {
            var dbFilePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var platform = new SQLitePlatformAndroid();
            return new SQLiteConnection(platform, Path.Combine(dbFilePath, DbFileName));
        }
    }
}