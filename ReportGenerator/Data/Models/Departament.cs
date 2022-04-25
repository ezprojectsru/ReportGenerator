using System;
using System.Collections.Generic;
using System.Text;

namespace ReportGenerator.Data.Models
{
    public class Departament
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }

        public Departament()
        { }

        public Departament(int id, string name, string shortName, string description)
        {
            Id = id;
            Name = name;
            ShortName = shortName;
            Description = description;
        }
    }
}
