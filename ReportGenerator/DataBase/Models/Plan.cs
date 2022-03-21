using System;


namespace ReportGenerator.DataBase.Models
{
    /// <summary>
    /// Класс модели Плана
    /// </summary>
    public class Plan
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime startDate { get; set; }
        public DateTime finishDate { get; set; }
        public int responsibleId { get; set; }
        public int directorId { get; set; }
        public string comment { get; set; }

        public Plan()
            {
            }

        public Plan(int id, string name, DateTime startDate, DateTime finishDate, int responsibleId, int directorId, string comment)
        {
            this.id = id;
            this.name = name;
            this.startDate = startDate;
            this.finishDate = finishDate;
            this.responsibleId = responsibleId;
            this.directorId = directorId;
            this.comment = comment;
        }

    }

}
