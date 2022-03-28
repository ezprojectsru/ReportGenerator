using ReportGenerator.DataBase.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportGenerator.DataBase.Models
{
    public class ItemTask
    {
        private TaskTypeControl _taskTypeControl = new TaskTypeControl();
        public int Id { get; set; }
        public string Name { get; set; }
        public int PlanId { get; set; }
        public int Priority { get; set; }
        public string TypeName { get; set; } 
        public int Intensity { get; set; }
        public int StartCompletion { get; set; }
        public int PlanCompletion { get; set; }
        public string Comment { get; set; }

        public ItemTask()
        { }
        public ItemTask(Task task)
        {
            
            Id = task.id;
            Name = task.name;
            PlanId = task.planId;
            Priority = task.priority;
            TypeName = _taskTypeControl.GetShortNameById(task.typeId);
            Intensity = task.intensity;
            StartCompletion = task.startCompletion;
            PlanCompletion = task.planCompletion;
            Comment = task.comment;
        }
    }
}
