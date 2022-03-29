using DevExpress.Mvvm;
using ReportGenerator.Services;
using System.Windows;
using System.Windows.Input;
using ReportGenerator.DataBase.Controls;
using ReportGenerator.DataBase.Models;
using ReportGenerator.Views;
using System.Threading;

namespace ReportGenerator.ViewModels
{
    /// <summary>
    /// Класс ViewModel для страницы авторизации и проверки подключения к серверу
    /// </summary>
    public class AuthorizationViewModel : BindableBase
    {
        private Thread _thread;
        private CancellationTokenSource _tokenSource;
        private bool isConnected = false;
        public string ConnectionStatus { get; set; } = "Проверка подключения к Серверу, подождите...";
        public string Login { get; set; }
        public string Password { get; set; }

        public SessionUser SessionUser = null;

        private UserControl _userControl = new UserControl();


        /// <summary>
        /// Инициализация потока для проверки подключения
        /// </summary>
        private void Init()
        {
            _tokenSource = new CancellationTokenSource();
            _thread = new Thread(Worker) { IsBackground = true };
            _thread.Start(_tokenSource.Token);
        }

        

        /// <summary>
        /// Команда для проверки введеных Логина и Пароля. При успехе переадресовывает на основную страницу приложения.
        /// </summary>
        public ICommand LoginUser => new DelegateCommand(() =>
        {
            User user = _userControl.GetUser(Login);
            if (user != null)
            {
                bool correctPassword = false;
                if (PasswordHasher.IsHashSupported(user.password))
                {
                    correctPassword = PasswordHasher.Verify(Password, user.password);
                }
                
                if (correctPassword)
                {
                    SessionUser = new SessionUser(user);
                    MainWindow mainWindow = new MainWindow();
                    // MessageService.Send(sessionUser);
                    mainWindow.Show();
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
            
            
        }, ()=> !string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(Password)&& isConnected);

        /// <summary>
        /// Команда для открытия окна настроек подключения к серверу
        /// </summary>
        public ICommand OpenSettingsWindow => new DelegateCommand(() =>
        {             

            SettingsWindow settingsWindow = new SettingsWindow();
            if (settingsWindow.ShowDialog() == true)
            {
                isConnected = false;
                ConnectionStatus = "Проверка подключения к Серверу, подождите...";
                Init();
            }

        });

        
        /// <summary>
        /// Команда для проверки подключения к серверу
        /// </summary>
        public ICommand CheckConnection => new DelegateCommand(() =>
        {
            _tokenSource = new CancellationTokenSource();
            _thread = new Thread(Worker) { IsBackground = true };
            _thread.Start(_tokenSource.Token);            

        }, () => _thread == null);

        private void Worker(object state)
        {
            DbConnection db = new DbConnection();

            if (db.CheckConnect())
            {
                isConnected = true;
                ConnectionStatus = "";
            }
            else
            {
                isConnected = false;
                ConnectionStatus = "Подключение к Серверу отсутствует!";
            }
        }
    }
}
