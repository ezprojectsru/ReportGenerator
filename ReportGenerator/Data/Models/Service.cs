using System;
using System.Collections.Generic;
using System.Text;

namespace ReportGenerator.Data.Models
{
    public class Service
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int DepartamentId { get; set; }
        public string Description { get; set; }
        public List<Project> Projects { get; set; }
    }
}
