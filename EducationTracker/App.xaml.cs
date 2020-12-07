using System;
using Xamarin.Forms;
using EducationTracker.Classes;
using Xamarin.Forms.Xaml;

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
            universals.PopulateTerm();
            universals.PopulateCourses();
            universals.PopulateAssessment();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
