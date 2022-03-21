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
        private SessionUser _sessionUser;

        private readonly PageNavigationService _navigation;
        public Page CurrentPage { get; set; }
        public string FullName { get; set; }
        public string Departament { get; set; }
        public string Role { get; set; }

        public MainWindowViewModel(PageNavigationService navigation)
        {

            MessageService.Bus += Receive;
            _navigation = navigation;
            navigation.OnPageChanged += page => CurrentPage = page;
            navigation.Navigate(new PlanWindow());          

        }

        /// <summary>
        /// Получение объекта спользователя с данными для текущей сессии
        /// </summary>        
        private void Receive(object data)
        {
            if (data is SessionUser sessionUser)
            {
                _sessionUser = sessionUser;
                setStrings();
                MessageService.Bus -= Receive;
            }            
        }

        /// <summary>
        /// Инициализация полей пользователя
        /// </summary>
        private void setStrings()
        {
            FullName = _sessionUser.user.fullName;
            Departament = DepartamentControl.GetNameById(_sessionUser.user.departamentId);
            Role = RoleControl.GetNameById(_sessionUser.user.roleId);
        }

        /// <summary>
        /// Команда открытия страницы Планов
        /// </summary>
        public ICommand OpenPlansPage => new DelegateCommand(() =>
        {
                                          
            _navigation.Navigate(new PlanWindow());
            MessageService.Send(_sessionUser);            
        });

        /// <summary>
        /// Команда открытия страницы Управления
        /// </summary>
        public ICommand OpenAppManagerPage => new DelegateCommand(() =>
        {
            _navigation.Navigate(new AppManagerPage());
            MessageService.Send(_sessionUser);            
        });

    }
}
