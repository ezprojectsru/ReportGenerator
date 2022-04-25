using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DevExpress.Mvvm.POCO;
using ReportGenerator.Data.Controls;
using ReportGenerator.Data.Models;
using ReportGenerator.Services;
using ReportGenerator.ViewModels.Base;
using ReportGenerator.Views.Pages;

namespace ReportGenerator.ViewModels
{
    public class WrapperWindowVM : ViewModel
    {
        #region Пользователь
        private Thread _thread;
        private CancellationTokenSource _tokenSource;

        private User _mainUser;
        public User MainUser
        {
            get => _mainUser;
            set
            {
                Set(ref _mainUser, value);
                _tokenSource = new CancellationTokenSource();
                _thread = new Thread(LoadUserData) { IsBackground = true };
                _thread.Start(_tokenSource.Token);
            }
        }
        #endregion

        #region Департамент
        private DepartamentControl _departamentControl = new DepartamentControl();
        private Departament _mainUserDepartament;
        public Departament MainUserDepartament
        {
            get => _mainUserDepartament;
            set => Set(ref _mainUserDepartament, value);
        }
        #endregion

        #region Роль
        private RoleControl _roleControl = new RoleControl();
        private Role _mainUserRole;
        public Role MainUserRole
        {
            get => _mainUserRole;
            set => Set(ref _mainUserRole, value);
        }
        #endregion

        #region Статусбар
        private string _statusBarText = "Готово";
        public string StatusBarText
        {
            get => _statusBarText;
            set => Set(ref _statusBarText, value);
        }
        #endregion

        #region Страницы
        private Page _projectPage;
        private Page _planPage = new PlanPage();
        private Page _currentPage;
        public Page CurrentPage
        {
            get => _currentPage;
            set => Set(ref _currentPage, value);
        } 
        #endregion

        #region COMMANDS
        #region OpenProjectPageCmd
        public ICommand OpenProjectPageCmd { get; }
        private bool CanOpenProjectPageCmdExecute(object p) => true;

        private void OnOpenProjectPageCmdExecuted(object p)
        {
            CurrentPage = _projectPage;
        }
        #endregion

        #region OpenPlanPageCmd
        public ICommand OpenPlanPageCmd { get; }
        private bool CanOpenPlanPageCmdExecute(object p) => true;

        private void OnOpenPlanPageCmdExecuted(object p)
        {
            CurrentPage = _planPage;
        }
        #endregion

        #region CloseApplicationCmd
        public ICommand CloseApplicationCmd { get; }
        private bool CanCloseApplicationCmdExecute(object p) => true;

        private void OnCloseApplicationCmdExecuted(object p)
        {
            Application.Current.Shutdown();
        }
        #endregion

        #endregion

        public WrapperWindowVM(User user)
        {
            MainUser = user;
            ProjectPageVM vm = ProjectPageVM.Create(MainUser);
            ProjectPage projectPage = new ProjectPage(vm);
            _projectPage = projectPage;
            CurrentPage = _projectPage;

            #region INIT COMMANDS
            OpenProjectPageCmd = new LambdaCommand(OnOpenProjectPageCmdExecuted, CanOpenProjectPageCmdExecute);
            OpenPlanPageCmd = new LambdaCommand(OnOpenPlanPageCmdExecuted, CanOpenPlanPageCmdExecute);
            CloseApplicationCmd = new LambdaCommand(OnCloseApplicationCmdExecuted, CanCloseApplicationCmdExecute);
            #endregion
        }
        public static WrapperWindowVM Create(User user)
        {
            return ViewModelSource.Create(() => new WrapperWindowVM(user));
        }

        private void LoadUserData(object state)
        {
            MainUserDepartament = _departamentControl.GetDepartamentById(MainUser.DepartamentId);
            MainUserRole = _roleControl.GetRoleById(MainUser.RoleId);
        }

    }
}
