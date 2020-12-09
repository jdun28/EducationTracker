using System;
using Xamarin.Forms;
using EducationTracker.Classes;
using Xamarin.Forms.Xaml;
using SQLite;
using System.Linq;

namespace EducationTracker
{
    public partial class App : Application
    {
        public static string FilePath;
        Universals universals = new Universals();

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        public App(string filePath)
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
            FilePath = filePath;
        }

        protected override void OnStart()
        {
            using (SQLiteConnection db = new SQLiteConnection(App.FilePath))
            {
                db.CreateTable<Term>();
                var tCount = db.Query<Term>("SELECT * FROM Term");
                if (tCount.Count == 0)
                {
                    universals.PopulateTerm();
                }
                db.CreateTable<Course>();
                var cCount = db.Query<Course>("SELECT * FROM Course");
                if(cCount.Count == 0)
                {
                    universals.PopulateCourses();
                }
                db.CreateTable<Assessment>();
                var aCount = db.Query<Assessment>("SELECT * FROM ASSESSMENT");
                if(aCount.Count == 0)
                {
                    universals.PopulateAssessment();
                }
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
