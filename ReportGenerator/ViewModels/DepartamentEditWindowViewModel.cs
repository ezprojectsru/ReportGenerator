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
        private Departament _departamentEdit { get; set; }

        private AppManagerPageViewModel _appManagerPageViewModel;
        public DepartamentEditWindowViewModel(AppManagerPageViewModel appManagerPageViewModel)
        {
            Title = "Добавление отдела";

            _appManagerPageViewModel = appManagerPageViewModel;
            _departamentEdit = appManagerPageViewModel.DepartamentSelected;

            if (_departamentEdit != null)
            {
                CurrentDepartament = _departamentEdit;
                Title = "Редактирование отдела";
            }
            else
            {
                CurrentDepartament = new Departament(0, "", "", "");
            }

           // MessageService.Bus += Receive;
        }

        private void SendDialogResultDepartamentMethod(object currentWindow)
        {
            if (!string.IsNullOrWhiteSpace(CurrentDepartament.name) && !string.IsNullOrWhiteSpace(CurrentDepartament.shortName))
            {
                try
                {
                    Departament _resultDepartament = new Departament(CurrentDepartament.id, CurrentDepartament.name, CurrentDepartament.shortName, CurrentDepartament.description);
                    _appManagerPageViewModel.NewDepartament = _resultDepartament;
                    // MessageService.Send(_resultDepartament);
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
        /// Команда для отправки результата работы окна на страницу запроса
        /// </summary>
        public ICommand SendDialogResultDepartament => new DelegateCommand<object>((currentWindow) =>
        {
            SendDialogResultDepartamentMethod(currentWindow);
        });
    }
}
