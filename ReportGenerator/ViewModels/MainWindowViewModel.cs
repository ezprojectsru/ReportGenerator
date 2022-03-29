using System.Windows;
using DevExpress.Mvvm;
using ReportGenerator.DataBase.Controls;
using ReportGenerator.DataBase.Models;
using ReportGenerator.Services;
using ReportGenerator.Views;
using System.Windows.Controls;
using System.Windows.Input;

namespace ReportGenerator.ViewModels
{
    /// <summary>
    /// Класс ViewModel основного окна приложения, содержащего в себе фрейм для отображения страниц приложения.
    /// </summary>
    public class MainWindowViewModel : BindableBase
    {
        private RoleControl _roleControl = new RoleControl();
        public SessionUser SessionUser;
        private readonly PageNavigationService _navigation;
        public Page CurrentPage { get; set; }
        public string FullName { get; set; }
        public string Departament { get; set; }
        public string Role { get; set; }

        private DepartamentControl _departamentControl = new DepartamentControl();
        public MainWindowViewModel(PageNavigationService navigation, AuthorizationViewModel authorizationViewModel)
        {
            SessionUser = authorizationViewModel.SessionUser;
            _navigation = navigation;
            setStrings();
            navigation.OnPageChanged += page => CurrentPage = page;
            navigation.Navigate(new PlanWindow());
        }

        /// <summary>
        /// Инициализация полей пользователя
        /// </summary>
        private void setStrings()
        {
            FullName = SessionUser.user.fullName;
            Departament = _departamentControl.GetNameById(SessionUser.user.departamentId);
            Role = _roleControl.GetNameById(SessionUser.user.roleId);
        }

        /// <summary>
        /// Команда открытия страницы Планов
        /// </summary>
        public ICommand OpenPlansPage => new DelegateCommand(() =>
        {
            _navigation.Navigate(new PlanWindow());
        });

        /// <summary>
        /// Команда открытия страницы Управления
        /// </summary>
        public ICommand OpenAppManagerPage => new DelegateCommand(() =>
        {
            _navigation.Navigate(new AppManagerPage());
        });

    }
}
