using System;
using SQLite;
namespace EducationTracker.Classes
{
    public class Term
    {
        [PrimaryKey, AutoIncrement] public int TermID { get; set; }
        public string TermName { get; set; }
        public DateTime TermStart { get; set; }
        public DateTime TermEnd { get; set; }

        public Term()
        {
        }
    }
}
