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
        bool assessmentNotification;
        Universals universals = new Universals();

        public AddEditAssessment(Course course)
        {
            InitializeComponent();
            Start = DateTime.Today;
            End = DateTime.Today;
            Universals.CurrentCourse = universals.GetCourse(course.CourseID);
            courseId = course.CourseID;
            saveAssessmentButton.IsEnabled = false;
            Universals.CurrentAssessment = null;

        }
        public AddEditAssessment(Assessment assessment)
        {
            InitializeComponent();
            Universals.CurrentAssessment = assessment;
            courseId = assessment.CourseID;
            Universals.CurrentCourse = universals.GetCourse(courseId);
            assessmentNameEntry.Text = assessment.AssessmentName;
            assessmentTypePicker.SelectedItem = assessment.AssessmentType.ToString();
            assessmentStartDatePicker.Date = Convert.ToDateTime(assessment.AssessmentStart);
            assessmentEndDatePicker.Date = Convert.ToDateTime(assessment.AssessmentEnd);
            assessmentNotification = assessment.Notification;
            aType = Universals.CurrentAssessment.AssessmentType;
        }

        bool SaveAllowed()
        {
            if(string.IsNullOrEmpty(assessmentNameEntry.Text))
            {
                return false;
            }
            if(string.IsNullOrEmpty(aType))
            {
                return false;
            }
            return true;

        }

        void saveAssessmentButton_Clicked(System.Object sender, System.EventArgs e)
        {
            bool startAfterEnd = Start > End;
            if (startAfterEnd)
            {
                DisplayAlert("Alert", "Cannot save course. Start date must be before end end date. Please try again.", "Continue");
                return;
            }
            if (Universals.CurrentAssessment != null)
            {
                if (assessmentStartDatePicker.Date == Universals.CurrentAssessment.AssessmentStart)
                {
                    Start = Universals.CurrentAssessment.AssessmentStart;
                }
                if (assessmentEndDatePicker.Date == Universals.CurrentAssessment.AssessmentEnd)
                {
                    End = Universals.CurrentAssessment.AssessmentEnd;
                }
                if ((assessmentTypePicker.Items[assessmentTypePicker.SelectedIndex]).ToString() == Universals.CurrentAssessment.AssessmentType)
                {
                    aType = Universals.CurrentAssessment.AssessmentType;
                }
                Universals.CurrentAssessment.CourseID = courseId;
                Universals.CurrentAssessment.AssessmentName = aName;
                Universals.CurrentAssessment.AssessmentType = aType;
                Universals.CurrentAssessment.AssessmentStart = Start;
                Universals.CurrentAssessment.AssessmentEnd = End;
                Universals.CurrentAssessment.Notification = assessmentNotification;

                try
                {
                    using (SQLiteConnection db = new SQLiteConnection(App.FilePath))
                    {
                        db.CreateTable<Assessment>();
                        db.Update(Universals.CurrentAssessment);
                    }
                        DisplayAlert("Alert", "Your assessment has been updated.", "Continue");
                        Navigation.PushAsync(new AssessmentListPage(Universals.CurrentCourse));
                    
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
                    AssessmentEnd = End,
                    Notification = false
                };

                using (SQLiteConnection db = new SQLiteConnection(App.FilePath))
                {
                    db.CreateTable<Assessment>();
                    var types = db.Query<Assessment>("SELECT * FROM Assessment WHERE AssessmentType = '" + aType + "' AND CourseID = " + courseId +";").ToList();
                    if (types.Count > 0)
                    {
                        DisplayAlert("Alert", aType + " assessment for this course already exists. Limit 1 per course.", "Continue");
                        return;
                    }
                }
                try
                {
                    using (SQLiteConnection db = new SQLiteConnection(App.FilePath))
                    {
                        db.CreateTable<Assessment>();
                        db.Insert(newAssessment);
                        var insertedAssessment = db.Query<Assessment>("SELECT * FROM Assessment;").ToList();
                    }
                        DisplayAlert("Alert", "Your assessment has been added", "Continue");
                        Navigation.PushAsync(new AssessmentListPage(Universals.CurrentCourse));
                }
                catch (Exception)
                {
                    DisplayAlert("Error", "An error has occurred. Please try again.", "Continue");
                }
            }
        }

        void cancelAssessmentSaveButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new AssessmentListPage(Universals.CurrentCourse));
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

        void assessmentTypePicker_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {

            using (SQLiteConnection db = new SQLiteConnection(App.FilePath))
            {
                db.CreateTable<Assessment>();
                int pCount = (db.Query<Assessment>("SELECT AssessmentID from Assessment WHERE AssessmentType = 'Performance' AND CourseID = '" + Universals.CurrentCourse.CourseID + "' ;")).Count;
                int oCount = (db.Query<Assessment>("SELECT AssessmentID from Assessment WHERE AssessmentType = 'Objective ' AND CourseID = '" + Universals.CurrentCourse.CourseID + "';")).Count;
                


                if (pCount > 0)
                {
                    
                    if(aType is "Objective")
                    {
                        DisplayAlert("Alert", "Performance Assessment already exists for this course.", "Continue");
                        assessmentTypePicker.SelectedItem = aType;
                        return;
                    }

                }
                if (oCount > 0)
                {
                    if (aType is "Performance")
                    {
                        DisplayAlert("Alert", "Objective Assessment already exists for this course.", "Continue");
                        assessmentTypePicker.SelectedItem = aType;
                        return;
                    }
                }
            }

            aType = (assessmentTypePicker.Items[assessmentTypePicker.SelectedIndex]).ToString();
            //Universals.CurrentAssessment.AssessmentType = aType;
            saveAssessmentButton.IsEnabled = SaveAllowed();
        }
    }
}
