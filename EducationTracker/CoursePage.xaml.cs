using System;
using EducationTracker.Classes;
using SQLite;
using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;

namespace EducationTracker
{
    public partial class CoursePage : ContentPage
    {
        int termID;
        Universals universals = new Universals();
        Course newCourse = new Course();

        public CoursePage()
        {
            InitializeComponent();
        }
        public CoursePage(Term SelectedTerm)
        {
            termID = SelectedTerm.TermID;
            
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection db = new SQLiteConnection(App.FilePath))
            {
                db.CreateTable<Course>();
                List<Course> courses = db.Table<Course>().ToList();
                CoursesLV.ItemsSource = courses;
            }
        }

        void AddCourseButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new AddEditCourse());
        }

        void EditCourseButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new AddEditCourse(Universals.CurrentCourse));
        }

        void DeleteCourseButton_Clicked(System.Object sender, System.EventArgs e)
        {
            using (SQLiteConnection db = new SQLiteConnection(App.FilePath))
            {
                db.CreateTable<Course>();
                int toDelete = db.Delete(Universals.CurrentCourse);
                DisplayAlert("Alert", "Course has been deleted successfully.", "Continue");
            }
        }
        void viewCourseButton_Clicked(System.Object sender, System.EventArgs e)
        {

        }

        void CoursesLV_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            Universals.CurrentCourse = CoursesLV.SelectedItem as Course;
        }
    }
}
