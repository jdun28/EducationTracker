using System;
using System.Collections.Generic;
using EducationTracker.Classes;
using SQLite;
using Xamarin.Forms;
using System.Linq;

namespace EducationTracker
{
    public partial class AssessmentListPage : ContentPage
    {
        int courseId;
        Universals universals = new Universals();
        public AssessmentListPage(Course course)
        {
            InitializeComponent();
            Universals.CurrentCourse = universals.GetCourse(course.CourseID);
            courseId = course.CourseID;
        }

        void AssessmentLV_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            Universals.CurrentAssessment = AssessmentLV.SelectedItem as Assessment;
            Navigation.PushAsync(new AssessmentDetailPage(Universals.CurrentAssessment));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            using (SQLiteConnection db = new SQLiteConnection(App.FilePath))
            {
                db.CreateTable<Assessment>();
                var assessments = db.Query<Assessment>("SELECT * FROM Assessment WHERE CourseID = '" + courseId + "';");
                AssessmentLV.ItemsSource = assessments;
            }
        }

        void AddAssessment_Clicked(System.Object sender, System.EventArgs e)
        {
            using (SQLiteConnection db = new SQLiteConnection(App.FilePath))
            {
                db.CreateTable<Assessment>();
                var schedAssess = db.Query<Assessment>("SELECT * FROM Assessment WHERE CourseID = " + courseId + ";");
                if (schedAssess.Count < 2)
                {
                    Navigation.PushAsync(new AddEditAssessment(Universals.CurrentCourse));
                }
                else
                {
                    DisplayAlert("Alert", "Cannot schedule more than two assessments per course.", "Continue");
                }
            }
        }
    }
}
