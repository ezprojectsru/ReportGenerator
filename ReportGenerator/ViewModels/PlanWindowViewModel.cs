using DevExpress.Mvvm;
using ReportGenerator.DataBase.Controls;
using ReportGenerator.DataBase.Models;
using ReportGenerator.Services;
using ReportGenerator.Views;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace ReportGenerator.ViewModels
{
    /// <summary>
    /// Класс ViewModel страницы Планов и Задач
    /// </summary>
    public class PlanWindowViewModel : BindableBase
    {
        private UserControl _userControl = new UserControl();
        private TaskControl _taskControl = new TaskControl();
        private PlanControl _planControl = new PlanControl();
        private ItemPlan _planSelected;
        public ItemPlan PlanSelected
        {
            get { return _planSelected; }
            set
            {
                _planSelected = value;
                if (_planSelected != null) GetTasksFromSelectedPlan(_planSelected.Id);
            }
        }

        private Task _taskSelected;
        public Task TaskSelected
        {
            get { return _taskSelected; }
            set
            {
                _taskSelected = value;

            }
        }

        public Task NewTask;
        public Plan NewPlan;
        public SessionUser SessionUser;
        public string Title { get; set; }
        public List<ItemPlan> Plans { get; set; }
        public List<Task> Tasks { get; set; }
        public int TaskEditPlanId { get; set; }
        public  Task TaskEdit { get; set; }
        public  Plan PlanEdit { get; set; }


        public PlanWindowViewModel(AuthorizationViewModel authorizationViewModel)
        {
            SessionUser = authorizationViewModel.SessionUser;
            getPlansList();
            Title = "Список планов";
            
        }

        /// <summary>
        /// Получение планов текущего пользователя
        /// </summary>
        private void getPlansList()
        {
            List<Plan> targetPlans = _planControl.GetPlanListByUserId(SessionUser.user.id);
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



        #region METHODS FOR COMMANDS
        private void DeleteSelectedtaskMethod()
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить задачу?", "Удаление задачи",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Task targetTask = _taskSelected;
                int planId = _taskSelected.planId;
                _taskControl.DeleteCurrentTask(_taskSelected);
                Tasks = _taskControl.GetTaskListByPlanId(planId);
            }
        }

        private void OpenCreateNewTaskWindowMethod()
        {
            TaskEditPlanId = _planSelected.Id;
            TaskEdit = null;
            int planId = _planSelected.Id;
            TaskEditWindow taskEditWindow = new TaskEditWindow();
            if (taskEditWindow.ShowDialog() == true)
            {
                if (NewTask.id == 0)
                {
                    _taskControl.InsertNewTask(NewTask);
                    Tasks = _taskControl.GetTaskListByPlanId(NewTask.planId);
                }
            }
        }

        private void OpenEditSelectedTaskWindowMethod()
        {
            TaskEditPlanId = 0;
            TaskEdit = _taskSelected;
            TaskEditWindow taskEditWindow = new TaskEditWindow();
            if (taskEditWindow.ShowDialog() == true)
            {
                if (NewTask.id != 0)
                {
                    _taskControl.UpdateCurrentTask(NewTask);
                    Tasks = _taskControl.GetTaskListByPlanId(NewTask.planId);
                }
            }
        }

        private void OpenEditSelectedPlanWindowMethod()
        {
            int responsibleId = _userControl.GetIddByFullName(_planSelected.Responsible);
            int directorId = _userControl.GetIddByFullName(_planSelected.Director);

            PlanEdit = new Plan(_planSelected.Id, _planSelected.Name,
                _planSelected.StartDate, _planSelected.FinishDate, responsibleId,
                directorId, _planSelected.Comment);

            PlanEditWindow planEditWindow = new PlanEditWindow();

            if (planEditWindow.ShowDialog() == true)
            {
                if (NewPlan.id != 0)
                {
                    _planControl.UpdateCurrentPlan(NewPlan);

                    List<Plan> targetPlans = _planControl.GetPlanListByUserId(SessionUser.user.id);
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
        }

        private void OpenCreateNewPlanWindowMethod()
        {
            PlanEditWindow planEditWindow = new PlanEditWindow();
            PlanEdit = null;

            if (planEditWindow.ShowDialog() == true)
            {
                if (NewPlan.id == 0)
                {
                    _planControl.InsertNewPlan(NewPlan);

                    List<Plan> targetPlans = _planControl.GetPlanListByUserId(SessionUser.user.id);
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
        }

        private void DeleteSelectedPlanMethod()
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить план? Вместе с планом будут удалены и все его задачи.",
                    "Удаление плана", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                _taskControl.DeleteTasksByPlanId(_planSelected.Id);

                _planControl.DeleteCurrentPlan(_planSelected.Id);

                List<Plan> targetPlans = _planControl.GetPlanListByUserId(SessionUser.user.id);
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
        #endregion

        #region COMMANDS
        /// <summary>
        /// Коменда для удаления выбранной задачи
        /// </summary>
        public ICommand DeleteSelectedtask => new DelegateCommand(() =>
        {
            DeleteSelectedtaskMethod();
        }, () => _taskSelected != null);

        /// <summary>
        /// Коменда для открытия окна создания новой задачи
        /// </summary>
        public ICommand OpenCreateNewTaskWindow => new DelegateCommand(() =>
        {
            OpenCreateNewTaskWindowMethod();
        }, () => _planSelected != null);

        /// <summary>
        /// Коменда для открытия окна редактирования выбранной задачи
        /// </summary>
        public ICommand OpenEditSelectedTaskWindow => new DelegateCommand(() =>
        {
            OpenEditSelectedTaskWindowMethod();
        }, () => _taskSelected != null);

        /// <summary>
        /// Коменда для открытия окна редактирования выбранного плана
        /// </summary>
        public ICommand OpenEditSelectedPlanWindow => new DelegateCommand(() =>
        {
            OpenEditSelectedPlanWindowMethod();
        }, () => _planSelected != null);

        /// <summary>
        /// Коменда для открытия окна создания нового плана
        /// </summary>
        public ICommand OpenCreateNewPlanWindow => new DelegateCommand(() =>
        {
            OpenCreateNewPlanWindowMethod();
        });

        /// <summary>
        /// Коменда для удаления выбранного плана и всех его задач
        /// </summary>
        public ICommand DeleteSelectedPlan => new DelegateCommand(() =>
        {
            DeleteSelectedPlanMethod();
        }, () => _planSelected != null); 
        #endregion

    }
}