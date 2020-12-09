using System;
using SQLite;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace EducationTracker.Classes
{
    public class Universals
    {
        private static string dbFileName = "WGU_971.db3";
        private static string dbFolderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        public static string dbCompletePath = Path.Combine(dbFolderPath, dbFileName);

        public static Term CurrentTerm { get; set; }
        public static Course CurrentCourse { get; set; }
        public static Assessment CurrentAssessment { get; set; }

        public Term GetTerm(int id)
        {
            using (SQLiteConnection db = new SQLiteConnection(App.FilePath))
            {
                db.CreateTable<Term>();
                List<Term> term = db.Query<Term>("SELECT * FROM Term WHERE TermID = '" + id + "';").ToList();
                if (term.Count == 1)
                {
                    Term selectedTerm = term[0];
                    return selectedTerm;
                }
                else
                {
                    return null;
                }
            }
        }

        public Course GetCourse(int courseID)
        {
            using (SQLiteConnection db = new SQLiteConnection(App.FilePath))
            {
                db.CreateTable<Course>();
                List<Course> course = db.Query<Course>("SELECT * FROM Course WHERE CourseId = '" + courseID + "';").ToList();
                Course currentCourse;
                if (course.Count == 1)
                {
                    currentCourse = course[0];
                    return currentCourse;
                }
                else
                {
                    return currentCourse = null;
                }
            }
        }

        public bool IsNotNullOrEmpty(string s)
        {
            return string.IsNullOrEmpty(s);
        }

        public bool CheckPhoneFormat(string phone)
        {
            Regex validFormat = new Regex(@"[0-9]{3}-[0-9]{3}-[0-9]{4}");
            if (validFormat.IsMatch(phone))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CheckEmailFormat(string email)
        {
            Regex validFormat = new Regex(@"\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-z]{2,}\b");
            if (validFormat.IsMatch(email))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Loading All Data
        public void PopulateTerm()
        {
            Term term1 = new Term()
            {
                TermID = 1,
                TermName = "Winter 2020",
                TermStart = Convert.ToDateTime("12/01/2020"),
                TermEnd = Convert.ToDateTime("05/31/2021")
            };

        Term term2 = new Term()
        {
            TermID = 2,
                TermName = "Summer 2021",
                TermStart = Convert.ToDateTime("06/01/2021"),
                TermEnd = Convert.ToDateTime("11/30/2021")
            };

            using (SQLiteConnection db = new SQLiteConnection(App.FilePath))
            {
                db.CreateTable<Term>();
                db.Insert(term1);
                db.Insert(term2);
            }
        }

        public void PopulateCourses()
        {
            Course course1 = new Course()
            {
                CourseID = 1,
                TermID = 1,
                CourseName = "Introduction to IT - C182",
                CourseStatus = "Completed",
                CourseStart = Convert.ToDateTime("12/01/2020"),
                CourseEnd = Convert.ToDateTime("12/08/2020"),
                Notes = "",
                InstructorName = "Jen Dunlap",
                InstructorPhone = "360-480-8229",
                InstructorEmail = "jdunl28@wgu.edu",
                Notification = true
            };
            Course course2 = new Course()
            {
                CourseID = 2,
                TermID = 1,
                CourseName = "Web Development Foundations - C779",
                CourseStatus = "In Progress",
                CourseStart = Convert.ToDateTime("12/10/2020"),
                CourseEnd = Convert.ToDateTime("01/31/2021"),
                Notes = "test note",
                InstructorName = "Jen Dunlap",
                InstructorPhone = "360-480-8229",
                InstructorEmail = "jdunl28@wgu.edu",
                Notification = true
            };
            Course course3 = new Course()
            {
                CourseID = 3,
                TermID = 1,
                CourseName = "Software Quality Assurance - C857",
                CourseStatus = "Enrolled",
                CourseStart = Convert.ToDateTime("02/01/2021"),
                CourseEnd = Convert.ToDateTime("02/28/2021"),
                Notes = "test note",
                InstructorName = "Jen Dunlap",
                InstructorPhone = "360-480-8229",
                InstructorEmail = "jdunl28@wgu.edu",
                Notification = false
            };

            Course course4 = new Course()
            {
                CourseID = 4,
                TermID = 1,
                CourseName = "IT Applications C394",
                CourseStatus = "Enrolled",
                CourseStart = Convert.ToDateTime("03/01/2021"),
                CourseEnd = Convert.ToDateTime("03/31/2021"),
                Notes = "",
                InstructorName = "Jen Dunlap",
                InstructorPhone = "360-480-8229",
                InstructorEmail = "jdunl28@wgu.edu",
                Notification = false
            };
            Course course5 = new Course()
            {
                CourseID = 5,
                TermID = 1,
                CourseName = "IT Foundations C393",
                CourseStatus = "Enrolled",
                CourseStart = Convert.ToDateTime("04/01/2021"),
                CourseEnd = Convert.ToDateTime("04/30/2021"),
                Notes = "test note",
                InstructorName = "Jen Dunlap",
                InstructorPhone = "360-480-8229",
                InstructorEmail = "jdunl28@wgu.edu",
                Notification = false
            };
            Course course6 = new Course()
            {
                CourseID = 6,
                TermID = 1,
                CourseName = "Software I C# C968",
                CourseStatus = "Enrolled",
                CourseStart = Convert.ToDateTime("05/01/2021"),
                CourseEnd = Convert.ToDateTime("05/31/2021"),
                Notes = "test note",
                InstructorName = "Jen Dunlap",
                InstructorPhone = "360-480-8229",
                InstructorEmail = "jdunl28@wgu.edu",
                Notification = false
            };
            using (SQLiteConnection db = new SQLiteConnection(App.FilePath))
            {
                db.CreateTable<Course>();
                db.Insert(course1);
                db.Insert(course2);
                db.Insert(course3);
                db.Insert(course4);
                db.Insert(course5);
                db.Insert(course6);

            }
        }

        public void PopulateAssessment()
        {
            Assessment course1o = new Assessment()
            {
                AssessmentID = 1,
                CourseID = 1,
                AssessmentName = "C182 Objective Assessment",
                AssessmentType = "Objective",
                AssessmentStart = Convert.ToDateTime("12/10/2020"),
                AssessmentEnd = Convert.ToDateTime("12/10/2020"),
                Notification = false
            };
            Assessment course1p = new Assessment()
            {
                AssessmentID = 2,
                CourseID = 1,
                AssessmentName = "C182 Performance Assessment",
                AssessmentType = "Performance",
                AssessmentStart = Convert.ToDateTime("12/08/2020"),
                AssessmentEnd = Convert.ToDateTime("12/10/2020"),
                Notification = false
            };
            Assessment course2o = new Assessment()
            {
                AssessmentID = 3,
                CourseID = 2,
                AssessmentName = "C779 Objective Assessment",
                AssessmentType = "Objective",
                AssessmentStart = Convert.ToDateTime("01/31/2021"),
                AssessmentEnd = Convert.ToDateTime("01/31/2021"),
                Notification = false
            };
            Assessment course2p = new Assessment()
            {
                AssessmentID = 4,
                CourseID = 2,
                AssessmentName = "C779 Performance Assessment",
                AssessmentType = "Performance",
                AssessmentStart = Convert.ToDateTime("01/15/2021"),
                AssessmentEnd = Convert.ToDateTime("01/31/2021"),
                Notification = false
            };
            Assessment course3o = new Assessment()
            {
                AssessmentID = 5,
                CourseID = 3,
                AssessmentName = "C857 Objective Assessment",
                AssessmentType = "Objective",
                AssessmentStart = Convert.ToDateTime("02/28/2021"),
                AssessmentEnd = Convert.ToDateTime("02/28/2021"),
                Notification = false
            };
            Assessment course3p = new Assessment()
            {
                AssessmentID = 6,
                CourseID = 3,
                AssessmentName = "C857 Performance Assessment",
                AssessmentType = "Performance",
                AssessmentStart = Convert.ToDateTime("02/25/2021"),
                AssessmentEnd = Convert.ToDateTime("02/28/2021"),
                Notification = false
            };

            Assessment course4o = new Assessment()
            {
                AssessmentID = 7,
                CourseID = 4,
                AssessmentName = "C394 Objective Assessment",
                AssessmentType = "Objective",
                AssessmentStart = Convert.ToDateTime("03/24/2021"),
                AssessmentEnd = Convert.ToDateTime("3/24/2021"),
                Notification = false
            };
            Assessment course4p = new Assessment()
            {
                AssessmentID = 8,
                CourseID = 4,
                AssessmentName = "C394 Performance Assessment",
                AssessmentType = "Performance",
                AssessmentStart = Convert.ToDateTime("03/12/2021"),
                AssessmentEnd = Convert.ToDateTime("03/24/2021"),
                Notification = false
            };
            Assessment course5o = new Assessment()
            {
                AssessmentID = 9,
                CourseID = 5,
                AssessmentName = "C393 Objective Assessment",
                AssessmentType = "Objective",
                AssessmentStart = Convert.ToDateTime("4/29/2021"),
                AssessmentEnd = Convert.ToDateTime("04/29/2021"),
                Notification = false
            };
            Assessment course5p = new Assessment()
            {
                AssessmentID = 10,
                CourseID = 5,
                AssessmentName ="C393 Performance Assessment",
                AssessmentType = "Performance",
                AssessmentStart = Convert.ToDateTime("4/20/2021"),
                AssessmentEnd = Convert.ToDateTime("04/30/2021"),
                Notification = false

            };
            Assessment course6o = new Assessment()
            {
                AssessmentID = 11,
                CourseID = 6,
                AssessmentName = "C986 Objective Assessment",
                AssessmentType = "Objective",
                AssessmentStart = Convert.ToDateTime("5/15/2021"),
                AssessmentEnd = Convert.ToDateTime("05/15/2021"),
                Notification = false
            };
            Assessment course6p = new Assessment()
            {
                AssessmentID = 12,
                CourseID = 6,
                AssessmentName = "C986 Performance Assessment",
                AssessmentType = "Performance",
                AssessmentStart = Convert.ToDateTime("5/15/2021"),
                AssessmentEnd = Convert.ToDateTime("05/30/2021"),
                Notification = false
            };

            using (SQLiteConnection db = new SQLiteConnection(App.FilePath))
            {
                db.CreateTable<Assessment>();
                //course 1 assessments
                db.Insert(course1o);
                db.Insert(course1p);
                //course 2 assessments
                db.Insert(course2o);
                db.Insert(course2p);
                //course 3 assessments
                db.Insert(course3o);
                db.Insert(course3p);
                //course 4 assessments
                db.Insert(course4o);
                db.Insert(course4p);
                //course 5 assessments
                db.Insert(course5o);
                db.Insert(course5p);
                //course 6 assessments
                db.Insert(course6o);
                db.Insert(course6p);

            }
        }
    }
}
