using System;
using SQLite;
namespace EducationTracker.Classes
{
    public class Course
    {
        [PrimaryKey, AutoIncrement] public int CourseID { get; set; }

        public int TermID { get; set; }
        public string CourseName { get; set; }
        public string CourseStatus { get; set; }
        public DateTime CourseStart { get; set; }
        public DateTime CourseEnd { get; set; }
        public string Notes { get; set; }
        public string InstructorName { get; set; }
        public string InstructorPhone { get; set; }
        public string InstructorEmail { get; set; }


        public Course()
        {
        }
    }
}
