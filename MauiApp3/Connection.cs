using Android.Systems;
using Android.Telecom;
using Microsoft.Maui.Controls.Compatibility;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp3
{
    public static class Connection
    {
        public static SQLiteAsyncConnection _db;
        public static SQLiteConnection _dbconnection;

        public static async Task Init()
        {
            if (_db != null)
            {
                return;
            }


            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "data.db");

            _db = new SQLiteAsyncConnection(databasePath);
            _dbconnection = new SQLiteConnection(databasePath);



            await _db.CreateTableAsync<terms>();

            await _db.CreateTableAsync<courses>();
            await _db.CreateTableAsync<PAs>();
            await _db.CreateTableAsync<OAs>();
            await _db.CreateTableAsync<instructors>();

        }

    }
}
