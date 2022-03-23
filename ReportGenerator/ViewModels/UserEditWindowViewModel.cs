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
        public string Title { get; set; }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime СreateDate { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string DepartamentId { get; set; }
        public string RoleId { get; set; }
        public string SectorId { get; set; }
        public string GroupId { get; set; }
        private User _user;
        public List<string> Departaments{ get; set; }
        public string Departament { get; set; }
        public List<string> Roles { get; set; }
        public string Role { get; set; }
        public List<string> Sectors { get; set; }
        public string Sector { get; set; }
        public List<string> Groups { get; set; }
        public string Group { get; set; }
        private string oldUsername;

        private UserControl _userControl = new UserControl();
        private DepartamentControl _departamentControl = new DepartamentControl();
        private GroupControl _groupControl = new GroupControl();
        private RoleControl _roleControl = new RoleControl();
        private SectorControl _sectorControl = new SectorControl();

        public UserEditWindowViewModel()
        {
            Title = "Добавление пользователя";
            MessageService.Bus += Receive;
        }

        /// <summary>
        /// Принимает объекты, необходимые для работы класса.       
        /// User - потльзователь со страницы Управления и настроек
        /// INT - нулевый int, является сигналом, что мы не редактируем существующего пользователя, я добавляем нового
        /// </summary>
        private void Receive(object dataReceive)
        {
            if (dataReceive is User data)
            {
                _user = data;
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
        /// Инициализация полей формы в случае с редактированием существующего пользователя
        /// </summary>
        private void setStrings()
        {
            if (_user != null)
            {
                Id = _user.id;
                Username = _user.username;
                oldUsername = Username;
                Password = _user.password;
                СreateDate = _user.create_date;
                FullName = _user.fullName;
                Email = _user.email;
                DepartamentId = _user.departamentId.ToString();
                RoleId = _user.roleId.ToString();
                SectorId = _user.sectorId.ToString();
                GroupId = _user.groupId.ToString();
                Title = "Редактирование пользователя";
                Departaments = new List<string>();
                Departaments = _departamentControl.GetAllNameDepartaments();
                Departament = _departamentControl.GetNameById(_user.departamentId);
                Roles = new List<string>();
                Roles = _roleControl.GetAllNameRoles();
                Role = _roleControl.GetNameById(_user.roleId);
                Sectors = new List<string>();
                Sectors = _sectorControl.GetAllNameSectors();
                Sector = _sectorControl.GetNameById(_user.sectorId);
                Groups = new List<string>();
                Groups = _groupControl.GetAllNameGroups();
                Group = _groupControl.GetNameById(_user.groupId);
            }
        }

        /// <summary>
        /// Инициализация полей формы в случае с добавлением нового пользователя
        /// </summary>
        private void clearStrings()
        {
            Id = 0;
            Username = "";
            Password = "";
            СreateDate = DateTime.Now;
            FullName = "";
            Email = "";
            DepartamentId = "";
            RoleId = "";
            SectorId = "";
            GroupId = "";
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


        /// <summary>
        /// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        /// 
        ///      ТУТ ВСЕ ПЕРЕДЕЛАЮ !!!!
        ///      
        ///      в 00.00 нашел случайно багу, но сил совсем не осталось :))
        ///      Пофиксил пока так...
        ///      
        /// </summary>
        public ICommand SendDialogResultUser => new DelegateCommand<object>((currentWindow) =>
        {
            if (!string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password) && 
            !string.IsNullOrWhiteSpace(FullName) && !string.IsNullOrWhiteSpace(Email))
            {
                if (Id == 0)
                {
                    if (!_userControl.ExistsUserName(Username))
                    {

                        try
                        {
                            int departamentId = _departamentControl.GetIddByName(Departament);
                            int roleId = _roleControl.GetIddByName(Role);
                            int sectorId = _sectorControl.GetIddByName(Sector);
                            int groupId = _groupControl.GetIddByName(Group);
                            User _resultUser = new User(Id, Username, Password, СreateDate, FullName, 
                                Email, departamentId, roleId, sectorId, groupId);
                            MessageService.Send(_resultUser);
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
                    if (oldUsername != Username)
                    {
                        if (!_userControl.ExistsUserName(Username))
                        {
                            try
                            {

                                int departamentId = _departamentControl.GetIddByName(Departament);
                                int roleId = _roleControl.GetIddByName(Role);
                                int sectorId = _sectorControl.GetIddByName(Sector);
                                int groupId = _groupControl.GetIddByName(Group);
                                User _resultUser = new User(Id, Username, Password, СreateDate, 
                                    FullName, Email, departamentId, roleId, sectorId, groupId);
                                MessageService.Send(_resultUser);
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

                            int departamentId = _departamentControl.GetIddByName(Departament);
                            int roleId = _roleControl.GetIddByName(Role);
                            int sectorId = _sectorControl.GetIddByName(Sector);
                            int groupId = _groupControl.GetIddByName(Group);
                            User _resultUser = new User(Id, Username, Password, СreateDate, 
                                FullName, Email, departamentId, roleId, sectorId, groupId);
                            MessageService.Send(_resultUser);
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
        });
    }
}
