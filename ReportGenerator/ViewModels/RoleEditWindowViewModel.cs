using DevExpress.Mvvm;
using ReportGenerator.DataBase.Models;
using ReportGenerator.Services;
using System.Windows;
using System.Windows.Input;

namespace ReportGenerator.ViewModels
{
    public class RoleEditWindowViewModel : BindableBase
    {
        public string Title { get; set; }
        public Role CurrentRole{ get; set; }
        private Role _roletEdit { get; set; }

        private AppManagerPageViewModel _appManagerPageViewModel;

        public RoleEditWindowViewModel(AppManagerPageViewModel appManagerPageViewModel)
        {
            Title = "Добавление роли";
            _appManagerPageViewModel = appManagerPageViewModel;
            _roletEdit = appManagerPageViewModel.RoleSelected;

            if (_roletEdit != null)
            {
                CurrentRole = _roletEdit;
                Title = "Редактирование роли";
            }
            else
            {
                CurrentRole = new Role(0, "");
            }
        }

        private void SendDialogResultRoleMethod(object currentWindow)
        {
            if (!string.IsNullOrWhiteSpace(CurrentRole.name))
            {
                try
                {
                    Role _resultRole = new Role(CurrentRole.id, CurrentRole.name);
                    _appManagerPageViewModel.NewRole = _resultRole;

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

        public ICommand SendDialogResultRole => new DelegateCommand<object>((currentWindow) =>
        {
            SendDialogResultRoleMethod(currentWindow);
        });
    }
}
