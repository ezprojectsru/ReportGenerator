using System;
using System.Collections.Generic;
using System.Text;

namespace ReportGenerator.Data.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ServicesId { get; set; }
        public int ProjectStatusId { get; set; }
        public double StatusPercent { get; set; }
        public string Description { get; set; }
        public List<Plan> Plans { get; set; }


        public Project()
        {
        }

        public Project(Project project)
        {
            Id = project.Id;
            Name = project.Name;
            ServicesId = project.ServicesId;
            ProjectStatusId = project.ProjectStatusId;
            StatusPercent = project.StatusPercent;
            Description = project.Description;
            Plans = project.Plans;
        }

        public Project(int id, string name, int servicesId, int projectStatusId, double statusPercent, string description, List<Plan> plans)
        {
            Id = id;
            Name = name;
            ServicesId = servicesId;
            ProjectStatusId = projectStatusId;
            StatusPercent = statusPercent;
            Description = description;
            Plans = plans;
        }
    }
}
