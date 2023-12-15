using Plugin.LocalNotification;
using System.Xml.Linq;

namespace MauiApp3.Database
{
    public static class dbQuery
    {

        #region Terms Queries

        public static async Task<int> AddTerm(string name, string start, string end)
        {
            await Connection.Init();

            var term = new terms()
            {

                termName = name,
                startDate = start,
                endDate = end,


            };

            await Connection._db.InsertAsync(term);

            return term.Id;

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

            var course = await dbQuery.GetCourses(termId);

            for (int i = 0; i < course.Count(); i++)
            {
                await dbQuery.DeleteCourse(course.ElementAt(i).coursesId);

            }


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



             deleteCourseNotify(courseId);

            var course = await GetCourse(courseId);


            await dbQuery.deleteInstructor(course.FirstOrDefault().instructorId);

            var oa = await dbQuery.GetOas(courseId);

            for (int x = 0; x < oa.Count(); x++)
            {
                await deleteOA(oa.ElementAt(x).oaId);
                 deleteOaNotify(oa.ElementAt(x).oaId);
            }

            var pa = await dbQuery.GetPas(courseId);

            for (int y = 0; y < pa.Count(); y++)
            {
                await dbQuery.deletePA(pa.ElementAt(y).paId);
                deletePaNotify(pa.ElementAt(y).paId);
            }

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

            deletePaNotify(paId);

        }

        public static async Task<int> editPA(int paId, string paName, string start, string end, string due, bool notify)
        {
            await Connection.Init();

            await Connection._db.QueryAsync<PAs>("UPDATE PAs SET paName = '" + paName + "', startDate = '" + start + "', endDate='" + end + "', dueDate ='" + due + "', notify = " + notify + " WHERE paId = " + paId);

            return paId;
        }

        public static async Task<int> countPA(int courseId)
        {
            await Connection.Init();

            var count = await Connection._db.QueryAsync<PAs>("SELECT * FROM PAs WHERE coursesId =" + courseId);

            return count.Count;
        }

        #endregion


        #region OA Queries

        public static async Task<int> countOA(int courseId)
        {
            await Connection.Init();

            var count = await Connection._db.QueryAsync<OAs>("SELECT * FROM OAs WHERE coursesId =" + courseId);

            return count.Count;
        }

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

            deleteOaNotify(oaId);

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

        public async static Task<IEnumerable<instructors>> GetInstructor(int instructorId)
        {

            await Connection.Init();

            var instructor = await Connection._db.QueryAsync<instructors>("SELECT * FROM instructors WHERE Id = " + instructorId);


            return instructor;
        }


        public static async Task deleteInstructor(int instructorId)
        {
            await Connection.Init();

            await Connection._db.QueryAsync<instructors>("DELETE FROM instructors WHERE Id = " + instructorId);

        }


        public static async Task updateInstructor(int Id, string name, string email, string phone)
        {
            await Connection.Init();


            await Connection._db.ExecuteAsync("UPDATE instructors SET instructorName ='" + name + "', eMail = '" + email + "', phone = '" + phone + "' WHERE Id = " + Id);

        }


        #endregion


        #region Notify Course

        public static async Task AddNotifyCourse(int id, string courseName, string start, string end, string due)
        {


            #region Notify Start

            string startId = id.ToString() + 1.ToString();


            var notifyStart = new NotificationRequest
            {
                NotificationId = int.Parse(startId),
                Title = "Upcoming Start Date",
                Description = courseName + " is expected to start on " + start,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Now.AddSeconds(5)

                }
                
            };

            await LocalNotificationCenter.Current.Show(notifyStart);

            #endregion

            #region Notify End
            string endId = id.ToString() + 2.ToString();

            var notifyEnd = new NotificationRequest
            {
                NotificationId = int.Parse(endId),
                Title = "Upcoming End Date",
                Description = courseName + " is expected to end on " + end,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Now.AddSeconds(5)

                }
            };

            await LocalNotificationCenter.Current.Show(notifyEnd);

            #endregion

            #region Notify Due
            string dueId = id.ToString() + 3.ToString();

