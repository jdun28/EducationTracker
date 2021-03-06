﻿using System;
using System.Collections.Generic;
using EducationTracker.Classes;
using System.Linq;
using SQLite;
using Plugin.LocalNotifications;

using Xamarin.Forms;
using Xamarin.Essentials;

namespace EducationTracker
{
    public partial class CourseDetailPage : ContentPage
    {
        int termId;
        int courseId;
        Universals universals = new Universals();
        public CourseDetailPage(Course course)
        {
            InitializeComponent();
            
            Universals.CurrentCourse = course;
            termId = course.TermID;
            courseId = course.CourseID;
            courseNameDetail.Text = course.CourseName;
            statusDetail.Text = course.CourseStatus;
            startDateDetail.Text = course.CourseStart.ToString();
            endDateDetail.Text = course.CourseEnd.ToString();
            instructorNameDetail.Text = course.InstructorName;
            emailDetail.Text = course.InstructorEmail;
            phoneDetail.Text = course.InstructorPhone;
            EnableNotifications.IsToggled = course.Notification;


            if((course.Notes != null) && (course.Notes.Length > 0))
            {
                notesDetail.Text = course.Notes;
                ShareNotes.IsVisible = true;
            }
            else
            {
                notesTitle.IsVisible = false;
                notesDetail.IsVisible = false;
                ShareNotes.IsVisible = false;
            }

        }

        void ViewAssessments_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new AssessmentListPage(Universals.CurrentCourse));
        }

        void editCourseButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new AddEditCourse(Universals.CurrentCourse));
        }
        void DeleteCourseButton_Clicked(System.Object sender, System.EventArgs e)
        {
            using (SQLiteConnection db = new SQLiteConnection(App.FilePath))
            {
                db.CreateTable<Course>();
                db.Delete(Universals.CurrentCourse);
                DisplayAlert("Alert", "Course has been deleted successfully.", "Continue");
                Universals.CurrentTerm = universals.GetTerm(termId);
                Navigation.PushAsync(new CourseListPage(Universals.CurrentTerm));
            }
        }
        void ShareNotes_Clicked(System.Object sender, System.EventArgs e)
        {
            Share.RequestAsync(Universals.CurrentCourse.Notes.ToString());
        }

        void EnableNotifications_Toggled(System.Object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            if (e.Value)
            {
                using (SQLiteConnection db = new SQLiteConnection(App.FilePath))
                {
                    db.CreateTable<Course>();
                    List<Course> courseList = db.Query<Course>("SELECT * FROM Course;").ToList();
                    db.Execute("UPDATE Course SET Notification = true WHERE CourseID = " + Universals.CurrentCourse.CourseID + ";");
                }
            }
            if(!e.Value)
            {
                using (SQLiteConnection db = new SQLiteConnection(App.FilePath))
                {
                    db.CreateTable<Course>();
                    List<Course> courseList = db.Query<Course>("SELECT * FROM Course;").ToList();
                    db.Execute("UPDATE Course SET Notification = false WHERE CourseID = " + Universals.CurrentCourse.CourseID + ";");
                    CrossLocalNotifications.Current.Cancel(101);
                    CrossLocalNotifications.Current.Cancel(102);
                }
            }
        }
    }
}
