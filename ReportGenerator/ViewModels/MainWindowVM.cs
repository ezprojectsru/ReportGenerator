using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using ReportGenerator.Data.Controls;
using ReportGenerator.Data.Models;
using ReportGenerator.Services;
using ReportGenerator.ViewModels.Base;
using ReportGenerator.Views.Dialogs;
using ReportGenerator.Views.Windows;

namespace ReportGenerator.ViewModels
{
    public class MainWindowVM: ViewModel
    {
        #region Авторизация
        private List<User> _users = new List<User>();
        private UserControl _userControl = new UserControl();
        private User _user = new User();
        public User User
        {
            get => _user;
            set => Set(ref _user, value);
        } 
        #endregion

        #region Проверка подключения к БД
        private string _connectionStatus = "Проверка подключения, подождите...";
        public string ConnectionStatus
        {
            get => _connectionStatus;
            set => Set(ref _connectionStatus, value);
        }

        private Thread _thread;
        private CancellationTokenSource _tokenSource;
        private bool isConnected = false;
        #endregion

        #region COMMANDS
        #region CheckConnectionCmd
        public ICommand CheckConnectionCmd { get; }
        private bool CanCheckConnectionCmdExecute(object p) => _thread == null;

        private void OnCheckConnectionCmdExecuted(object p)
        {
            CheckConnection();
        }
        #endregion

        #region OpenConnectionSettingsWindowCmd
        public ICommand OpenConnectionSettingsWindowCmd { get; }
        private bool CanOpenConnectionSettingsWindowCmdExecute(object p) => true;

        private void OnOpenConnectionSettingsWindowCmdExecuted(object p)
        {
            OpenConnectionSettingsWindow();
        }
        #endregion

        #region Authorization
        public ICommand AuthorizationCmd { get; }
        private bool CanAuthorizationCmdExecute(object p) => _users != null && !string.IsNullOrWhiteSpace(User.Username) && !string.IsNullOrWhiteSpace(User.Password);

        private void OnAuthorizationCmdExecuted(object p)
        {
            Authorization();
        }
        #endregion


        #endregion

        public MainWindowVM()
        {
            #region INIT COMMANDS
            CheckConnectionCmd = new LambdaCommand(OnCheckConnectionCmdExecuted, CanCheckConnectionCmdExecute);
            OpenConnectionSettingsWindowCmd = new LambdaCommand(OnOpenConnectionSettingsWindowCmdExecuted, CanOpenConnectionSettingsWindowCmdExecute);
            AuthorizationCmd = new LambdaCommand(OnAuthorizationCmdExecuted, CanAuthorizationCmdExecute); 
            #endregion
        }
        private void Worker(object state)
        {
            DbConnection db = new DbConnection();
            isConnected = db.CheckConnect();
            ConnectionStatus = db.CheckConnect() ? "" : "Подключение к Серверу отсутствует!";

            _users = isConnected ? _userControl.GetAllUsersList() : null;
        }

        #region COMMAND'S MOTHODS
        private void CheckConnection()
        {
            _tokenSource = new CancellationTokenSource();
            _thread = new Thread(Worker) { IsBackground = true };
            _thread.Start(_tokenSource.Token);
        }

        private void OpenConnectionSettingsWindow()
        {
            ConnectionSettingsWindowVM vm = ConnectionSettingsWindowVM.Create();
            ConnectionSettingsWindow dialogWindow = new ConnectionSettingsWindow(vm);
            if (dialogWindow.ShowDialog() == true)
            {
                isConnected = false;
                ConnectionStatus = "Проверка подключения, подождите...";
                CheckConnection();
            }
        }

        private void Authorization()
        {
            int selectPos = _users.FindIndex(x => x.Username == User.Username);

            if (selectPos >= 0)
            {
                bool correctPassword = false;

                if (PasswordHasher.IsHashSupported(_users[selectPos].Password))
                {
                    correctPassword = PasswordHasher.Verify(User.Password, _users[selectPos].Password);
                }

                if (correctPassword)
                {
                    WrapperWindowVM vm = WrapperWindowVM.Create(_users[selectPos]);
                    WrapperWindow wrapperWindow = new WrapperWindow(vm);
                    wrapperWindow.Show();
                    Application.Current.MainWindow.Close();
                }
                else
                {
                    MessageBox.Show("Вы ввели неправильный Логин или Пароль!");
                }

            }
            else
            {
                MessageBox.Show("Вы ввели неправильный Логин или Пароль!");
            }
        } 
        #endregion
    }
}
