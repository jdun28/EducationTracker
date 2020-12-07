using System;
using SQLite;
namespace EducationTracker.Classes
{
    public class Assessment
    {
        [PrimaryKey, AutoIncrement] public int AssessmentID { get; set; }

        public int CourseID { get; set; }
        public string AssessmentName { get; set; }
        public string AssessmentType { get; set; }
        public DateTime AssessmentStart { get; set; }
        public DateTime AssessmentEnd { get; set; }

        public Assessment()
        {
        }
    }
}
