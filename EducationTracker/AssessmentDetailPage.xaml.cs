using System;
using System.Collections.Generic;
using EducationTracker.Classes;
using SQLite;
using Xamarin.Forms;
using System.Linq;
using Plugin.LocalNotifications;

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
            EnableNotifications.IsToggled = assessment.Notification;

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
                Navigation.PushAsync(new AssessmentListPage(Universals.CurrentCourse));
            }
        }

        void EnableNotifications_Toggled(System.Object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            if (e.Value)
            {
                using (SQLiteConnection db = new SQLiteConnection(App.FilePath))
                {
                    db.CreateTable<Assessment>();
                    List<Assessment> assessmentList = db.Query<Assessment>("SELECT * FROM Assessment;").ToList();
                    
                    db.Execute("UPDATE Assessment SET Notification = true WHERE AssessmentID = " + Universals.CurrentAssessment.AssessmentID + ";");
                }
            }
            if (!e.Value)
            {
                using (SQLiteConnection db = new SQLiteConnection(App.FilePath))
                {
                    db.CreateTable<Assessment>();
                    List<Assessment> assessmentList = db.Query<Assessment>("SELECT * FROM Assessment;").ToList();
                    db.Execute("UPDATE Assessment SET Notification = false WHERE AssessmentID = " + Universals.CurrentAssessment.AssessmentID + ";");
                    CrossLocalNotifications.Current.Cancel(103);
                    CrossLocalNotifications.Current.Cancel(104);
                }
            }

        }
    }
}
