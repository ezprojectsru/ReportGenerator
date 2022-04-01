using DevExpress.Mvvm;
using ReportGenerator.DataBase.Controls;
using ReportGenerator.DataBase.Models;
using ReportGenerator.Services;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace ReportGenerator.ViewModels
{
    /// <summary>
    /// Класс ViewModel для окна редактирования планов
    /// </summary>
    public class PlanEditWindowViewModel : BindableBase
    {
        private UserControl _userControl = new UserControl();
        public string Title { get; set; }
        public Plan Plan { get; set; }
        public List<string> Users { get; set; }
        public List<string> UsersDirector { get; set; }
        public string ResponsibleFullName { get; set; }
        public string DirectorFullName { get; set; }
        private Plan _planEdit { get; set; }
        private PlanWindowViewModel _planWindowViewModel;

        public PlanEditWindowViewModel(PlanWindowViewModel planWindowViewModel)
        {
            Title = "Создание плана";
            _planWindowViewModel = planWindowViewModel;
            _planEdit = planWindowViewModel.PlanEdit;

            if (_planEdit != null)
            {
                Plan = new Plan(_planEdit);
                setStrings();
            }
            else
            {
                clearStrings();
            }
            
        }

       
        /// <summary>
        /// Инициализируем поля формы в случае редактирования плана
        /// </summary>
        private void setStrings()
        {
            if (Plan != null)
            {
                
                Title = "Редактирование плана";   
                
                Users = new List<string>();
                Users = _userControl.GetAllFullNameUsers();
                UsersDirector = new List<string>();
                UsersDirector = _userControl.GetAllFullNameUsers();

                ResponsibleFullName = _userControl.GetFullNameById(Plan.responsibleId);
                DirectorFullName = _userControl.GetFullNameById(Plan.directorId);
            }
        }

        /// <summary>
        /// Инициализируем поля формы в случае создания плана
        /// </summary>
        private void clearStrings()
        {
            Plan = new Plan(0,"", DateTime.Now, DateTime.Now,0,0,"");
            ResponsibleFullName = "";
            DirectorFullName = "";
            Title = "Создание плана";
            Users = new List<string>();
            Users = _userControl.GetAllFullNameUsers();
            UsersDirector = new List<string>();
            UsersDirector = _userControl.GetAllFullNameUsers();
        }

        private void SendDialogResultPlanMethod(object currentWindow)
        {
            if (!string.IsNullOrWhiteSpace(Plan.name))
            {
                try
                {
                    Plan.responsibleId = _userControl.GetIddByFullName(ResponsibleFullName);
                    Plan.directorId = _userControl.GetIddByFullName(DirectorFullName);

                    
                    _planWindowViewModel.NewPlan = Plan;

                    Window wnd = currentWindow as Window;
                    wnd.DialogResult = true;
                    wnd.Close();
                }
                catch
                {
                    MessageBox.Show("Поля заполнены не корректно!", "Ошибка");
                }
            }
            else
            {
                MessageBox.Show("Не все поля заполнены!", "Ошибка");
            }
        }

        /// <summary>
        /// Команда для отправки результата работы окна
        /// </summary>
        public ICommand SendDialogResultPlan => new DelegateCommand<object>((currentWindow) =>
        {
            SendDialogResultPlanMethod(currentWindow);
        });
    }
}
