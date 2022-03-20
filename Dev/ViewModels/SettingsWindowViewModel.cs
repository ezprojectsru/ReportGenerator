using DevExpress.Mvvm;
using ReportGenerator.Services;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;

namespace ReportGenerator.ViewModels
{
    public class SettingsWindowViewModel : BindableBase
    {
        /// <summary>
        /// Класс ViewModels для окна с настройка подключения к серверу
        /// </summary>
        public ConnectConfig Config { get; set; }
        public string Title { get; set; }


        public SettingsWindowViewModel()
        {
            try
            {
                Title = "Настройки подключения";
                string data = File.ReadAllText("settings.json");
                Config = JsonSerializer.Deserialize<ConnectConfig>(data);
            }
            catch
            {
                Config.Server = "";
                Config.DbName = "";
                Config.Username = "";
                Config.Password = "";
            }
            
        }

        /// <summary>
        /// Команда для созранения настроек
        /// </summary>
        public ICommand SaveSettings => new DelegateCommand<object>((currentWindow) =>
        {
            ConnectConfig config = new ConnectConfig()
            {
                Server = Config.Server,
                DbName = Config.DbName,
                Username = Config.Username,
                Password = Config.Password
            };
            string configJson = JsonSerializer.Serialize(config, typeof(ConnectConfig));
            StreamWriter file = File.CreateText("settings.json");
            file.WriteLine(configJson);
            file.Close();

            Window wnd = currentWindow as Window;
            wnd.DialogResult = true;
            wnd.Close();



        });

        

    }
}
