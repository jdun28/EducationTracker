using System;
using System.Collections.Generic;
using EducationTracker.Classes;
using SQLite;
using System.Linq;

using Xamarin.Forms;

namespace EducationTracker
{
    public partial class AddEditCourse : ContentPage
    {
        Universals universals = new Universals();
        int termId;
        int courseId;
        string cName;
        string cStatus;
        string cStart;
        string cEnd;
        DateTime start;
        DateTime end;
        string iName;
        string iEmail;
        string iPhone;
        string cNotes;
        bool courseNotification;

        public AddEditCourse(Term term)
        {
            InitializeComponent();
            Universals.CurrentTerm = term;
            Universals.CurrentTerm.TermID = term.TermID;
            start = DateTime.Today;
            end = DateTime.Today;
            saveCourseButton.IsEnabled = false;
            Universals.CurrentCourse = null;

        }
        public AddEditCourse(Course course)
        {
            InitializeComponent();
            Universals.CurrentCourse = course;
            termId = course.TermID;
            courseId = course.CourseID;
            courseNameEntry.Text = course.CourseName;
            courseStatusPicker.SelectedItem = course.CourseStatus.ToString();
            courseStartDatePicker.Date = Convert.ToDateTime(course.CourseStart);
            courseEndDatePicker.Date = Convert.ToDateTime(course.CourseEnd);
            instrutorNameEntry.Text = course.InstructorName;
            instrutorEmailEntry.Text = course.InstructorEmail;
            instrutorPhoneEntry.Text = course.InstructorPhone;
            notesEntry.Text = course.Notes;
            courseNotification = course.Notification;
        }

        bool SaveAllowed()
        {
            if(string.IsNullOrEmpty(courseNameEntry.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(instrutorNameEntry.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(instrutorEmailEntry.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(instrutorPhoneEntry.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(cStatus))
            {
                return false;
            }

            else
            {
                return true;
            }
        }

        void saveCourseButton_Clicked(System.Object sender, System.EventArgs e)
        {
            bool startAfterEnd = start > end;
            if (startAfterEnd)
            {
                DisplayAlert("Alert", "Cannot save course. Start date must be before end end date. Please try again.", "Continue");
                return;
            }
            if (Universals.CurrentCourse != null)
            {
                if (courseStartDatePicker.Date == Universals.CurrentCourse.CourseStart)
                {
                    start = Universals.CurrentCourse.CourseStart;
                }
                if (courseEndDatePicker.Date == Universals.CurrentCourse.CourseEnd)
                {
                    end = Universals.CurrentCourse.CourseEnd;
                }
                if((courseStatusPicker.Items[courseStatusPicker.SelectedIndex]).ToString() == Universals.CurrentCourse.CourseStatus)
                {
                    cStatus = Universals.CurrentCourse.CourseStatus;
                }
                
                else
                {
                    
                    Universals.CurrentTerm = universals.GetTerm(termId);
                    Universals.CurrentCourse.CourseID = courseId;
                    Universals.CurrentCourse.TermID = termId;
                    Universals.CurrentCourse.CourseName = cName;
                    Universals.CurrentCourse.CourseStatus = cStatus;
                    Universals.CurrentCourse.CourseStart = start;
                    Universals.CurrentCourse.CourseEnd = end;
                    Universals.CurrentCourse.InstructorName = iName;
                    Universals.CurrentCourse.InstructorEmail = iEmail;
                    Universals.CurrentCourse.InstructorPhone = iPhone;
                    Universals.CurrentCourse.Notes = cNotes;
                    Universals.CurrentCourse.Notification = courseNotification;

                    try
                    {
                        using (SQLiteConnection db = new SQLiteConnection(App.FilePath))
                        {
                            db.CreateTable<Course>();
                            db.Update(Universals.CurrentCourse);
                            DisplayAlert("Alert", "Course Information has been updated.", "Continue");
                            Navigation.PushAsync(new CourseListPage(Universals.CurrentTerm));
                        }
                    }
                    catch (Exception)
                    {
                        DisplayAlert("Error", "An Error has occurred. Please try again.", "Continue");
                    }
                }
            }
            else
            {
                if(start>end)
                {
                    DisplayAlert("Alert", "Cannot save course. Start date must be before end end date. Please try again.", "Continue");
                }
                else
                {
                    try
                    {
                        Course newCourse = new Course()
                        {
                            TermID = Universals.CurrentTerm.TermID,
                            CourseName = cName,
                            CourseStatus = cStatus,
                            CourseStart = start,
                            CourseEnd = end,
                            InstructorName = iName,
                            InstructorEmail = iEmail,
                            InstructorPhone = iPhone,
                            Notes = cNotes,
                            Notification = false
                        };
                        using (SQLiteConnection db = new SQLiteConnection(App.FilePath))
                        {
                            db.CreateTable<Course>();
                            db.Insert(newCourse);
                        }
                        DisplayAlert("Alert", "Course added successfully.", "Continue");
                        Navigation.PushAsync(new CourseListPage(Universals.CurrentTerm));
                    }
                    catch (Exception)
                    {
                        DisplayAlert("Error", "An Error has occurred. Please try again.", "Continue");
                    }
                }
            }
        }

        void cancelCourseSaveButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new CourseListPage(Universals.CurrentTerm));
        }

        void courseNameEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            cName = courseNameEntry.Text;
            saveCourseButton.IsEnabled = SaveAllowed();
        }

        void courseStatusPicker_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            cStatus = (courseStatusPicker.Items[courseStatusPicker.SelectedIndex]).ToString();
            saveCourseButton.IsEnabled = SaveAllowed();
        }

        void courseStartDatePicker_DateSelected(System.Object sender, Xamarin.Forms.DateChangedEventArgs e)
        {
            cStart = e.NewDate.ToString();
            start = Convert.ToDateTime(cStart);
        }

        void courseEndDatePicker_DateSelected(System.Object sender, Xamarin.Forms.DateChangedEventArgs e)
        {
            cEnd = e.NewDate.ToString();
            end = Convert.ToDateTime(cEnd);
        }

        void instrutorNameEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            iName = instrutorNameEntry.Text;
            saveCourseButton.IsEnabled = SaveAllowed();
        }

        void instrutorEmailEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            iEmail = instrutorEmailEntry.Text;
            saveCourseButton.IsEnabled = SaveAllowed();
        }

        void instrutorPhoneEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            iPhone = instrutorPhoneEntry.Text;
            saveCourseButton.IsEnabled = SaveAllowed();
        }

        void notesEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            cNotes = notesEntry.Text;
        }
    }
}
