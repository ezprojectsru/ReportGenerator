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
        public int typeId { get; set; } 
        public int intensity { get; set; }
        public int startCompletion { get; set; }
        public int planCompletion { get; set; }
        public int reportId { get; set; }
        public string comment { get; set; }

        public Task()
        {
        }

        public Task(Task task)
        {
            this.id = task.id;
            this.name = task.name;
            this.planId = task.planId;
            this.priority = task.priority;
            this.typeId = task.typeId;
            this.intensity = task.intensity;
            this.startCompletion = task.startCompletion;
            this.planCompletion = task.planCompletion;
            this.reportId = task.reportId;
            this.comment = task.comment;
        }

        public Task(int id, string name, int planId, int priority, int typeId, int intensity, int startCompletion, int planCompletion, int reportId, string comment)
        {
            this.id = id;
            this.name = name;
            this.planId = planId;
            this.priority = priority;
            this.typeId = typeId;
            this.intensity = intensity;
            this.startCompletion = startCompletion;
            this.planCompletion = planCompletion;
            this.reportId = reportId;
            this.comment = comment;
        }
    }
}
