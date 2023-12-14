using SQLite;

namespace MauiApp3.Database
{
    public class terms
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string termName { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
    }
    public class courses
    {
        [PrimaryKey, AutoIncrement]
        public int coursesId { get; set; }
        public int termId { get; set; }

        public string courseName { get; set; }

        public string startDate { get; set; }
        public string endDate { get; set; }

        public string dueDate { get; set; }

        public int instructorId { get; set; }

        public string status { get; set; }

        public string notes { get; set; }

        public string description { get; set; }

        public bool notify { get; set; }

    }

    public class PAs
    {
        [PrimaryKey, AutoIncrement]
        public int paId { get; set; }
        public int coursesId { get; set; }
        public string paName { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }

        public string dueDate { get; set; }

        public bool notify { get; set; }

    }

    public class OAs
    {
        [PrimaryKey, AutoIncrement]
        public int oaId { get; set; }

        public int coursesId { get; set; }
        public string oaName { get; set; }

        public string startDate { get; set; }
        public string endDate { get; set; }

        public string dueDate { get; set; }

        public bool notify { get; set; }

    }

    public class instructors
    {
        [PrimaryKey, AutoIncrement]

        public int Id { get; set; }

        public string instructorName { get; set; }

        public string phone { get; set; }

        public string eMail { get; set; }

    }

  
}
