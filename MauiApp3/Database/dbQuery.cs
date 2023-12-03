using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp3.Database
{
    public static class dbQuery
    {

        #region Terms Queries

        public static async Task AddTerm(string name, string start, string end)
        {
            await Connection.Init();

            var term = new terms()
            {

                termName = name,
                startDate = start,
                endDate = end,


            };

            await Connection._db.InsertAsync(term);

        }
        public async static Task<IEnumerable<terms>> GetTerm1()
        {


            await Connection.Init();


            var term = await Connection._db.QueryAsync<terms>("SELECT * FROM terms ORDER BY Id LIMIT 1;");


            return term;
        }
        public async static Task<IEnumerable<terms>> GetTerm2()
        {

            await Connection.Init();


            var term = await Connection._db.QueryAsync<terms>("SELECT * FROM terms ORDER BY Id LIMIT 1 OFFSET 1; ");


            return term;
        }
        public async static Task<IEnumerable<terms>> GetTerm3()
        {

            await Connection.Init();


            var term = await Connection._db.QueryAsync<terms>("SELECT * FROM terms ORDER BY Id LIMIT 1 OFFSET 2;");


            return term;
        }

        public async static Task<IEnumerable<terms>> GetTerm4()
        {

            await Connection.Init();


            var term = await Connection._db.QueryAsync<terms>("SELECT * FROM terms ORDER BY Id LIMIT 1 OFFSET 3;");


            return term;
        }
        public async static Task<IEnumerable<terms>> GetTerm5()
        {

            await Connection.Init();


            var term = await Connection._db.QueryAsync<terms>("SELECT * FROM terms ORDER BY Id LIMIT 1 OFFSET 4;");


            return term;
        }
        public async static Task<IEnumerable<terms>> GetTerm6()
        {

            await Connection.Init();


            var term = await Connection._db.QueryAsync<terms>("SELECT * FROM terms ORDER BY Id LIMIT 1 OFFSET 5;");


            return term;
        }

        public static async Task deleteTerm(int termId)
        {
            await Connection.Init();



            await Connection._db.ExecuteAsync("DELETE FROM terms WHERE Id =" + termId);

        }

        public static async Task updateTerm(int termId, string termName, string startDate, string endDate)
        {
            await Connection.Init();


            await Connection._db.ExecuteAsync("UPDATE terms SET termName ='" + termName + "', startDate = '" + startDate + "', endDate = '" + endDate + "' WHERE Id = " + termId);

        }



        #endregion


        #region Courses Queries
        public static async Task<int> AddCourse(int termId, string courseName, string start, string end, string due, int instructorId, string status, string notes, string description, bool notify)
        {
            await Connection.Init();

            var course = new courses()
            {
                termId = termId,
                courseName = courseName,
                startDate = start,
                endDate = end,
                dueDate = due,
                instructorId = instructorId,
                status = status,
                notes = notes,
                description = description,
                notify = notify

            };

            await Connection._db.InsertAsync(course);

            return course.coursesId;
        }


        public async static Task<IEnumerable<courses>> GetCourses(int termId)
        {

            await Connection.Init();


            var courses = await Connection._db.QueryAsync<courses>("SELECT * FROM courses WHERE termId =" + termId);


            return courses;
        }

        public async static Task<IEnumerable<courses>> GetCourse(int courseId)
        {

            await Connection.Init();


            var course = await Connection._db.QueryAsync<courses>("SELECT * FROM courses WHERE coursesId =" + courseId);


            return course;
        }

        public async static Task<IEnumerable<courses>> GetNotes(int courseId)
        {

            await Connection.Init();


            var notes = await Connection._db.QueryAsync<courses>("SELECT notes FROM courses WHERE coursesId =" + courseId);


            return notes;
        }


        public static async Task DeleteCourse(int courseId)
        {
            await Connection.Init();

            await Connection._db.QueryAsync<courses>("DELETE FROM courses WHERE coursesId =" + courseId);

        }

        public static async Task updateCourse(int courseId, string courseName, string description, string startDate, string endDate, string status, string notes, string dueDate, bool notify)
        {
            await Connection.Init();


            await Connection._db.ExecuteAsync("UPDATE courses SET courseName ='" + courseName + "', description ='" + description + "', startDate ='" + startDate + "', endDate = '" + endDate + "', status ='" + status + "', notes = '" + notes + "', dueDate ='" + dueDate + "', notify = " + notify + " WHERE coursesId = " + courseId);

           
        }



        #endregion


        #region PA Queries

        public static async Task<int> AddPa(string paName, int courseId, string start, string end, string due, bool notify)
        {
            await Connection.Init();

            var pa = new PAs()
            {
                paName = paName,
                coursesId = courseId,
                startDate = start,
                endDate = end,
                dueDate = due,
                notify = notify



            };

            await Connection._db.InsertAsync(pa);

            return pa.paId;
        }

        public async static Task<IEnumerable<PAs>> GetPas(int courseId)
        {

            await Connection.Init();


            var pas = await Connection._db.QueryAsync<PAs>("SELECT * FROM PAs WHERE coursesId =" + courseId);


            return pas;
        }

        public static async Task deletePA(int paId)
        {
            await Connection.Init();

            await Connection._db.QueryAsync<PAs>("DELETE FROM PAs WHERE paId =" + paId);

        }
        public static async Task<int> editPA(int paId, string paName, string start, string end, string due, bool notify)
        {
            await Connection.Init();

            await Connection._db.QueryAsync<PAs>("UPDATE PAs SET paName = '" + paName + "', startDate = '" + start + "', endDate='" + end + "', dueDate ='" + due + "', notify = " + notify + " WHERE paId = " + paId);

            return paId;
        }


        #endregion


        #region OA Queries

        public static async Task<int> AddOa(string OaName, int courseId, string start, string end, string due, bool notify)
        {
            await Connection.Init();

            var oa = new OAs()
            {
                oaName = OaName,
                coursesId = courseId,
                startDate = start,
                endDate = end,
                dueDate = due,
                notify = notify



            };

            await Connection._db.InsertAsync(oa);
            return oa.oaId;
        }

        public async static Task<IEnumerable<OAs>> GetOas(int courseId)
        {

            await Connection.Init();


            var oas = await Connection._db.QueryAsync<OAs>("SELECT * FROM OAs WHERE coursesId =" + courseId);


            return oas;
        }

        public static async Task deleteOA(int oaId)
        {
            await Connection.Init();

            await Connection._db.QueryAsync<OAs>("DELETE FROM OAs WHERE oaId =" + oaId);

        }
        public static async Task<int> editOA(int oaId, string oaName, string start, string end, string due, bool notify)
        {
            await Connection.Init();

            await Connection._db.QueryAsync<OAs>("UPDATE OAs SET oaName = '" + oaName + "', startDate = '" + start + "', endDate='" + end + "', dueDate ='" + due + "', notify = " + notify + " WHERE oaId = " + oaId);

            return oaId;
        }

        #endregion


        #region Instructor Queries

        public static async Task<int> AddInstructor(string instructorName, string phone, string email)
        {
            await Connection.Init();

            var instructor = new instructors()
            {
                instructorName = instructorName,
                phone = phone,
                eMail = email



            };

            await Connection._db.InsertAsync(instructor);

            return instructor.Id;

        }

        public async static Task<IEnumerable<instructors>> GetInstructors()
        {

            await Connection.Init();

            var instructors = await Connection._db.Table<instructors>().ToListAsync();

            return instructors;
        }

        public async static Task<IEnumerable<instructors>> GetInstructor(int instructorId)
        {

            await Connection.Init();

            var instructor = await Connection._db.QueryAsync<instructors>("SELECT * FROM instructors WHERE Id = " + instructorId);


            return instructor;
        }

        public static string GetInstructorName(int instructorId)
        {



            var instructor = Connection._db.QueryAsync<instructors>("SELECT instructorName FROM instructors WHERE Id = " + instructorId);


            return instructor.ToString();
        }


        public static async Task deleteInstructor(int instructorId)
        {
            await Connection.Init();

            await Connection._db.DeleteAsync(instructorId);

        }


        public static async Task updateInstructor(int Id, string name, string email, string phone)
        {
            await Connection.Init();


            await Connection._db.ExecuteAsync("UPDATE instructors SET instructorName ='" + name + "', eMail = '" + email + "', phone = '" + phone + "' WHERE Id = " + Id);

        }





        #endregion


        #region Notify Course Queries

        public static async Task AddNotifyCourse(int id, string courseName, string start, string end, string due)
        {
            await Connection.Init();

            var notify = new notifyCourse()
            {

                Id = id,
                courseName = courseName,
                start = start,
                end = end,
                dueDate = due,


            };

            await Connection._db.InsertAsync(notify);

        }

        public async static Task<IEnumerable<notifyCourse>> getNotifyCourses()
        {

            await Connection.Init();


            var notify = await Connection._db.QueryAsync<notifyCourse>("SELECT * FROM notifyCourse");


            return notify;
        }

        public async static Task<IEnumerable<notifyCourse>> getCourseNotify(int courseId)
        {

            await Connection.Init();


            var notify = await Connection._db.QueryAsync<notifyCourse>("SELECT * FROM notifyCourse WHERE Id =" + courseId );


            return notify;
        }
        public static async Task updateCourseNotify(int courseId, string courseName,string startDate, string endDate, string dueDate)
        {
            await Connection.Init();


            await Connection._db.ExecuteAsync("UPDATE notifyCourse SET courseName ='" + courseName + "', start'" + startDate + "', endDate = '" + endDate + "', dueDate ='" + dueDate + "' WHERE Id = " + courseId);

        }

        public static async Task deleteCourseNotify(int courseId)
        {
            await Connection.Init();

            await Connection._db.QueryAsync<notifyCourse>("DELETE FROM notifyCourse WHERE Id =" + courseId);

        }

        #endregion


        #region Notify PA


        public static async Task AddNotifyPA(int id, string paName, string start, string end, string due)
        {
            await Connection.Init();

            var notify = new notifyPA()
            {

                Id = id,
                paName = paName,
                start = start,
                end = end,
                dueDate = due,


            };

            await Connection._db.InsertAsync(notify);

        }


        public async static Task<IEnumerable<notifyPA>> getNotifyPAs()
        {

            await Connection.Init();


            var notify = await Connection._db.QueryAsync<notifyPA>("SELECT * FROM notifyPA");


            return notify;
        }


        public async static Task<IEnumerable<notifyPA>> getPaNotify(int paId)
        {

            await Connection.Init();


            var notify = await Connection._db.QueryAsync<notifyPA>("SELECT * FROM notifyPA WHERE Id =" + paId);


            return notify;
        }

        public static async Task updatePaNotify(int paId, string paName, string startDate, string endDate, string dueDate)
        {
            await Connection.Init();


            await Connection._db.ExecuteAsync("UPDATE notifyPA SET paName = '" + paName + "', start ='" + startDate + "', endDate = '" + endDate + "', dueDate ='" + dueDate + "' WHERE Id = " + paId);

        }

        public static async Task deletePaNotify(int paId)
        {
            await Connection.Init();

            await Connection._db.QueryAsync<notifyPA>("DELETE FROM notifyPA WHERE Id =" + paId);

        }

        #endregion


        #region Notify OA


        public static async Task AddNotifyOA(int id, string oaName, string start, string end, string due)
        {
            await Connection.Init();

            var notify = new notifyOA()
            {

                Id = id,
                oaName = oaName,
                start = start,
                end = end,
                dueDate = due,


            };

            await Connection._db.InsertAsync(notify);

        }

        public async static Task<IEnumerable<notifyOA>> getNotifyOAs()
        {

            await Connection.Init();


            var notify = await Connection._db.QueryAsync<notifyOA>("SELECT * FROM notifyOA");


            return notify;
        }


        public async static Task<IEnumerable<notifyOA>> getOaNotify(int oaId)
        {

            await Connection.Init();


            var notify = await Connection._db.QueryAsync<notifyOA>("SELECT * FROM notifyOA WHERE Id =" + oaId);


            return notify;
        }

        public static async Task updateOaNotify(int oaId, string oaName, string startDate, string endDate, string dueDate)
        {
            await Connection.Init();


            await Connection._db.ExecuteAsync("UPDATE notifyOA SET oaName ='" + oaName + "', start'" + startDate + "', endDate = '" + endDate + "', dueDate ='" + dueDate + "' WHERE Id = " + oaId);

        }

        public static async Task deleteOaNotify(int oaId)
        {
            await Connection.Init();

            await Connection._db.QueryAsync<notifyPA>("DELETE FROM notifyOA WHERE Id =" + oaId);

        }

        #endregion


    }
}

