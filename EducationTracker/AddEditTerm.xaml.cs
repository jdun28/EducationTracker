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
        DateTime t_start;
        DateTime t_end;
        string termName;
        Universals universals = new Universals();

        public AddEditTerm()
        {
            InitializeComponent();
            t_start = DateTime.Today;
            t_end = DateTime.Today;
            saveTermButton.IsEnabled = false;

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
            bool startAfterEnd = t_start > t_end;
            if (startAfterEnd)
            {
                DisplayAlert("Alert", "Cannot save term. Start date must be before end date. Please try again.", "OK");
                return;
            }
            if (Universals.CurrentTerm != null)
                {
                    if (termStart.Date == Universals.CurrentTerm.TermStart)
                    {
                        t_start = termStart.Date;
                    }
                    else
                    {
                        t_start = Convert.ToDateTime(start);
                    }
                    if (termEnd.Date == Universals.CurrentTerm.TermEnd)
                    {
                        t_end = termEnd.Date;
                    }
                    else
                    {
                        t_end = Convert.ToDateTime(end);
                    }

                        Universals.CurrentTerm.TermName = termName;
                        Universals.CurrentTerm.TermStart = t_start;
                        Universals.CurrentTerm.TermEnd = t_end;
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
                else
                {
                    if (t_start > t_end)
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
                                TermStart = t_start,
                                TermEnd = t_end

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
            t_end = Convert.ToDateTime(end);
        }

        void termStart_DateSelected(System.Object sender, Xamarin.Forms.DateChangedEventArgs e)
        {
            start = e.NewDate.ToString();
            t_start = Convert.ToDateTime(start);
        }

        void termNameEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            termName = termNameEntry.Text;
            saveTermButton.IsEnabled = true;
        }
    }
}