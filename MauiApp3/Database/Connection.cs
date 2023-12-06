
using SQLite;


namespace MauiApp3.Database
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

            else
            {
                var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "data.db");

                _db = new SQLiteAsyncConnection(databasePath);
                _dbconnection = new SQLiteConnection(databasePath);

                await _db.CreateTableAsync<terms>();
                await _db.CreateTableAsync<courses>();
                await _db.CreateTableAsync<PAs>();
                await _db.CreateTableAsync<OAs>();
                await _db.CreateTableAsync<instructors>();
                await _db.CreateTableAsync<notifyCourse>();
                await _db.CreateTableAsync<notifyPA>();
                await _db.CreateTableAsync<notifyOA>();



                #region SAMPLE DATA 


                int termId = await dbQuery.AddTerm("Winter Term", DateTime.Now.ToShortDateString(), DateTime.Now.AddMonths(6).ToShortDateString());
                int instructorId = await dbQuery.AddInstructor("Anika Patel", "555-123-4567", "anika.patel@strimeuniversity.edu");
                int courseId = await dbQuery.AddCourse(termId, ".Net Maui 101", DateTime.Now.ToShortDateString(), DateTime.Now.AddDays(3).ToShortDateString(), DateTime.Now.AddDays(7).ToShortDateString(), instructorId, "Active", "This course used to be based off of Xamarin", "Use your knowledge and skills to create a cross platform software application", false);
                await dbQuery.AddOa("Objective Assessment", courseId, DateTime.Now.AddDays(1).ToShortDateString(), DateTime.Now.AddDays(2).ToShortDateString(), DateTime.Now.AddDays(7).ToShortDateString(), false);
                await dbQuery.AddPa("Performance Assessment", courseId, DateTime.Now.AddDays(1).ToShortDateString(), DateTime.Now.AddDays(2).ToShortDateString(), DateTime.Now.AddDays(7).ToShortDateString(), false);

                #endregion




            }


        }

    }
}
