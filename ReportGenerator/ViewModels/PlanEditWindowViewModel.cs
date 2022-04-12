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
        public List<Project> Projects { get; set; }
        public int ItemIndex { get; set; }
        public int ResponsibleIndex { get; set; }
        public int DirectorIndex { get; set; }
        public List<User> Users { get; set; }
        public List<User> UsersDirector { get; set; }
        private Plan _planEdit { get; set; }
        private PlanWindowViewModel _planWindowViewModel;
        private ProjectControl _projectControl = new ProjectControl();

        public PlanEditWindowViewModel(PlanWindowViewModel planWindowViewModel)
        {
            _planWindowViewModel = planWindowViewModel;
            _planEdit = planWindowViewModel.PlanEdit;

            Plan = new Plan(_planEdit);
            Users = new List<User>();
            Users = _userControl.GetAllUsersList();
            UsersDirector = new List<User>();
            UsersDirector = _userControl.GetAllUsersList();
            int serviceId = _projectControl.GetServiceIdByProjectId(_planEdit.projectId);
            Projects = _projectControl.GetAllProjectsByServiceId(serviceId);
            Project pr = _projectControl.GetProjectById(_planEdit.projectId);
            ItemIndex = Projects.FindIndex(x => x.Id == pr.Id);
            User resp = _userControl.GetUserById(_planEdit.responsibleId);
            ResponsibleIndex = Users.FindIndex(x => x.id == resp.id);
            User director = _userControl.GetUserById(_planEdit.directorId);
            DirectorIndex = Users.FindIndex(x => x.id == director.id);
            Title = _planEdit.id != 0 ? "Редактирование плана" : "Создание плана";
        }


        private void SendDialogResultPlanMethod(object currentWindow)
        {
            if (!string.IsNullOrWhiteSpace(Plan.name))
            {
                try
                {
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
