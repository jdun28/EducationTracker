using System;
using System.Collections.Generic;
using EducationTracker.Classes;

using Xamarin.Forms;

namespace EducationTracker
{
    public partial class CourseDetailPage : ContentPage
    {
        public CourseDetailPage(Course course)
        {
            InitializeComponent();
            Universals.CurrentCourse = course;

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
    }
}
