using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DatingSite_TermProject.Models
{
    public class DatePlanModel
    {
        private int datePlanID;
        private DateTime date;
        private TimeSpan time;
        private string description;
        private string location;
        private int dateId;
        public DatePlanModel()
        {
        }
        public DatePlanModel(int dpID, DateTime dt, TimeSpan t, string desc, string l, int dID)
        {
            DatePlanID = dpID;
            Date = dt;
            Time = t;
            Description = desc;
            Location = l;
            DateId = dID;
        }

        public int DatePlanID
        {
            get { return datePlanID; }
            set { datePlanID = value; }
        }
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }   
        public TimeSpan Time
        {
            get { return time; }
            set { time = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public string Location
        {
            get { return location; }
            set { location = value; }
        }
        public int DateId
        {
            get { return dateId; }
            set { dateId = value; }
        }
        public string GetFullDateTime()
        {            
            return new DateTime(Date.Year, Date.Month, Date.Day, Time.Hours, Time.Minutes, Time.Seconds).ToString("f");   
        }
    }
}
