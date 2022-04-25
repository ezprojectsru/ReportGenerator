using System;
using System.Collections.Generic;
using System.Text;

namespace ReportGenerator.Data.Models
{
    public class Plan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public int ProjectId { get; set; }
        public int ResponsibleId { get; set; }
        public int DirectorId { get; set; }
        public string Comment { get; set; }

        public Plan()
        {
        }

        public Plan(int id, string name, DateTime startDate, DateTime finishDate, int projectId, int responsibleId, int directorId, string comment)
        {
            Id = id;
            Name = name;
            StartDate = startDate;
            FinishDate = finishDate;
            ProjectId = projectId;
            ResponsibleId = responsibleId;
            DirectorId = directorId;
            Comment = comment;
        }

        public Plan(Plan plan)
        {
            Id = plan.Id;
            Name = plan.Name;
            StartDate = plan.StartDate;
            FinishDate = plan.FinishDate;
            ProjectId = plan.ProjectId;
            ResponsibleId = plan.ResponsibleId;
            DirectorId = plan.DirectorId;
            Comment = plan.Comment;
        }
    }
}
