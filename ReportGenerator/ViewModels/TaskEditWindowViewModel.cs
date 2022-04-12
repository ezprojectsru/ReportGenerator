using DevExpress.Mvvm;
using ReportGenerator.DataBase.Controls;
using ReportGenerator.DataBase.Models;
using ReportGenerator.Services;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UserControl = ReportGenerator.DataBase.Controls.UserControl;

namespace ReportGenerator.ViewModels
{
    /// <summary>
    /// Класс ViewModel для окна редактирования задач
    /// </summary>
    public class TaskEditWindowViewModel : BindableBase
    {
        private UserControl _userControl = new UserControl();
        private PlanControl _planControl = new PlanControl();
        private TaskTypeControl _taskTypeControl = new TaskTypeControl();
        private PlanWindowViewModel _planWindowViewModel;

        
        public string Title { get; set; }
        public Task Task { get; set; }
        public List<TaskType> TaskTypes { get; set; }
        public int TaskTypeIndex { get; set; }
        private Task _taskEdit;
        private Task _reportEdit;
       

        

        public TaskEditWindowViewModel(PlanWindowViewModel planWindowViewModel)
        {
            _planWindowViewModel = planWindowViewModel;
            _taskEdit = planWindowViewModel.TaskEdit;
            _reportEdit = planWindowViewModel.ReportEdit;

           // _taskEditPlanId = planWindowViewModel.TaskEditPlanId;
            int planId = _reportEdit == null ? _taskEdit.planId : _reportEdit.planId;
            int responsibleId = _planControl.GetResponsibleIdByPlanId(planId);
            int departamentId = _userControl.GetDepartamentIdById(responsibleId);
            TaskTypes = _taskTypeControl.GetTaskTypeListByDepartamentId(departamentId);

            int typeId = _reportEdit == null ? _taskEdit.typeId : _reportEdit.typeId;
            TaskType tt = _taskTypeControl.GetTaskTypeById(typeId);
            TaskTypeIndex = TaskTypes.FindIndex(x => x.id == tt.id);

            Task = _reportEdit == null ? new Task(_taskEdit) : new Task(_reportEdit);
            if (_reportEdit == null)
            {
                Title = _taskEdit.id == 0 ? "Создание задачи" : "Редактирование задачи";
            }
            else
            {
                Title = _reportEdit.id == 0 ? "Создание отчета" : "Редактирование отчета";
            }
                      
                       
        }



        private void SendDialogResultTaskMethod(object currentWindow)
        {
            if (!string.IsNullOrWhiteSpace(Task.name))
            {
                try
                {
                    if (_reportEdit == null)
                    {
                        _planWindowViewModel.NewTask = Task;
                    }
                    else
                    {
                        _planWindowViewModel.NewReport = Task;
                    }

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
        public ICommand SendDialogResultTask => new DelegateCommand<object>((currentWindow) =>
        {
            SendDialogResultTaskMethod(currentWindow);
        });
    }
}
