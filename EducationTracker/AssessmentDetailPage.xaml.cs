using System;
using System.Collections.Generic;
using EducationTracker.Classes;
using SQLite;
using Xamarin.Forms;

namespace EducationTracker
{
    public partial class AssessmentDetailPage : ContentPage
    {
        public AssessmentDetailPage()
        {
            InitializeComponent();
        }
        public AssessmentDetailPage(Course course)
        {
            InitializeComponent();
            Universals.CurrentCourse = course;
        }
    }
}
