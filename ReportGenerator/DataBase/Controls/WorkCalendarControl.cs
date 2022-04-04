using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using Dapper;
using ReportGenerator.DataBase.Models;
using ReportGenerator.Services;

namespace ReportGenerator.DataBase.Controls
{
    public class WorkCalendarControl
    {
        private readonly DbConnection _db = new DbConnection();

        public WorkCalendar GetCurrentWeek(DateTime currentDay)
        {
            WorkCalendar week = new WorkCalendar();
            DateTime dayWithWeekends = currentDay.AddDays(-3);
            try
            {
                SqlConnection connection = _db.GetConnection();
                week = connection
                    .Query<WorkCalendar>("SELECT * FROM workCalendar WHERE startWeek <= @currentDay and endWeek >= @dayWithWeekends", new { currentDay, dayWithWeekends }).FirstOrDefault();
                
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return week;
        }

        public List<WorkCalendar> GetAllWeeksInYear(int year)
        {
            List<WorkCalendar> weeks = new List<WorkCalendar>();
            
            try
            {
                SqlConnection connection = _db.GetConnection();
                weeks = connection
                    .Query<WorkCalendar>("SELECT * FROM workCalendar WHERE year = @year", new { year }).ToList();

                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return weeks;
        }

        


    }
}
