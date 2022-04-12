﻿using ReportGenerator.DataBase.Controls;
using System;

namespace ReportGenerator.DataBase.Models
{
    /// <summary>
    /// Вспомогательный клас для конвертации полей с ID в названия полей. Будет упразнен после написания конвертора.
    /// </summary>
    public class ItemPlan
    {
        private UserControl _userControl = new UserControl();
        private WorkProjectControl _workProjectControl = new WorkProjectControl();

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string Project { get; set; }
        public string Responsible { get; set; }
        public string Director { get; set; }
        public string Comment { get; set; }

        public ItemPlan(Plan plan)
        {
            Id = plan.id;
            Name = plan.name;
            StartDate = plan.startDate;
            FinishDate = plan.finishDate;
            Project = _workProjectControl.GetNameById(plan.projectId);
            Responsible = _userControl.GetFullNameById(plan.responsibleId);
            Director = _userControl.GetFullNameById(plan.directorId); ;
            Comment = plan.comment;

        }

        public ItemPlan(int id, string name, DateTime startDate, DateTime finishDate, string projectId, string responsibleId, string directorId, string comment)
        {
            Id = id;
            Name = name;
            StartDate = startDate;
            FinishDate = finishDate;
            Project = projectId; 
            Responsible = responsibleId;
            Director = directorId;
            Comment = comment;
        }


    }
}
