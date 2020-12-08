using System;
using System.Collections.Generic;
using EducationTracker.Classes;
using SQLite;
using Xamarin.Forms;

namespace EducationTracker
{
    public partial class AssessmentDetailPage : ContentPage
    {
        Universals universals = new Universals();
        int courseID;

        public AssessmentDetailPage()
        {
            InitializeComponent();
        }
        public AssessmentDetailPage(Assessment assessment)
        {
            InitializeComponent();
            Universals.CurrentAssessment = assessment;
            courseID = assessment.CourseID;
            assessmentNameEntry.Text = assessment.AssessmentName;
            assessmentTypeText.Text = assessment.AssessmentType;
            assessmentStart.Text = assessment.AssessmentStart.ToString();
            assessmentEnd.Text = assessment.AssessmentEnd.ToString();

        }

        void EditAssessment_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new AddEditAssessment(Universals.CurrentAssessment));
        }

        void DeleteAssessment_Clicked(System.Object sender, System.EventArgs e)
        {
            using (SQLiteConnection db = new SQLiteConnection(App.FilePath))
            {
                db.CreateTable<Assessment>();
                db.Delete(Universals.CurrentAssessment);
                DisplayAlert("Alert", "Your assessment has been deleted.", "Continue");
                Universals.CurrentCourse = universals.GetCourse(courseID);
                Navigation.PushAsync(new AssessmentDetail(Universals.CurrentCourse));
            }
        }
    }
}
