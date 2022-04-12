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
        public int projectId { get; set; }
        public int responsibleId { get; set; }
        public int directorId { get; set; }
        public string comment { get; set; }

        public Plan()
            {
            }

        public Plan(int id, string name, DateTime startDate, DateTime finishDate, int projectId, int responsibleId, int directorId, string comment)
        {
            this.id = id;
            this.name = name;
            this.startDate = startDate;
            this.finishDate = finishDate;
            this.projectId = projectId;
            this.responsibleId = responsibleId;
            this.directorId = directorId;
            this.comment = comment;
        }

        public Plan(Plan plan)
        {
            this.id = plan.id;
            this.name = plan.name;
            this.startDate = plan.startDate;
            this.finishDate = plan.finishDate;
            this.projectId = plan.projectId;
            this.responsibleId = plan.responsibleId;
            this.directorId = plan.directorId;
            this.comment = plan.comment;
        }

    }

}
