using DevExpress.Mvvm;
using ReportGenerator.DataBase.Controls;
using ReportGenerator.DataBase.Models;
using ReportGenerator.Services;
using ReportGenerator.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace ReportGenerator.ViewModels
{
    /// <summary>
    /// Вспомогательный клас для конвертации полей с ID в названия полей. Будет упразнен после написания конвертора.
    /// </summary>
    public class ItemPlan
    {
        private UserControl _userControl = new UserControl();

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string Responsible { get; set; }
        public string Director { get; set; }
        public string Comment { get; set; }

        public ItemPlan(Plan plan)
        {
            Id = plan.id;
            Name = plan.name;
            StartDate = plan.startDate;
            FinishDate = plan.finishDate;
            Responsible = _userControl.GetFullNameById(plan.responsibleId);
            Director = _userControl.GetFullNameById(plan.directorId); ;
            Comment = plan.comment;

        }

        public ItemPlan(int id, string name, DateTime startDate, DateTime finishDate, string responsibleId, string directorId, string comment)
        {
            Id = id;
            Name = name;
            StartDate = startDate;
            FinishDate = finishDate;
            Responsible = responsibleId;
            Director = directorId;
            Comment = comment;
           
        }

        
    }


    /// <summary>
    /// Класс ViewModel страницы Планов
    /// </summary>
    public class PlanWindowViewModel : BindableBase
    {
        private UserControl _userControl = new UserControl();
        private TaskControl _taskControl = new TaskControl();
        private PlanControl _planControl = new PlanControl();
        private ItemPlan _planSelected;
        public ItemPlan PlanSelected { 
            get { return _planSelected; } 
            set
            {
                _planSelected = value;                
               if(_planSelected != null) GetTasksFromSelectedPlan(_planSelected.Id);
            } }

        private Task _taskSelected;
        public Task TaskSelected
        {
            get { return _taskSelected; }
            set
            {
                _taskSelected = value;   
                
            }
        }      

        private Task _newTask;
        private Plan _newPlan;
        private SessionUser _sessionUser;
        public string Title {get; set;}
        public List<ItemPlan> Plans { get; set; }
        public List<Task> Tasks { get; set; }
        public PlanWindowViewModel()
        {
            Title = "Список планов";
            MessageService.Bus += Receive;
        }

        /// <summary>
        /// Принимает объекты, необходимые для работы класса.
        /// SessionUser - пользователь текущей сесии
        /// Task - задача из редактора (окна) задач
        /// Plan - план из редактора (окна) планов
        /// </summary>        
        private void Receive(object data)
        {
            if (data is SessionUser sessionUser)
            {
                _sessionUser = sessionUser;
                getPlansList();
                MessageService.Bus -= Receive;
            }
            if (data is Task task)
            {
                _newTask = task;
                MessageService.Bus -= Receive;

            }
            if (data is Plan plan)
            {
                _newPlan = plan;
                MessageService.Bus -= Receive;

            }
        }

       
        /// <summary>
        /// Получение планов текущего пользователя
        /// </summary>
        private void getPlansList()
        {
            List<Plan> targetPlans = _planControl.GetPlanListByUserId(_sessionUser.user.id);
            Plans = new List<ItemPlan>();
            foreach (Plan pl in targetPlans)
            {                
                ItemPlan item = new ItemPlan(pl.id, pl.name, pl.startDate, pl.finishDate,
                    _userControl.GetFullNameById(pl.responsibleId),
                    _userControl.GetFullNameById(pl.directorId), pl.comment ?? "");
                Plans.Add(item);
            }            
        }

        /// <summary>
        /// Получение задач выбранного плана
        /// </summary>        
        public void GetTasksFromSelectedPlan(int id)
        {
            Tasks = _taskControl.GetTaskListByPlanId(id);    
            
        }

        /// <summary>
        /// Коменда для удаления выбранной задачи
        /// </summary>
        public ICommand DeleteSelectedtask => new DelegateCommand(() =>
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить задачу?", "Удаление задачи", 
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Task targetTask = _taskSelected;
                int planId = _taskSelected.planId;
                _taskControl.DeleteCurrentTask(_taskSelected);
                Tasks = _taskControl.GetTaskListByPlanId(planId);                
            }
        }, () => _taskSelected != null);

        /// <summary>
        /// Коменда для открытия окна создания новой задачи
        /// </summary>
        public ICommand OpenCreateNewTaskWindow => new DelegateCommand(() =>
        {
            int planId = _planSelected.Id;            
            TaskEditWindow taskEditWindow = new TaskEditWindow();
            MessageService.Send(planId); 
            MessageService.Bus += Receive;

            if (taskEditWindow.ShowDialog() == true)
            {
                if (_newTask.id == 0)
                {
                    _taskControl.InsertNewTask(_newTask);
                    Tasks = _taskControl.GetTaskListByPlanId(_newTask.planId);
                }                
            }
            else
            {
                MessageService.Bus -= Receive;                
            }
        }, () => _planSelected != null);

        /// <summary>
        /// Коменда для открытия окна редактирования выбранной задачи
        /// </summary>
        public ICommand OpenEditSelectedTaskWindow => new DelegateCommand(() =>
        {            
            Task targetTask = _taskSelected;
            TaskEditWindow taskEditWindow = new TaskEditWindow();            
            MessageService.Send(targetTask);
            MessageService.Bus += Receive;

            if (taskEditWindow.ShowDialog() == true)
            {
                if (_newTask.id != 0)
                {
                    _taskControl.UpdateCurrentTask(_newTask);
                    Tasks = _taskControl.GetTaskListByPlanId(_newTask.planId);
                }               

            }
            else
            {
                MessageService.Bus -= Receive;                
            }

        }, () => _taskSelected != null);

        /// <summary>
        /// Коменда для открытия окна редактирования выбранного плана
        /// </summary>
        public ICommand OpenEditSelectedPlanWindow => new DelegateCommand(() =>
        {           

            int responsibleId = _userControl.GetIddByFullName(_planSelected.Responsible);
            int directorId = _userControl.GetIddByFullName(_planSelected.Director); 

            Plan currentPlan = new Plan(_planSelected.Id, _planSelected.Name, 
                _planSelected.StartDate, _planSelected.FinishDate, responsibleId, 
                directorId, _planSelected.Comment);            
            
            PlanEditWindow planEditWindow = new PlanEditWindow();
            MessageService.Send(currentPlan);
            MessageService.Bus += Receive;

            if (planEditWindow.ShowDialog() == true)
            {
                if (_newPlan.id != 0)
                {
                    _planControl.UpdateCurrentPlan(_newPlan);

                    List<Plan> targetPlans = _planControl.GetPlanListByUserId(_sessionUser.user.id);
                    Plans = new List<ItemPlan>();
                    foreach (Plan pl in targetPlans)
                    {
                        ItemPlan item = new ItemPlan(pl.id, pl.name, pl.startDate, pl.finishDate,
                            _userControl.GetFullNameById(pl.responsibleId),
                            _userControl.GetFullNameById(pl.directorId), pl.comment ?? "");
                        Plans.Add(item);
                    }

                    _taskSelected = null;
                    Tasks = null;
                    _planSelected = null;
                }
            }
            else
            {
                MessageService.Bus -= Receive;
            }

        }, () => _planSelected != null);

        /// <summary>
        /// Коменда для открытия окна создания нового плана
        /// </summary>
        public ICommand OpenCreateNewPlanWindow => new DelegateCommand(() =>
        {
            PlanEditWindow planEditWindow = new PlanEditWindow();
            MessageService.Send(0);
            MessageService.Bus += Receive;

            if (planEditWindow.ShowDialog() == true)
            {

                if (_newPlan.id == 0)
                {
                    _planControl.InsertNewPlan(_newPlan);

                    List<Plan> targetPlans = _planControl.GetPlanListByUserId(_sessionUser.user.id);
                    Plans = new List<ItemPlan>();
                    foreach (Plan pl in targetPlans)
                    {
                        ItemPlan item = new ItemPlan(pl.id, pl.name, pl.startDate, pl.finishDate,
                            _userControl.GetFullNameById(pl.responsibleId),
                            _userControl.GetFullNameById(pl.directorId), pl.comment ?? "");
                        Plans.Add(item);
                    }

                    _taskSelected = null;
                    Tasks = null;                    
                    _planSelected = null;
                }
            }
            else
            {
                 MessageService.Bus -= Receive;
            }
        });

        /// <summary>
        /// Коменда для удаления выбранного плана и всех его задач
        /// </summary>
        public ICommand DeleteSelectedPlan => new DelegateCommand(() =>
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить план? Вместе с планом будут удалены и все его задачи.", 
                "Удаление плана", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                _taskControl.DeleteTasksByPlanId(_planSelected.Id);

                _planControl.DeleteCurrentPlan(_planSelected.Id);

                List<Plan> targetPlans = _planControl.GetPlanListByUserId(_sessionUser.user.id);
                Plans = new List<ItemPlan>();
                foreach (Plan pl in targetPlans)
                {
                    ItemPlan item = new ItemPlan(pl.id, pl.name, pl.startDate, pl.finishDate,
                        _userControl.GetFullNameById(pl.responsibleId),
                        _userControl.GetFullNameById(pl.directorId), pl.comment ?? "");
                    Plans.Add(item);
                }

                _taskSelected = null;
                Tasks = null;
                _planSelected = null;
            }
        }, () => _planSelected != null);

    }
}
