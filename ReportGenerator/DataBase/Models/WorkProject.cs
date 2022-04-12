using System;
using System.Collections.Generic;
using System.Text;

namespace ReportGenerator.DataBase.Models
{
    public class WorkProject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ServicesId { get; set; }
        public int ProjectStatusId { get; set; }
        public int StatusPercent { get; set; }
        public string Description { get; set; }

    }
}
