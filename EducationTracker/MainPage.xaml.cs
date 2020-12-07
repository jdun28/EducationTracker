using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EducationTracker.Classes;
using SQLite;
using Xamarin.Forms;

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
            }
            OnAppearing();
        }

        void TermsListView_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            Universals.CurrentTerm = TermsListView.SelectedItem as Term;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            using (SQLiteConnection db = new SQLiteConnection(App.FilePath))
            {
                db.CreateTable<Term>();
                var terms = db.Table<Term>().ToList();
                TermsListView.ItemsSource = terms;
            }
            
        }

        void ViewTermButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new CoursesDetail(Universals.CurrentTerm));
            //Navigation.PushAsync(new CoursePage());
        }
    }
}
