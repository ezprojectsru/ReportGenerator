using DevExpress.Mvvm;
using ReportGenerator.DataBase.Models;
using ReportGenerator.Services;
using System.Windows;
using System.Windows.Input;

namespace ReportGenerator.ViewModels
{
    /// <summary>
    /// Класс ViewModel для окна редактирования отделов
    /// </summary>
    public class DepartamentEditWindowViewModel : BindableBase
    {
        public string Title { get; set; }
        public Departament CurrentDepartament { get; set; }
        public DepartamentEditWindowViewModel()
        {
            Title = "Добавление отдела";
            MessageService.Bus += Receive;
        }

        /// <summary>
        /// Принимает объекты, необходимые для работы класса.       
        /// Departament - отдел со страница настроек и управления
        /// INT - нулевый int, является сигналом, что мы не редактируем существующий отдел, я добавляем новый
        /// </summary> 
        private void Receive(object dataReceive)
        {
            if (dataReceive is Departament data)
            {
                CurrentDepartament = data;
                Title = "Редактирование отдела";
                MessageService.Bus -= Receive;
            }

            if (dataReceive is int index)
            {
                CurrentDepartament = new Departament(index, "","","");
                MessageService.Bus -= Receive;
            }
        }
        
        /// <summary>
        /// Команда для отправки результата работы окна на страницу запроса
        /// </summary>
        public ICommand SendDialogResultDepartament => new DelegateCommand<object>((currentWindow) =>
        {
            if (!string.IsNullOrWhiteSpace(CurrentDepartament.name) && !string.IsNullOrWhiteSpace(CurrentDepartament.shortName))
            {
                    try
                    {
                        Departament _resultDepartament = new Departament(CurrentDepartament.id, CurrentDepartament.name, CurrentDepartament.shortName, CurrentDepartament.description);
                        MessageService.Send(_resultDepartament);
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
