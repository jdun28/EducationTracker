using System;
using System.Collections.Generic;
using EducationTracker.Classes;
using SQLite;
using System.Linq;
using Xamarin.Forms;

namespace EducationTracker
{
    public partial class AddEditAssessment : ContentPage
    {
        int courseId;
        string aName;
        string aType;
        string aStart;
        string aEnd;
        DateTime Start;
        DateTime End;
        Universals universals = new Universals();

        public AddEditAssessment()
        {
            InitializeComponent();
            Start = DateTime.Today;
            End = DateTime.Today;

        }
        public AddEditAssessment(Assessment assessment)
        {

            InitializeComponent();
            Universals.CurrentAssessment = assessment;
            courseId = assessment.CourseID;
            assessmentNameEntry.Text = assessment.AssessmentName;
            assessmentPicker.SelectedItem = assessment.AssessmentType;
            assessmentStartDatePicker.Date = Convert.ToDateTime(assessment.AssessmentStart);
            assessmentEndDatePicker.Date = Convert.ToDateTime(assessment.AssessmentEnd);
        }

        bool SaveAllowed()
        {
            if(!universals.IsNotNullOrEmpty(assessmentNameEntry.Text))
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        void saveAssessmentButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Universals.CurrentCourse = universals.GetCourse(courseId);
            if(Universals.CurrentAssessment != null)
            {
                Universals.CurrentAssessment.CourseID = courseId;
                Universals.CurrentAssessment.AssessmentName = aName;
                Universals.CurrentAssessment.AssessmentType = aType;
                Universals.CurrentAssessment.AssessmentStart = Start;
                Universals.CurrentAssessment.AssessmentEnd = End;

                try
                {
                    using (SQLiteConnection db = new SQLiteConnection(App.FilePath))
                    {
                        db.CreateTable<Assessment>();
                        db.Update(Universals.CurrentAssessment);
                        DisplayAlert("Alert", "Your assessment has been updated.", "Continue");
                        Navigation.PushAsync(new AssessmentDetail(Universals.CurrentCourse));
                    }
                }
                catch (Exception)
                {
                    DisplayAlert("Error", "An error occurred. Please try again.", "Continue");
                }
            }
            else
            {
                Assessment newAssessment = new Assessment()
                {
                    CourseID = courseId,
                    AssessmentName = aName,
                    AssessmentType = aType,
                    AssessmentStart = Start,
                    AssessmentEnd = End
                };

                try
                {
                    using (SQLiteConnection db = new SQLiteConnection(App.FilePath))
                    {
                        db.CreateTable<Assessment>();
                        db.Insert(newAssessment);
                        DisplayAlert("Alert", "Your assessment has been added", "Continue");
                        Navigation.PushAsync(new AssessmentDetail(Universals.CurrentCourse));
                    }
                }
                catch (Exception)
                {
                    DisplayAlert("Error", "An error has occurred. Please try again.", "Continue");
                }
            }
        }

        void cancelAssessmentSaveButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new AssessmentDetail(Universals.CurrentCourse));
        }

        void assessmentEndDatePicker_DateSelected(System.Object sender, Xamarin.Forms.DateChangedEventArgs e)
        {
            aEnd = e.NewDate.ToString();
            End = Convert.ToDateTime(aEnd);
        }

        void assessmentStartDatePicker_DateSelected(System.Object sender, Xamarin.Forms.DateChangedEventArgs e)
        {
            aStart = e.NewDate.ToString();
            Start = Convert.ToDateTime(aStart);
        }
        void assessmentNameEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            aName = assessmentNameEntry.Text;
            saveAssessmentButton.IsEnabled = SaveAllowed();
        }

        void assessmentPicker_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            aType = assessmentPicker.SelectedItem.ToString();
        }
    }
}
