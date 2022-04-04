using System;
using System.Collections.Generic;
using System.Text;

namespace ReportGenerator.DataBase.Models
{
    public class WorkCalendar
    {
        public int Id { get; set; }
        public int WeekNumber { get; set; }
        public DateTime StartWeek { get; set; }
        public DateTime EndWeek { get; set; }
        public int WorkDays { get; set; }
        public int Year { get; set; }

        public WorkCalendar()
        {
        }

        public WorkCalendar(int id, int weekNumber, DateTime startWeek, DateTime endWeek, int workDays, int year)
        {
            Id = id;
            WeekNumber = weekNumber;
            StartWeek = startWeek;
            EndWeek = endWeek;
            WorkDays = workDays;
            Year = year;
        }
    }
}
