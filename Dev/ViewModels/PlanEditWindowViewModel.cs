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
        public string Title { get; set; }
        private Plan _plan;
        public List<string> Users { get; set; }
        public string ResponsibleFullName { get; set; }
        public string DirectorFullName { get; set; }
        private DateTime _startDate;
        private DateTime _finishDate; 
        public int Id;
        public string Name { get; set; }
        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }
        public DateTime FinishDate
        {
            get { return _finishDate; }
            set { _finishDate = value; }
        }
        public string ResponsibleId { get; set; }
        public string DirectorId { get; set; }
        public string Comment { get; set; }

        public PlanEditWindowViewModel()
        {
            Title = "Создание плана";
            MessageService.Bus += Receive;
        }

        /// <summary>
        /// Принимает объекты, необходимые для работы класса.       
        /// Plan - план со страницы Планов
        /// INT - нулевый int, является сигналом, что мы не редактируем существующий план, я добавляем новый
        /// </summary> 
        private void Receive(object dataReceive)
        {
            if (dataReceive is Plan data)
            {
                _plan = data;
                setStrings();
                MessageService.Bus -= Receive;
            }

            if (dataReceive is int id)
            {
                clearStrings();
                MessageService.Bus -= Receive;
            }
        }

        /// <summary>
        /// Инициализируем поля формы в случае редактирования плана
        /// </summary>
        private void setStrings()
        {
            if (_plan != null)
            {
                Id = _plan.id;
                Name = _plan.name;
                StartDate = _plan.startDate;
                FinishDate = _plan.finishDate;
                ResponsibleId = _plan.responsibleId.ToString();
                DirectorId = _plan.directorId.ToString();
                Comment = _plan.comment;
                Title = "Редактирование плана";                
                Users = new List<string>();
                Users = UserControl.GetAllFullNameUsers();
                ResponsibleFullName = UserControl.GetFullNameById(_plan.responsibleId);
                DirectorFullName = UserControl.GetFullNameById(_plan.directorId);
            }
        }

        /// <summary>
        /// Инициализируем поля формы в случае создания плана
        /// </summary>
        private void clearStrings()
        {
            Id = 0;
            Name = "";            
            StartDate = DateTime.Now;
            FinishDate = DateTime.Now;
            ResponsibleId = "";
            DirectorId = "";
            Comment = "";
            ResponsibleFullName = "";
            DirectorFullName = "";
            Title = "Создание плана";
            Users = new List<string>();
            Users = UserControl.GetAllFullNameUsers();
        }

        /// <summary>
        /// Команда для отправки результата работы окна
        /// </summary>
        public ICommand SendDialogResultPlan => new DelegateCommand<object>((currentWindow) =>
        {
            if (!string.IsNullOrWhiteSpace(Name))
            {
                try
                {
                    int responsibleId = UserControl.GetIddByFullName(ResponsibleFullName);
                    int directorId = UserControl.GetIddByFullName(DirectorFullName);
                    Plan _resultPlan= new Plan(Id, Name, StartDate, FinishDate, responsibleId, directorId, Comment);
                    MessageService.Send(_resultPlan);
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
        });
    }
}