            var notifyDue = new NotificationRequest
            {
                NotificationId = int.Parse(dueId),
                Title = "Upcoming Due Date",
                Description = courseName + " is due on " + due,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Now.AddSeconds(5)

                }
            };

            await LocalNotificationCenter.Current.Show(notifyDue);

            #endregion
        }

        public static void deleteCourseNotify(int courseId)
        {
          

            string startId = courseId.ToString() + 1.ToString();
            string endId = courseId.ToString() + 2.ToString();
            string dueId = courseId.ToString() + 3.ToString();

            LocalNotificationCenter.Current.Cancel(int.Parse(startId));
            LocalNotificationCenter.Current.Cancel(int.Parse(endId));
            LocalNotificationCenter.Current.Cancel(int.Parse(dueId));

        }

        #endregion


        #region Notify PA


        public static async Task AddNotifyPA(int id, string paName, string start, string end, string due)
        {
           

            #region Notify Start

            string startId = id.ToString() + 1.ToString();

            var notifyStart = new NotificationRequest
            {
                NotificationId = int.Parse(startId),
                Title = "Upcoming Assessment Start Date",
                Description = paName + " is expected to start on " + start,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Now.AddSeconds(5)

                }
            };

            await LocalNotificationCenter.Current.Show(notifyStart);

            #endregion

            #region Notify End
            string endId = id.ToString() + 2.ToString();

            var notifyEnd = new NotificationRequest
            {
                NotificationId = int.Parse(endId),
                Title = "Upcoming Assessment End Date",
                Description = paName + " is expected to end on " + end,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Now.AddSeconds(5)

                }
            };

            await LocalNotificationCenter.Current.Show(notifyEnd);

            #endregion

            #region Notify Due

            string dueId = id.ToString() + 3.ToString();

            var notifyDue = new NotificationRequest
            {
                NotificationId = int.Parse(dueId),
                Title = "Upcoming Assessment Due Date",
                Description = paName + " is due on " + due,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Now.AddSeconds(5)

                }
            };

            await LocalNotificationCenter.Current.Show(notifyDue);

            #endregion


        }


        public static void  deletePaNotify(int paId)
        {
            string startId = paId.ToString() + 1.ToString();
            string endId = paId.ToString() + 2.ToString();
            string dueId = paId.ToString() + 3.ToString();

            LocalNotificationCenter.Current.Cancel(int.Parse(startId));
            LocalNotificationCenter.Current.Cancel(int.Parse(endId));
            LocalNotificationCenter.Current.Cancel(int.Parse(dueId));
        }

        #endregion


        #region Notify OA


        public static async Task AddNotifyOA(int id, string oaName, string start, string end, string due)
        {

            #region Notify Start

            string startId = id.ToString() + 1.ToString();

            var notifyStart = new NotificationRequest
            {
                NotificationId = int.Parse(startId),
                Title = "Upcoming Assessment Start Date",
                Description = oaName + " is expected to start on " + start,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Now.AddSeconds(5)

                }
            };

            await LocalNotificationCenter.Current.Show(notifyStart);

            #endregion

            #region Notify End
            string endId = id.ToString() + 2.ToString();

            var notifyEnd = new NotificationRequest
            {
                NotificationId = int.Parse(endId),
                Title = "Upcoming Assessment End Date",
                Description = oaName + " is expected to end on " + end,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Now.AddSeconds(5)

                }
            };

            await LocalNotificationCenter.Current.Show(notifyEnd);

            #endregion

            #region Notify Due

            string dueId = id.ToString() + 3.ToString();

            var notifyDue = new NotificationRequest
            {
                NotificationId = int.Parse(dueId),
                Title = "Assessment Due",
                Description = oaName + " is due on " + due,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Now.AddSeconds(5)

                }
            };

            await LocalNotificationCenter.Current.Show(notifyDue);

            #endregion

        }

        public static void deleteOaNotify(int oaId)
        {
            string startId = oaId.ToString() + 1.ToString();
            string endId = oaId.ToString() + 2.ToString();
            string dueId = oaId.ToString() + 3.ToString();

            LocalNotificationCenter.Current.Cancel(int.Parse(startId));
            LocalNotificationCenter.Current.Cancel(int.Parse(endId));
            LocalNotificationCenter.Current.Cancel(int.Parse(dueId));
        }

        #endregion


    }
}

