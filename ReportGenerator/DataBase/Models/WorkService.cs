using System;
using System.Collections.Generic;
using System.Text;
using ReportGenerator.DataBase.Controls;

namespace ReportGenerator.DataBase.Models
{
    public class WorkService
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int DepartamentId { get; set; }
        public string Description { get; set; }
        public List<Project> Projects { get; set; }

    }
}
