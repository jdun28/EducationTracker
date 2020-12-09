using System;
using System.Collections.Generic;
using EducationTracker.Classes;
using SQLite;
using System.Linq;
using Xamarin.Forms;

namespace EducationTracker
{
    public partial class TermDetail : ContentPage
    {
        int termId;
        public TermDetail(Term term)
        {
            InitializeComponent();

            Universals.CurrentTerm = term;
            termId = term.TermID;

            termNameEntry.Text = term.TermName;
            termStartDetail.Text = term.TermStart.ToString();
            termEndDetail.Text = term.TermEnd.ToString();
        }

        void ViewCourses_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new CourseListPage(Universals.CurrentTerm));
        }

        void EditTerm_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new AddEditTerm(Universals.CurrentTerm));
        }

        void DeleteTerm_Clicked(System.Object sender, System.EventArgs e)
        {
            using(SQLiteConnection db = new SQLiteConnection(App.FilePath))
            {
                db.CreateTable<Term>();
                db.Delete(Universals.CurrentTerm);
                db.CreateTable<Course>();
                var coursesToDelete = db.Query<Course>("SELECT * FROM Course WHERE TermID = '" + termId + "';");
                db.Delete(coursesToDelete);
                Navigation.PushAsync(new MainPage());
            }
        }
    }
}
