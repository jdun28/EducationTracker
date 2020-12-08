using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SQLite;
using System.IO;
using EducationTracker.Classes;

using Xamarin.Forms;

namespace EducationTracker
{
    public partial class AddEditTerm : ContentPage
    {
        string start;
        string end;
        DateTime d_start;
        DateTime d_end;
        string termName;

        public AddEditTerm()
        {
            InitializeComponent();

        }
        public AddEditTerm(Term term)
        {
            InitializeComponent();
            Universals.CurrentTerm = term;

            termNameEntry.Text = term.TermName;
            termStart.Date = Convert.ToDateTime(term.TermStart);
            termEnd.Date = Convert.ToDateTime(term.TermEnd);
        }

        void saveTermButton_Clicked(System.Object sender, System.EventArgs e)
        {
            if (Universals.CurrentTerm != null)
            {
                if (d_start > d_end)
                {
                    DisplayAlert("Alert", "Cannot save term. Start date must be before end date. Please try again.", "OK");
                }
                else
                {
                    Universals.CurrentTerm.TermName = termName;
                    Universals.CurrentTerm.TermStart = d_start;
                    Universals.CurrentTerm.TermEnd = d_end;
                    try
                    {
                        using (SQLiteConnection db = new SQLiteConnection(App.FilePath))
                        {
                            db.CreateTable<Term>();
                            db.Update(Universals.CurrentTerm);
                            DisplayAlert("Alert", "Term information has been updated.", "Continue");
                            Navigation.PushAsync(new MainPage());
                        }
                    }
                    catch (Exception)
                    {
                        DisplayAlert("Error", "An error has occurred. Please try again.", "OK");
                    }
                }
            }
            else
            {
                if (d_start > d_end)
                {
                    DisplayAlert("Alert", "Cannot save term. Start date must be before end date. Please try again.", "OK");
                }
                else
                {
                    try
                    {
                        Term newTerm = new Term()
                        {
                            TermName = termNameEntry.Text,
                            TermStart = d_start,
                            TermEnd = d_end

                        };
                        using (SQLiteConnection db = new SQLiteConnection(App.FilePath))
                        {
                            db.CreateTable<Term>();
                            db.Insert(newTerm);
                        }
                        DisplayAlert("Alert", "Term added successfully.", "Continue");
                        Navigation.PushAsync(new MainPage());
                    }
                    catch (Exception)
                    {
                        DisplayAlert("Error", "An error has occurred. Please try again.", "OK");
                    }
                }
            }
        }

        void cancelTermSaveButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }

        void termEnd_DateSelected(System.Object sender, Xamarin.Forms.DateChangedEventArgs e)
        {
            end = e.NewDate.ToString();
            d_end = Convert.ToDateTime(end);
        }

        void termStart_DateSelected(System.Object sender, Xamarin.Forms.DateChangedEventArgs e)
        {
            start = e.NewDate.ToString();
            d_start = Convert.ToDateTime(start);
        }

        void termNameEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            termName = termNameEntry.Text;
        }
    }
}