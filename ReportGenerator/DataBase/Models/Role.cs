using System;
using System.Collections.Generic;
using System.Text;

namespace ReportGenerator.DataBase.Models
{
    public class Role
    {
        /// <summary>
        /// Класс модели Роли
        /// </summary>
        public int id { get; set; }
        public string name { get; set; }

        public Role()
        { }

        public Role(int id, string name)
        {
            this.id = id;
            this.name = name;

        }
    }    
}
