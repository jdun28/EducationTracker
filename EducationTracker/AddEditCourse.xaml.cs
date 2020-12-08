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

        public AddEditCourse(Term term)
        {
            InitializeComponent();
            Universals.CurrentTerm = term;
            start = DateTime.Today;
            end = DateTime.Today;

        }
        public AddEditCourse(Course course)
        {
            InitializeComponent();
            Universals.CurrentCourse = course;
            termId = course.TermID;
            courseId = course.CourseID;
            courseNameEntry.Text = course.CourseName;
            courseStatusPicker.Title = course.CourseStatus.ToString();
            courseStartDatePicker.Date = Convert.ToDateTime(course.CourseStart);
            courseEndDatePicker.Date = Convert.ToDateTime(course.CourseEnd);
            instrutorNameEntry.Text = course.InstructorName;
            instrutorEmailEntry.Text = course.InstructorEmail;
            instrutorPhoneEntry.Text = course.InstructorPhone;
            notesEntry.Text = course.Notes;
        }

        bool SaveAllowed()
        {
            if(!universals.IsNotNullOrEmpty(courseNameEntry.Text))
            {
                return false;
            }
            if(!universals.IsNotNullOrEmpty(instrutorNameEntry.Text))
            {
                return false;
            }
            if(!universals.IsNotNullOrEmpty(instrutorEmailEntry.Text))
            {
                return false;
            }
            if(universals.IsNotNullOrEmpty(instrutorPhoneEntry.Text))
            {
                return false;
            }
            if(!universals.CheckPhoneFormat(instrutorPhoneEntry.Text))
            {
                return false;
            }
            if(!universals.CheckEmailFormat(instrutorEmailEntry.Text))
            {
                return false;
            }
            return true;
        }

        void saveCourseButton_Clicked(System.Object sender, System.EventArgs e)
        {
            
            if (Universals.CurrentCourse != null)
            {
                if (start > end)
                {
                    DisplayAlert("Alert", "Cannot save course. Start date must be before end end date. Please try again.", "Continue");
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
                    try
                    {
                        using (SQLiteConnection db = new SQLiteConnection(App.FilePath))
                        {
                            db.CreateTable<Course>();
                            db.Update(Universals.CurrentCourse);
                            DisplayAlert("Alert", "Course Information has been updated.", "Continue");
                            Navigation.PushAsync(new CoursesDetail(Universals.CurrentTerm));
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
                            Notes = cNotes
                        };
                        using (SQLiteConnection db = new SQLiteConnection(App.FilePath))
                        {
                            db.CreateTable<Course>();
                            db.Insert(newCourse);
                        }
                        DisplayAlert("Alert", "Course added successfully.", "Continue");
                        Navigation.PushAsync(new CoursesDetail(Universals.CurrentTerm));
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
            Navigation.PushAsync(new CoursesDetail(Universals.CurrentTerm));
        }

        void courseNameEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            cName = courseNameEntry.Text;
            saveCourseButton.IsEnabled = SaveAllowed();
        }

        void courseStatusPicker_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {

            cStatus = courseStatusPicker.SelectedItem.ToString();
            
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
