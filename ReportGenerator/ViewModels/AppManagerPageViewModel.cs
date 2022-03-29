using DevExpress.Mvvm;
using ReportGenerator.DataBase.Controls;
using ReportGenerator.DataBase.Models;
using ReportGenerator.Services;
using ReportGenerator.Views;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace ReportGenerator.ViewModels
{   

    /// <summary>
    /// Класс ViewModel для страницы настроек и управления
    /// </summary>
    public class AppManagerPageViewModel : BindableBase
    {
        private UserControl _userControl = new UserControl();
        private DepartamentControl _departamentControl = new DepartamentControl();
        private GroupControl _groupControl = new GroupControl();
        private RoleControl _roleControl = new RoleControl();
        private SectorControl _sectorControl = new SectorControl();
        public List<Role> ListRoles { get; set; }
        private Role _roleSelected;
        public Role RoleSelected
        {
            get { return _roleSelected; }
            set
            {
                _roleSelected = value;
            }
        }

        public List<Departament> ListDepartaments { get; set; }
        private Departament _departamentSelected;
        public Departament DepartamentSelected
        {
            get { return _departamentSelected; }
            set
            {
                _departamentSelected = value;                
            }
        }
        public List<User> ListOfUsers { get; set; }
        public List<ItemUser> Users { get; set; }

        public User NewUser;
        public Departament NewDepartament;
        public Role NewRole;

        public SessionUser SessionUser;

        private ItemUser _userSelected;
        public ItemUser UserSelected
        {
            get { return _userSelected; }
            set
            {
                _userSelected = value;               
            }
        }

        public User TargetUser;

        public AppManagerPageViewModel(MainWindowViewModel mainWindowViewModel)
        {
            SessionUser = mainWindowViewModel.SessionUser;

            ListOfUsers = _userControl.GetAllUsersList();
            Users = new List<ItemUser>();
            foreach(User user in ListOfUsers)
            {
                Users.Add(new ItemUser (user));
            }
            
            ListDepartaments = new List<Departament>();
            ListDepartaments = _departamentControl.GetAllDepartamentsList();

            ListRoles = new List<Role>();
            ListRoles = _roleControl.GetAllRolesList();

        }

               
       
        
        /// <summary>
        /// Команда для открытия окна редактирования выбранного пользователя
        /// </summary>
        public ICommand OpenEditSelectedUserWindow => new DelegateCommand(() =>
        {
            TargetUser = new User(_userSelected.Id, _userSelected.Username, 
                _userSelected.Password, _userSelected.Сreate_Date, 
                _userSelected.FullName, _userSelected.Email,
                _departamentControl.GetIddByName(_userSelected.Departament),
                _roleControl.GetIddByName(_userSelected.Role),
                _sectorControl.GetIddByName(_userSelected.Sector),
                _groupControl.GetIddByName(_userSelected.Group));
            
            UserEditWindow userEditWindow = new UserEditWindow();

            if (userEditWindow.ShowDialog() == true)
            {
                if (NewUser.id != 0)
                {
                    if (NewUser.password.Length > 0)
                    {
                        string hashPassword = PasswordHasher.Hash(NewUser.password);
                        NewUser.password = hashPassword;
                        _userControl.UpdateCurrentUserWithPassword(NewUser);
                    }
                    else
                    {
                        _userControl.UpdateCurrentUserWithOutPassword(NewUser);
                    }

                    ListOfUsers = new List<User>();
                    ListOfUsers = _userControl.GetAllUsersList();
                    Users = new List<ItemUser>();
                    foreach (User user in ListOfUsers)
                    {
                        Users.Add(new ItemUser(user));
                    } 
                }
            }
            
        }, () => _userSelected != null);

        /// <summary>
        /// Команда для открытия окна создания нового пользователя
        /// </summary>
        public ICommand OpenCreateNewUserWindow => new DelegateCommand(() =>
        {
            TargetUser = null;
            UserEditWindow userEditWindow = new UserEditWindow();
           // MessageService.Send(0);
           // MessageService.Bus += Receive;

            if (userEditWindow.ShowDialog() == true)
            {
                if (NewUser.id == 0)
                {
                    string hashPassword = PasswordHasher.Hash(NewUser.password);
                    NewUser.password = hashPassword;

                    _userControl.InsertNewUser(NewUser);
                    ListOfUsers = new List<User>();
                    ListOfUsers = _userControl.GetAllUsersList();
                    Users = new List<ItemUser>();
                    foreach (User user in ListOfUsers)
                    {
                        Users.Add(new ItemUser(user));
                    } 
                }
            }
            
        });

        /// <summary>
        /// Команда для удаления выбранного пользователя
        /// </summary>
        public ICommand DeleteSelectedUser => new DelegateCommand(() =>
        {
            if (SessionUser.user.id != _userSelected.Id)
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить пользователя?", 
                    "Удаление пользователя", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    _userControl.DeleteCurrentUser(_userSelected.Id);
                    ListOfUsers = new List<User>();
                    ListOfUsers = _userControl.GetAllUsersList();
                    Users = new List<ItemUser>();
                    foreach (User user in ListOfUsers)
                    {
                        Users.Add(new ItemUser(user));
                    }
                }
            }
            else
            {
                MessageBox.Show("Вы не можете удалить пользователя текущей сессии!","Ошибка");
            }
        }, () => _userSelected != null);

        /// <summary>
        /// Команда для открытия окна создания нового отдела
        /// </summary>
        public ICommand OpenCreateNewDepartamentWindow => new DelegateCommand(() =>
        {
            DepartamentEditWindow departamentEditWindow = new DepartamentEditWindow();
            

            if (departamentEditWindow.ShowDialog() == true)
            {
                if (NewDepartament.id == 0)
                {
                    _departamentControl.InsertNewDepartament(NewDepartament);
                    ListDepartaments = new List<Departament>();
                    ListDepartaments = _departamentControl.GetAllDepartamentsList();                    
                }
            }
            
        });

        /// <summary>
        /// Командя для открытия окна редактирования отдела
        /// </summary>
        public ICommand OpenEditSelectedDepartamentWindow => new DelegateCommand(() =>
        {
            DepartamentEditWindow departamentEditWindow = new DepartamentEditWindow();
            

            if (departamentEditWindow.ShowDialog() == true)
            {
                if (NewDepartament.id != 0)
                {
                    _departamentControl.UpdateCurrentDepartament(NewDepartament);
                    ListDepartaments = new List<Departament>();
                    ListDepartaments = _departamentControl.GetAllDepartamentsList();
                }
            }
            
        }, () => _departamentSelected != null);

        /// <summary>
        /// Команда для удаления выбранного отдела
        /// </summary>
        public ICommand DeleteSelectedDepartament => new DelegateCommand(() =>
        {
                if (MessageBox.Show("Вы уверены, что хотите удалить отдел?", "Удаление отдела", 
                    MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                _departamentControl.DeleteCurrentDepartament(_departamentSelected.id);
                    ListDepartaments = new List<Departament>();
                    ListDepartaments = _departamentControl.GetAllDepartamentsList();
                 }
        }, () => _departamentSelected != null);

        public ICommand OpenCreateNewRoleWindow => new DelegateCommand(() =>
        {
            RoleEditWindow roleEditWindow = new RoleEditWindow();
           

            if (roleEditWindow.ShowDialog() == true)
            {
                if (NewRole.id == 0)
                {
                    _roleControl.InsertNewRole(NewRole);
                    ListRoles = new List<Role>();
                    ListRoles = _roleControl.GetAllRolesList();
                }
            }
            
        });

        public ICommand OpenEditSelectedRoleWindow => new DelegateCommand(() =>
        {
            RoleEditWindow roleEditWindow = new RoleEditWindow();
            

            if (roleEditWindow.ShowDialog() == true)
            {
                if (NewRole.id != 0)
                {
                    _roleControl.UpdateCurrentRole(NewRole);
                    ListRoles = new List<Role>();
                    ListRoles = _roleControl.GetAllRolesList();
                }
            }
            
        }, () => _roleSelected != null);

        public ICommand DeleteSelectedRole=> new DelegateCommand(() =>
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить роль?", "Удаление роли", 
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                _roleControl.DeleteCurrentRole(_roleSelected.id);
                ListRoles = new List<Role>();
                ListRoles = _roleControl.GetAllRolesList();
            }
        }, () => _roleSelected != null);
    }
}
