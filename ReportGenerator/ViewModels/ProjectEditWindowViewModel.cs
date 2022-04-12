using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using DevExpress.Mvvm;
using ReportGenerator.DataBase.Controls;
using ReportGenerator.DataBase.Models;

namespace ReportGenerator.ViewModels
{
    public class ProjectEditWindowViewModel : BindableBase
    {
        public string Title { get; set; }
        public Project Project { get; set; }
        public List<WorkService> WorkServices { get; set; }
        public List<ProjectStatus> ProjectStatuses { get; set; }
        public int ProjectStatusIndex { get; set; }
        public int WorkServiceIndex { get; set; }
        private Project _projectEdit { get; set; }
        private PlanWindowViewModel _planWindowViewModel;
        private WorkServiceControl _workServiceControl = new WorkServiceControl();
        private ProjectStatusControl _projectStatusControl = new ProjectStatusControl();

        public ProjectEditWindowViewModel(PlanWindowViewModel planWindowViewModel)
        {
            _planWindowViewModel = planWindowViewModel;
            _projectEdit = planWindowViewModel.ProjectEdit;
            Project = new Project(_projectEdit);

            WorkServices =
                _workServiceControl.GetAllWorkServicesByDepartamentId(_planWindowViewModel.SessionUser.user
                    .departamentId);
            WorkService ws = _workServiceControl.GetServiceById(_projectEdit.ServicesId);
            WorkServiceIndex = WorkServices.FindIndex(x => x.ID == ws.ID);

            ProjectStatuses = _projectStatusControl.GetAllProjectStatus();
            ProjectStatus ps = _projectStatusControl.GetProjectStatusById(_projectEdit.ProjectStatusId);
            ProjectStatusIndex = ProjectStatuses.FindIndex(x => x.Id == ps.Id);

            Title = _projectEdit.Id != 0 ? "Редактирование проекта" : "Создание проекта";
        }

        private  void SendDialogResultProjectMethod(object currentWindow)
        {
            if (!string.IsNullOrWhiteSpace(Project.Name))
            {
                try
                {
                    _planWindowViewModel.NewProject = Project;
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

        public ICommand SendDialogResultProject => new DelegateCommand<object>((currentWindow) =>
        {
            SendDialogResultProjectMethod(currentWindow);
        });
    }
}
