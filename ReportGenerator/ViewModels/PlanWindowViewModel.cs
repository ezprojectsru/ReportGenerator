using System;
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
        private WorkServiceControl _workServicesControl = new WorkServiceControl();
       // private WorkProjectControl _workProjectControl = new WorkProjectControl();
        private WorkCalendarControl _workCalendarControl = new WorkCalendarControl();
        private ProjectControl _projectControl = new ProjectControl();

        private TaskTypeControl _taskTypeControl = new TaskTypeControl();
        private ProjectStatusControl _projectStatusControl = new ProjectStatusControl();


        private Plan _currentPlanSelected;
        public Plan CurrentPlanSelected
        {
            get { return _currentPlanSelected; }
            set
            {
                _currentPlanSelected = value;
                if (_currentPlanSelected != null)
                {
                    GetTasksFromSelectedPlan(_currentPlanSelected.id);
                }
                else
                {
                    Tasks = new List<Task>();
                }
            }
        }


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

        private WorkProject _projectSelected;
        public WorkProject ProjectSelected
        {
            get { return _projectSelected; }
            set
            {
                _projectSelected = value;
            }
        }

        public List<Project> MyProjects { get; set; }
        private Project _myProjectSelected;
        public Project MyProjectSelected
        {
            get { return _myProjectSelected; }
            set
            {
                CurrentPlanSelected = null;

                if (_myProjectSelected == value) return;
                _myProjectSelected = value;
                
            }
        }

        private WorkService _workServiceSelected;
        public WorkService WorkServiceSelected
        {
            get { return _workServiceSelected; }
            set
            {
                

                if (_workServiceSelected == value) return;
                _workServiceSelected = value;

                if (WorkServiceSelected != null)
                {
                    _myProjectSelected = null;
                    GetProjectList();
                }

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

        private Task _reportSelected;
        public Task ReportSelected
        {
            get { return _reportSelected; }
            set
            {
                _reportSelected = value;

            }
        }

        public Task NewTask;
        public Task NewReport;
        public Plan NewPlan;
        public Project NewProject;
        public SessionUser SessionUser;
        public string Title { get; set; }
        public List<ItemPlan> Plans { get; set; }
        public List<Task> Tasks { get; set; }
        public List<WorkProject> Projects { get; set; }
        public List<WorkService> WorkServices { get; set; }
        public int TaskEditPlanId { get; set; }
        public  Task TaskEdit { get; set; }
        public  Task ReportEdit { get; set; }
        public  Plan PlanEdit { get; set; }
        public Project ProjectEdit { get; set; }

        public int SelectWCPos { get; set; }
        private WorkCalendar _workCalendar;
        public WorkCalendar WorkCalendar
        {
            get { return _workCalendar; }
            set
            {
                _workCalendar = value;
                // getPlansList();
                GetProjectList();
            }
        }
        public List<WorkCalendar> WorkCalendars { get; set; }
       // public List<string> WorkCalendarsStrings { get; set; } = new List<string>();


        public PlanWindowViewModel(AuthorizationViewModel authorizationViewModel)
        {
            SessionUser = authorizationViewModel.SessionUser;

            WorkCalendars = new List<WorkCalendar>();
            WorkCalendars = _workCalendarControl.GetAllWeeksInYear(DateTime.Now.Year);
            _workCalendar = _workCalendarControl.GetCurrentWeek(DateTime.Now);
            SelectWCPos = WorkCalendars.FindIndex(x => x.Id == WorkCalendar.Id);

            WorkServices = _workServicesControl.GetAllWorkServicesByDepartamentId(SessionUser.user.departamentId);

           // Projects = _workProjectControl.GetAllProjects();
           //MyProjects = new List<Project>();
           //MyProjects = _myProjectControl.GetAllProjectsByServiceId(WorkServiceSelected.ID);

            
            Title = "Планы";
            
        }

        private void GetProjectList()
        {
            MyProjects = new List<Project>();
            MyProjects = _projectControl.GetAllWeekProjectsByServiceId(WorkServiceSelected.ID,WorkCalendar.StartWeek, WorkCalendar.EndWeek.AddDays(2));
           // getPlansList();
        }

        /// <summary>
        /// Получение планов текущего пользователя
        /// </summary>
       /* private void getPlansList()
        {
            
            
            // WorkCalendarsStrings = _workCalendarControl.GetAllWeeksStringsInYear(DateTime.Now.Year);

            //List<Plan> targetPlans = _planControl.GetPlanListByUserId(SessionUser.user.id);
            List<Plan> targetPlans = _planControl.GetPlanListBetweenDatesByUserId(SessionUser.user.id, WorkCalendar.StartWeek, WorkCalendar.EndWeek);
            Plans = new List<ItemPlan>();
            foreach (Plan pl in targetPlans)
            {
                ItemPlan item = new ItemPlan(pl.id, pl.name, pl.startDate, pl.finishDate,
                    _workProjectControl.GetNameById(pl.projectId),
                    _userControl.GetFullNameById(pl.responsibleId),
                    _userControl.GetFullNameById(pl.directorId), 
                    pl.comment ?? "");
                Plans.Add(item);
            }

            _taskSelected = null;
            Tasks = null;
            _planSelected = null;
        }*/

                
        public void GetTasksFromSelectedPlan(int id)
        {
            Tasks = _taskControl.GetTaskListByPlanId(id);

        }



        #region METHODS FOR COMMANDS

        

        private void DeleteSelectedTaskMethod()
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить задачу?", "Удаление задачи",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                
                int planId = _taskSelected.planId;
                _taskControl.DeleteTasksByReportId(_taskSelected.id);
                _taskControl.DeleteCurrentTask(_taskSelected);
                Tasks = _taskControl.GetTaskListByPlanId(planId);
            }
        }

        private void OpenCreateNewTaskWindowMethod()
        {
            //TaskEditPlanId = _currentPlanSelected.id;
            ReportEdit = null;
            List<TaskType> tt = _taskTypeControl.GetTaskTypeListByDepartamentId(SessionUser.user.departamentId);
            TaskEdit =  new Task(0, "", _currentPlanSelected.id, 0, tt[0].id, 0, 0, 0, 0, "");

            TaskEditWindow taskEditWindow = new TaskEditWindow();
            if (taskEditWindow.ShowDialog() == true)
            {
                if (NewTask.id == 0)
                {
                    _taskControl.InsertNewTask(NewTask);
                    NewTask.reportId = _taskControl.GetLastIndex();
                    _taskControl.InsertNewTask(NewTask);
                    Tasks = _taskControl.GetTaskListByPlanId(NewTask.planId);
                    
                }
            }
        }

        private void OpenEditSelectedTaskWindowMethod()
        {
            int planId = _taskSelected.planId;

            if (_taskSelected.reportId == 0)
            {
                ReportEdit = null;
                TaskEdit = _taskSelected;
                TaskEditWindow taskEditWindow = new TaskEditWindow();
                if (taskEditWindow.ShowDialog() == true)
                {
                    
                        _taskControl.UpdateCurrentTask(NewTask);
                       
                    
                }
            }
            else
            {
                ReportEdit = _taskSelected;
                
                
                TaskEdit = null;

                TaskEditWindow taskEditWindow = new TaskEditWindow();
                if (taskEditWindow.ShowDialog() == true)
                {
                    
                        _taskControl.UpdateCurrentTask(NewReport);

                }
            }

            Tasks = _taskControl.GetTaskListByPlanId(planId);


        }

        private void OpenEditSelectedPlanWindowMethod()
        {
            /*int responsibleId = _userControl.GetIddByFullName(_planSelected.Responsible);
            int directorId = _userControl.GetIddByFullName(_planSelected.Director);
            int projectId = _workProjectControl.GetIdByName(_planSelected.Project);*/

            PlanEdit = new Plan(_currentPlanSelected.id, _currentPlanSelected.name,
                _currentPlanSelected.startDate, _currentPlanSelected.finishDate, _currentPlanSelected.projectId, _currentPlanSelected.responsibleId,
                _currentPlanSelected.directorId, _currentPlanSelected.comment);

            PlanEditWindow planEditWindow = new PlanEditWindow();

            if (planEditWindow.ShowDialog() == true)
            {
                if (NewPlan.id != 0)
                {
                    _planControl.UpdateCurrentPlan(NewPlan);

                    // getPlansList();
                    GetProjectList();
                }
            }
        }

        private void OpenCreateNewPlanWindowMethod()
        {
            
            PlanEdit = new Plan(0, "", DateTime.Now, DateTime.Now, _myProjectSelected.Id, SessionUser.user.id, SessionUser.user.id, "");
            PlanEditWindow planEditWindow = new PlanEditWindow();
            

            if (planEditWindow.ShowDialog() == true)
            {
                if (NewPlan.id == 0)
                {
                    _planControl.InsertNewPlan(NewPlan);

                    //getPlansList();
                    GetProjectList();



                }
            }
        }

        private void OpenCreateNewProjectWindowMethod()
        {
            List<ProjectStatus> pss = _projectStatusControl.GetAllProjectStatus();
            ProjectEdit = new Project(0, "", _workServiceSelected.ID, pss[0].Id, 0, "");
            ProjectEditWindow projectEditWindow = new ProjectEditWindow();


            if (projectEditWindow.ShowDialog() == true)
            {
                if (NewProject.Id == 0)
                {
                   // _planControl.InsertNewPlan(NewPlan);
                    _projectControl.InsertNewProject(NewProject);

                    //getPlansList();
                    WorkService ws = WorkServiceSelected;
                    WorkServices = _workServicesControl.GetAllWorkServicesByDepartamentId(SessionUser.user.departamentId);
                    WorkServiceSelected = ws;
                    GetProjectList();



                }
            }
        }

        private void OpenEditSelectedProjectWindowMethod()
        {
            ProjectEdit = new Project(_myProjectSelected.Id, _myProjectSelected.Name, _myProjectSelected.ServicesId, _myProjectSelected.ProjectStatusId, _myProjectSelected.StatusPercent, _myProjectSelected.Description);
            
            ProjectEditWindow projectEditWindow = new ProjectEditWindow();

            if (projectEditWindow.ShowDialog() == true)
            {
                if (NewProject.Id != 0)
                {
                   // _planControl.UpdateCurrentPlan(NewPlan);
                    _projectControl.UpdateCurrentProject(NewProject);

                    // getPlansList();
                    WorkService ws = WorkServiceSelected;
                    WorkServices = _workServicesControl.GetAllWorkServicesByDepartamentId(SessionUser.user.departamentId);
                    WorkServiceSelected = ws;
                    GetProjectList();
                }
            }
        }
        private void DeleteSelectedProjectMethod()
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить проект? Вместе с проектом будут удалены и все его планы и задачи.",
                    "Удаление проекта", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                List<Plan> plans = _planControl.GetPlanListByProjectId(_myProjectSelected.Id);
                foreach (var pl in plans)
                {
                    _taskControl.DeleteTasksByPlanId(pl.id);
                    _planControl.DeleteCurrentPlan(pl.id);
                }
                _projectControl.DeleteCurrentProject(_myProjectSelected.Id);

                // getPlansList();
                WorkService ws = WorkServiceSelected;
                WorkServices = _workServicesControl.GetAllWorkServicesByDepartamentId(SessionUser.user.departamentId);
                WorkServiceSelected = ws;
                GetProjectList();
            }
        }

        private void DeleteSelectedPlanMethod()
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить план? Вместе с планом будут удалены и все его задачи.",
                    "Удаление плана", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                _taskControl.DeleteTasksByPlanId(_currentPlanSelected.id);

                _planControl.DeleteCurrentPlan(_currentPlanSelected.id);

               // getPlansList();
                GetProjectList();
            }
        }
        #endregion

        #region COMMANDS

        #region CALENDAR COMMAND
        public ICommand GoToNextWeek => new DelegateCommand(() =>
        {
            SelectWCPos += 1;
        }, () => SelectWCPos < WorkCalendars.Count - 1);
        public ICommand GoToLastWeek => new DelegateCommand(() =>
        {
            SelectWCPos -= 1;
        }, () => SelectWCPos > 0); 
        #endregion

        #region TASK COMMAND
        /// <summary>
        /// Коменда для удаления выбранной задачи
        /// </summary>
        public ICommand DeleteSelectedtask => new DelegateCommand(() =>
        {
            DeleteSelectedTaskMethod();
        }, () => _taskSelected != null && _taskSelected.reportId == 0);

        /// <summary>
        /// Коменда для открытия окна создания новой задачи
        /// </summary>
        public ICommand OpenCreateNewTaskWindow => new DelegateCommand(() =>
        {
            OpenCreateNewTaskWindowMethod();
        }, () => _currentPlanSelected != null );

        /// <summary>
        /// Коменда для открытия окна редактирования выбранной задачи
        /// </summary>
        public ICommand OpenEditSelectedTaskWindow => new DelegateCommand(() =>
        {
            OpenEditSelectedTaskWindowMethod();
        }, () => _taskSelected != null); 
        #endregion

        #region PLAN COMMAND
        /// <summary>
        /// Коменда для открытия окна редактирования выбранного плана
        /// </summary>
        public ICommand OpenEditSelectedPlanWindow => new DelegateCommand(() =>
        {
            OpenEditSelectedPlanWindowMethod();
        }, () => _currentPlanSelected != null);

        /// <summary>
        /// Коменда для открытия окна создания нового плана
        /// </summary>
        public ICommand OpenCreateNewPlanWindow => new DelegateCommand(() =>
        {
            OpenCreateNewPlanWindowMethod();
        }, () => _workServiceSelected != null && _myProjectSelected != null);

        /// <summary>
        /// Коменда для удаления выбранного плана и всех его задач
        /// </summary>
        public ICommand DeleteSelectedPlan => new DelegateCommand(() =>
        {
            DeleteSelectedPlanMethod();
        }, () => _currentPlanSelected != null); 
        #endregion

        #region PROJECT COMMAND
        public ICommand OpenCreateNewProjectWindow => new DelegateCommand(() =>
        {
            OpenCreateNewProjectWindowMethod();
        }, () => _workServiceSelected != null);

        public ICommand OpenEditSelectedProjectWindow => new DelegateCommand(() =>
        {
            OpenEditSelectedProjectWindowMethod();
        }, () => _myProjectSelected != null);
        public ICommand DeleteSelectedProject => new DelegateCommand(() =>
        {
            DeleteSelectedProjectMethod();
        }, () => _myProjectSelected != null); 
        #endregion


        #endregion

    }
}