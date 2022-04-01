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

        private int _departamentId;
        private int _responsibleId;
        public string Title { get; set; }
        public Task Task { get; set; }
        public List<string> Types { get; set; }
        public string TaskTypeName { get; set; }
        private int _taskEditPlanId { get; set; }
        private Task _taskEdit { get; set; }
        public List<int> PriorityList { get; set; }

        

        public TaskEditWindowViewModel(PlanWindowViewModel planWindowViewModel)
        {
            _planWindowViewModel = planWindowViewModel;
            _taskEdit = planWindowViewModel.TaskEdit;
            _taskEditPlanId = planWindowViewModel.TaskEditPlanId;
            PriorityList = new List<int>();
            for (int i = 0; i < 11; i++)
            {
                PriorityList.Add(i);
            }

            if (_taskEdit != null && _taskEditPlanId == 0)
            {
                Task = new Task(_taskEdit);
                setStrings();
            }
            else
            {
                clearStrings(_taskEditPlanId);
            }

            Title = "Создание задачи";            
                       
        }

        



        /// <summary>
        /// Инициализация combobox'ов
        /// </summary>        
        private void setTypes(int planId)
        {
            Types = new List<string>();
            _responsibleId = _planControl.GetResponsibleIdByPlanId(planId);
            _departamentId = _userControl.GetDepartamentIdById(_responsibleId);            
            Types = _taskTypeControl.GetTaskShortNamesListByDepartamentId(_departamentId);
        }

        /// <summary>
        /// Инициализация полей формы в случае с редактированием существующей задачи
        /// </summary>
        private void setStrings()
        {
            if (Task != null)
            {
                TaskTypeName = _taskTypeControl.GetShortNameById(Task.typeId);
                Title = "Редактирование задачи";
                setTypes(Task.planId);
            }
        }

        /// <summary>
        /// Инициализация полей формы в случае с созданием новой задачи
        /// </summary>
        private void clearStrings(int id)
        {
            Task = new Task(0,"",id,0,0,0,0,0,"");
            if (Task.typeId == 0)
            {
                TaskTypeName = "";
            }
            else
            {
                TaskTypeName = _taskTypeControl.GetShortNameById(Task.typeId);
            }
           
            Title = "Создание задачи";
            setTypes(Task.planId);
        }

        private void SendDialogResultTaskMethod(object currentWindow)
        {
            if (!string.IsNullOrWhiteSpace(Task.name))
            {
                try
                {
                    Task.typeId = _taskTypeControl.GetIdByShortName(TaskTypeName);
                    _planWindowViewModel.NewTask = Task;
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
