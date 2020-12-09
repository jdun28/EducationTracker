using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EducationTracker.Classes;
using SQLite;
using Xamarin.Forms;
using Plugin.LocalNotifications;

namespace EducationTracker
{
    public partial class MainPage : ContentPage
    {
        Universals universals = new Universals();
        public MainPage()
        {
            InitializeComponent();
        }

        void AddTermButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new AddEditTerm());
        }

        void EditTermButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new AddEditTerm(Universals.CurrentTerm));
        }

        void DeleteTermButton_Clicked(System.Object sender, System.EventArgs e)
        {
            using (SQLiteConnection db = new SQLiteConnection(App.FilePath))
            {
                db.CreateTable<Term>();

                int toDelete = db.Delete(Universals.CurrentTerm);
                DisplayAlert("Alert", "Term has been deleted.", "Continue");
                Navigation.PushAsync(new MainPage());
            }
            
        }

        void TermsListView_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            Universals.CurrentTerm = TermsListView.SelectedItem as Term;
            Navigation.PushAsync(new TermDetail(Universals.CurrentTerm));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection db = new SQLiteConnection(App.FilePath))
            {
                db.CreateTable<Term>();
                var terms = db.Table<Term>().ToList();
                TermsListView.ItemsSource = terms;

                db.CreateTable<Course>();
                List<Course> cAlertCheck = db.Query<Course>("SELECT * FROM Course WHERE Notification = true;");

                for (int i =0; i < cAlertCheck.Count; i++)
                {
                    if (cAlertCheck[i].CourseStart == DateTime.Today)
                    {
                        CrossLocalNotifications.Current.Show("Course Notification", "Course " + cAlertCheck[i].CourseName.ToString() + " starts today. Good luck!", 100);
                    }
                    if (cAlertCheck[i].CourseEnd == DateTime.Today)
                    {
                        CrossLocalNotifications.Current.Show("Course Notification", "Course " + cAlertCheck[i].CourseName.ToString() + " ends today. Congrats!", 101);
                    }
                }

                db.CreateTable<Assessment>();
                List<Assessment> aAlertCheck = db.Query<Assessment>("SELECT * FROM Assessment WHERE Notification = true;");

                for (int i =0; i < aAlertCheck.Count; i++)
                {
                    if(aAlertCheck[i].AssessmentStart == DateTime.Today)
                    {
                        CrossLocalNotifications.Current.Show("Assessment Notification", aAlertCheck[i].AssessmentName.ToString() + "is today. Good luck!", 102);
                    }
                    if(aAlertCheck[i].AssessmentEnd == DateTime.Today)
                    {
                        CrossLocalNotifications.Current.Show("Assessment Notification", aAlertCheck[i].AssessmentName.ToString() + "ends today. Congrats!", 103);
                    }
                }
            }
        }        
    }
}
