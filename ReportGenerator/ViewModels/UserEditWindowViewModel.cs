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
    /// Класс ViewModel для окна редактирования пользователей
    /// </summary>
    public class UserEditWindowViewModel : BindableBase
    {
        
        private User _user;
        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                oldUsername = _user.username;
            }
        }
        public string Title { get; set; }
        public List<string> Departaments{ get; set; }
        public string Departament { get; set; }
        public List<string> Roles { get; set; }
        public string Role { get; set; }
        public List<string> Sectors { get; set; }
        public string Sector { get; set; }
        public List<string> Groups { get; set; }
        public string Group { get; set; }
        private string oldUsername;
        private User _userEdit { get; set; }
        private AppManagerPageViewModel _appManagerPageViewModel;
        private UserControl _userControl = new UserControl();
        private DepartamentControl _departamentControl = new DepartamentControl();
        private GroupControl _groupControl = new GroupControl();
        private RoleControl _roleControl = new RoleControl();
        private SectorControl _sectorControl = new SectorControl();

        public UserEditWindowViewModel(AppManagerPageViewModel appManagerPageViewModel)
        {
            Title = "Добавление пользователя";

            _appManagerPageViewModel = appManagerPageViewModel;
            _userEdit = appManagerPageViewModel.TargetUser;

            if (_userEdit != null)
            {
                User = _userEdit;
                setStrings();
            }
            else
            {
                clearStrings();
            }
           
        }

        /// <summary>
        /// Инициализация полей формы в случае с редактированием существующего пользователя
        /// </summary>
        private void setStrings()
        {
            if (User != null)
            {
                User.password = "";
                Title = "Редактирование пользователя";

                Departaments = new List<string>();
                Departaments = _departamentControl.GetAllNameDepartaments();
                Departament = _departamentControl.GetNameById(User.departamentId);
                Roles = new List<string>();
                Roles = _roleControl.GetAllNameRoles();
                Role = _roleControl.GetNameById(User.roleId);
                Sectors = new List<string>();
                Sectors = _sectorControl.GetAllNameSectors();
                Sector = _sectorControl.GetNameById(User.sectorId);
                Groups = new List<string>();
                Groups = _groupControl.GetAllNameGroups();
                Group = _groupControl.GetNameById(User.groupId);
            }
        }

        /// <summary>
        /// Инициализация полей формы в случае с добавлением нового пользователя
        /// </summary>
        private void clearStrings()
        {
            User = new User(0,"","", DateTime.Now, "", "", 0,0,0,0);
            Title = "Добавление пользователя";
            Departaments = new List<string>();
            Departaments = _departamentControl.GetAllNameDepartaments();
            Roles = new List<string>();
            Roles = _roleControl.GetAllNameRoles();
            Sectors = new List<string>();
            Sectors = _sectorControl.GetAllNameSectors();
            Groups = new List<string>();
            Groups = _groupControl.GetAllNameGroups();
        }


        private void SendDialogResultUserMethod(object currentWindow)
        {
            if (!string.IsNullOrWhiteSpace(User.username) &&
            !string.IsNullOrWhiteSpace(User.fullName) && !string.IsNullOrWhiteSpace(User.email))
            {
                if (User.id == 0)
                {
                    if (!string.IsNullOrWhiteSpace(User.password))
                    {
                        if (!_userControl.ExistsUserName(User.username))
                        {

                            try
                            {

                                User.departamentId = _departamentControl.GetIddByName(Departament);
                                User.roleId = _roleControl.GetIddByName(Role);
                                User.sectorId = _sectorControl.GetIddByName(Sector);
                                User.groupId = _groupControl.GetIddByName(Group);
                                _appManagerPageViewModel.NewUser = User;
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
                            MessageBox.Show("Такое имя уже существует!", "Ошибка");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Не задан пароль!", "Ошибка");
                    }
                }
                else
                {
                    if (oldUsername != User.username)
                    {
                        if (!_userControl.ExistsUserName(User.username))
                        {
                            try
                            {

                                User.departamentId = _departamentControl.GetIddByName(Departament);
                                User.roleId = _roleControl.GetIddByName(Role);
                                User.sectorId = _sectorControl.GetIddByName(Sector);
                                User.groupId = _groupControl.GetIddByName(Group);
                                _appManagerPageViewModel.NewUser = User;
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
                            MessageBox.Show("Такое имя уже существует!", "Ошибка");
                        }
                    }
                    else
                    {
                        try
                        {

                            User.departamentId = _departamentControl.GetIddByName(Departament);
                            User.roleId = _roleControl.GetIddByName(Role);
                            User.sectorId = _sectorControl.GetIddByName(Sector);
                            User.groupId = _groupControl.GetIddByName(Group);
                            _appManagerPageViewModel.NewUser = User;
                            Window wnd = currentWindow as Window;
                            wnd.DialogResult = true;
                            wnd.Close();
                        }
                        catch
                        {
                            MessageBox.Show("Поля заполнены не корректно!", "Ошибка");
                        }
                    }
                }


            }
            else
            {
                MessageBox.Show("Не все поля заполнены!", "Ошибка");
            }
        }


        public ICommand SendDialogResultUser => new DelegateCommand<object>((currentWindow) =>
        {
            SendDialogResultUserMethod(currentWindow);
        });
    }
}
