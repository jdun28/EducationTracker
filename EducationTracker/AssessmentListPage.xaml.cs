using System;
using System.Collections.Generic;
using EducationTracker.Classes;
using SQLite;
using Xamarin.Forms;

namespace EducationTracker
{
    public partial class AssessmentListPage : ContentPage
    {
        int courseId;
        public AssessmentListPage(Course course)
        {
            InitializeComponent();
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
            Navigation.PushAsync(new AddEditAssessment());
        }
    }
}
