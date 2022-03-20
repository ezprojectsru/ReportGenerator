using DevExpress.Mvvm;
using ReportGenerator.Services;
using System.Windows;
using System.Windows.Input;
using ReportGenerator.DataBase.Controls;
using ReportGenerator.DataBase.Models;
using ReportGenerator.Views;

using System.Data.SqlClient;

namespace ReportGenerator.ViewModels
{
    /// <summary>
    /// Класс ViewModel для страницы авторизации и проверки подключения к серверу
    /// </summary>
    public class AuthorizationViewModel : BindableBase
    {
        private bool isConnected = false;
        public string ConnectionStatus { get; set; } = "Подключение к Серверу отсутствует!";
        public string Login { get; set; }
        public string Password { get; set; }
        
        public AuthorizationViewModel()
        {
            Init();
        }

        /// <summary>
        /// Процедура инициализации. Проверка подключения к серверу
        /// </summary>
        private void Init()
        {
            DbConnection db = new DbConnection();
            SqlConnection conn = db.ConnectOpen();

            if (conn != null)
            {

                isConnected = true;
                ConnectionStatus = "";
                db.ConnectClose();
            }
            else
            {
                ConnectionStatus = "Подключение к Серверу отсутствует!";
            }
        }   

        /// <summary>
        /// Команда для проверки введеных Логина и Пароля. При успехе переадресовывает на основную страницу приложения.
        /// </summary>
        public ICommand LoginUser => new DelegateCommand(() =>
        {
            User user = UserControl.GetUser(Login);
            if (user != null)
            {
                if (Password == user.password)
                {                      

                    MainWindow mainWindow = new MainWindow();
                    SessionUser sessionUser = new SessionUser(user);
                    MessageService.Send(sessionUser);
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
                Init();
            }

        });

    }
}
