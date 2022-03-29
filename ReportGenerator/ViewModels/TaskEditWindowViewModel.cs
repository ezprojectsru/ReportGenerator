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
    /// Класс ViewModel для окна редактирования задач
    /// </summary>
    public class TaskEditWindowViewModel : BindableBase
    {
        private UserControl _userControl = new UserControl();
        private PlanControl _planControl = new PlanControl();
        private TaskTypeControl _taskTypeControl = new TaskTypeControl();
        public string Title { get; set; }
        private Task _task;
        private int _departamentId;
        private int _responsibleId;
        public List<string> Types { get; set; }
        private int Id;
        public string Name { get; set; }   
        public int PlanId { get; set; }
        public string Priority { get; set; }
        public int SelectPriority { get; set; }
        public int TaskTypeId { get; set; }
        public string TaskTypeName { get; set; }
        public string Intensity { get; set; }
        public string StartCompletion { get; set; }
        public string PlanCompletion { get; set; }
        public string Comment { get; set; }
        private int _taskEditPlanId { get; set; }
        private Task _taskEdit { get; set; }
        private PlanWindowViewModel _planWindowViewModel;




        public TaskEditWindowViewModel(PlanWindowViewModel planWindowViewModel)
        {
            _planWindowViewModel = planWindowViewModel;
            _taskEdit = planWindowViewModel.TaskEdit;
            _taskEditPlanId = planWindowViewModel.TaskEditPlanId;

            if (_taskEdit != null && _taskEditPlanId == 0)
            {
                _task = _taskEdit;
                setStrings();
            }
            else
            {
                clearStrings(_taskEditPlanId);
            }

            Title = "Создание задачи";            
            //MessageService.Bus += Receive;            
        }

        /// <summary>
        /// Принимает объекты, необходимые для работы класса.       
        /// Task - задача со страницы Планов
        /// INT - нулевый int, является сигналом, что мы не редактируем существующую задачу, я добавляем новую
        /// </summary> 
        /*private void Receive(object dataReceive)
        {
            if (dataReceive is Task data)
            {
                _task = data;
                setStrings();                
                MessageService.Bus -= Receive;
            }

            if (dataReceive is int id)
            {
                clearStrings(id);
                MessageService.Bus -= Receive;
            }
        }*/

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
            if (_task != null)
            {
                Id = _task.id;
                Name = _task.name;
                PlanId = _task.planId;
                Priority = _task.priority.ToString();
                SelectPriority = _task.priority - 1;
                TaskTypeId = _task.typeId; 
                TaskTypeName = _taskTypeControl.GetShortNameById(TaskTypeId);
                Intensity = _task.intensity.ToString();
                StartCompletion = _task.startCompletion.ToString();
                PlanCompletion = _task.planCompletion.ToString();
                Comment = _task.comment;
                Title = "Редактирование задачи";
                setTypes(PlanId);
            }
        }

        /// <summary>
        /// Инициализация полей формы в случае с созданием новой задачи
        /// </summary>
        private void clearStrings(int id)
        {
            Id = 0;
            Name = "";
            PlanId = id;
            Priority = "1";
            SelectPriority = 0;
            TaskTypeId = 0;
            if (TaskTypeId == 0)
            {
                TaskTypeName = "";
            }
            else
            {
                TaskTypeName = _taskTypeControl.GetShortNameById(TaskTypeId);
            }
            Intensity = "";
            StartCompletion = "";
            PlanCompletion = "";
            Comment = "";
            Title = "Создание задачи";
            setTypes(PlanId);
        }

        /// <summary>
        /// Команда для отправки результата работы окна
        /// </summary>
        public ICommand SendDialogResultTask => new DelegateCommand<object>((currentWindow) =>
        {
            if (!string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Intensity) && 
            !string.IsNullOrWhiteSpace(StartCompletion) && !string.IsNullOrWhiteSpace(PlanCompletion))
            {
                try
                {
                    Task _resultTask = new Task();
                    _resultTask.id = Id;
                    _resultTask.name = Name;
                    _resultTask.planId = PlanId;
                    _resultTask.priority = Int32.Parse(Priority);
                    _resultTask.typeId = _taskTypeControl.GetIdByShortName(TaskTypeName); // < ================= 2
                    _resultTask.intensity = Int32.Parse(Intensity);
                    _resultTask.startCompletion = Int32.Parse(StartCompletion);
                    _resultTask.planCompletion = Int32.Parse(PlanCompletion);
                    _resultTask.comment = Comment;

                    _planWindowViewModel.NewTask = _resultTask;
                    //MessageService.Send(_resultTask);
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
