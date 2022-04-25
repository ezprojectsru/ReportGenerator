using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using DevExpress.Mvvm.POCO;
using ReportGenerator.Data.Controls;
using ReportGenerator.Data.Models;
using ReportGenerator.ViewModels.Base;

namespace ReportGenerator.ViewModels
{
    public class ProjectPageVM : ViewModel
    {
        #region Пользователь сесии
        private User _mainUser;
        public User MainUser
        {
            get => _mainUser;
            set => Set(ref _mainUser, value);
        }
        #endregion

        #region Пользователи
        private UserControl _userControl = new UserControl();
        private List<User> _users = new List<User>();
        public List<User> Users
        {
            get => _users;
            set => Set(ref _users, value);
        } 
        #endregion

        #region Сервисы
        private ServiceControl _serviceControl = new ServiceControl();
        private List<Service> _services = new List<Service>();
        public List<Service> Services
        {
            get => _services;
            set => Set(ref _services, value);
        }
        private Service _serviceSelected;
        public Service ServiceSelected
        {
            get => _serviceSelected;
            set => Set(ref _serviceSelected, value);
        }
        #endregion

        #region Проекты
        private ProjectControl _projectControl = new ProjectControl();
        private List<Project> _projects = new List<Project>();
        public List<Project> Projects
        {
            get => _projects;
            set => Set(ref _projects, value);
        }
        private Project _projectSelected;
        public Project ProjectSelected
        {
            get => _projectSelected;
            set => Set(ref _projectSelected, value);
        }
        #endregion

        #region Планы
        private PlanControl _planControl = new PlanControl();
        private List<Plan> _plans = new List<Plan>();
        public List<Plan> Plans
        {
            get => _plans;
            set => Set(ref _plans, value);
        }
        private Plan _planSelected;
        public Plan PlanSelected
        {
            get => _planSelected;
            set => Set(ref _planSelected, value);
        }
        #endregion

        #region Загрузка данных
        private Thread _thread;
        private CancellationTokenSource _tokenSource;
        private bool _isLoaded = false;
        public bool IsLoaded
        {
            get => _isLoaded;
            set
            {
                Set(ref _isLoaded, value);
                IsShow = !value;
            }
        }
        public bool IsShow { get; set; } = true; 
        #endregion

        public static ProjectPageVM Create(User user)
        {
            return ViewModelSource.Create(() => new ProjectPageVM(user));
        }
        public ProjectPageVM(User user)
        {
            MainUser = user;
            _tokenSource = new CancellationTokenSource();
            _thread = new Thread(LoadData) { IsBackground = true };
            _thread.Start(_tokenSource.Token);
        }
        
        #region Load Data
        private void LoadData(object state)
        {
            Services = _serviceControl.GetAllServicesListByDepartamentId(MainUser.DepartamentId);
            Projects = _projectControl.GetAllProjectList();
            Plans = _planControl.GetAllPlanList();
            Users = _userControl.GetAllUsersList();

            foreach (var service in Services)
            {
                LoadProjects(service);
            }

            IsLoaded = true;
        }

        private void LoadProjects(Service service)
        {
            List<Project> serviceProjects = Projects.FindAll(x => x.ServicesId == service.ID);
            service.Projects = new List<Project>();
            if (serviceProjects.Count > 0)
            {
                service.Projects.AddRange(serviceProjects);
                foreach (var project in service.Projects)
                {
                    LoadPlans(project);
                }
            }
            
        }

        private void LoadPlans(Project project)
        {
            List<Plan> projectPlans = Plans.FindAll(x => x.ProjectId == project.Id);
            project.Plans = new List<Plan>();
            if (projectPlans.Count > 0)
            {
                project.Plans.AddRange(projectPlans);
            }
        } 
        #endregion
    }
}
