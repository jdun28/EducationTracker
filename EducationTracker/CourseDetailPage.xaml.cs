using System;
using System.Collections.Generic;
using EducationTracker.Classes;
using System.Linq;
using SQLite;

using Xamarin.Forms;

namespace EducationTracker
{
    public partial class CourseDetailPage : ContentPage
    {
        int termId;
        public CourseDetailPage(Course course)
        {
            InitializeComponent();
            
            Universals.CurrentCourse = course;
            termId = course.TermID;

            courseNameDetail.Text = course.CourseName;
            statusDetail.Text = course.CourseStatus;
            startDateDetail.Text = course.CourseStart.ToString();
            endDateDetail.Text = course.CourseEnd.ToString();
            instructorNameDetail.Text = course.InstructorName;
            emailDetail.Text = course.InstructorEmail;
            phoneDetail.Text = course.InstructorPhone;

            if(course.Notes.Length > 0)
            {
                notesDetail.Text = course.Notes;
            }
            else
            {
                notesTitle.IsVisible = false;
                notesDetail.IsVisible = false;
            }

        }

        void ViewAssessments_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new AssessmentDetailPage(Universals.CurrentCourse));
        }

        void editCourseButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new AddEditCourse(Universals.CurrentCourse));
        }
        void DeleteCourseButton_Clicked(System.Object sender, System.EventArgs e)
        {
            using (SQLiteConnection db = new SQLiteConnection(App.FilePath))
            {
                db.CreateTable<Course>();
                int toDelete = db.Delete(Universals.CurrentCourse);
                DisplayAlert("Alert", "Course has been deleted successfully.", "Continue");
                Universals.CurrentTerm = GetTerm(termId);
                Navigation.PushAsync(new CoursesDetail(Universals.CurrentTerm));
            }
        }
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
    }
}
