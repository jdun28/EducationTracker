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

        public AddEditCourse()
        {
            InitializeComponent();
        }
        public AddEditCourse(Course course)
        {
            InitializeComponent();
            Universals.CurrentCourse = course;

            courseNameEntry.Text = course.CourseName;
            courseStatusPicker.Title = course.CourseStatus.ToString();
            courseStartDatePicker.Date = Convert.ToDateTime(course.CourseStart);
            courseEndDatePicker.Date = Convert.ToDateTime(course.CourseEnd);
            instrutorNameEntry.Text = course.InstructorName;
            instrutorEmailEntry.Text = course.InstructorEmail;
            instrutorPhoneEntry.Text = course.InstructorPhone;
            notesEntry.Text = course.Notes;
        }

        void saveCourseButton_Clicked(System.Object sender, System.EventArgs e)
        {
        }

        void cancelCourseSaveButton_Clicked(System.Object sender, System.EventArgs e)
        {
        }

        void courseNameEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            cName = courseNameEntry.Text;
        }

        void courseStatusPicker_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            var index = courseStatusPicker.SelectedIndex.ToString();
            cStatus = index;
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
        }

        void instrutorEmailEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            iEmail = instrutorEmailEntry.Text;
        }

        void instrutorPhoneEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            iPhone = instrutorPhoneEntry.Text;
        }

        void notesEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            cNotes = notesEntry.Text;
        }
    }
}
