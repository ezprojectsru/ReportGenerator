using System;


namespace ReportGenerator.DataBase.Models
{
    /// <summary>
    /// Класс модели Отчета
    /// </summary>
    public class Report
    {
        public int id { get; set; }
        public DateTime createDate { get; set; }
        public int planId { get; set; }
        public int actualIntensity { get; set; }
        public int actualCompletion { get; set; }
    }
}
