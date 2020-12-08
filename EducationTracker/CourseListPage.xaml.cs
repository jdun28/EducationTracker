using System;
using System.Collections.Generic;
using EducationTracker.Classes;
using SQLite;
using System.Linq;
using Xamarin.Forms;


namespace EducationTracker
{
    public partial class CourseListPage : ContentPage
    {
        int termID;
        Universals universals = new Universals();
        Course newCourse = new Course();

        public CourseListPage()
        {
            InitializeComponent();
        }

        public CourseListPage(Term SelectedTerm)
        {
            InitializeComponent();
            Universals.CurrentTerm = SelectedTerm;
            termID = SelectedTerm.TermID;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection db = new SQLiteConnection(App.FilePath))
            {
                db.CreateTable<Course>();
                var courses = db.Query<Course>("SELECT * FROM Course WHERE TermID = '" + termID + "';");
                CoursesLV.ItemsSource = courses;
            }
        }

        void AddCourseButton_Clicked(System.Object sender, System.EventArgs e)
        {
            using (SQLiteConnection db = new SQLiteConnection(App.FilePath))
            {
                db.CreateTable<Course>();
                var courseInTerm = db.Query<Course>("SELECT * FROM Course WHERE TermID= '" + termID + "';").ToList();

                if (courseInTerm.Count < 6)
                {
                    Navigation.PushAsync(new AddEditCourse(Universals.CurrentTerm));
                }
                else
                {
                    DisplayAlert("Alert", "Cannot add more than six classes per term.", "Continue");
                }
            }
        }
        void CoursesLV_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            Universals.CurrentCourse = CoursesLV.SelectedItem as Course;
            Navigation.PushAsync(new CourseDetailPage(Universals.CurrentCourse));
        }

    }
}
