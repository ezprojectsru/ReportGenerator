using System;
using System.Collections.Generic;
using System.Text;

namespace ReportGenerator.DataBase.Models
{/// <summary>
 /// Класс модели Задачи
 /// </summary>
    public class Task
    {
        public int id { get; set; }
        public string name { get; set; }
        public int planId { get; set; }        
        public int priority { get; set; }
        public int typeId { get; set; } // typeId
        public int intensity { get; set; }
        public int startCompletion { get; set; }
        public int planCompletion { get; set; }
        public string comment { get; set; }
    }
}
