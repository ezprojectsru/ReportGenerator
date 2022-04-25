using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using DevExpress.Mvvm.POCO;
using ReportGenerator.Services;
using ReportGenerator.ViewModels.Base;
using Encoder = ReportGenerator.Services.Encoder;


namespace ReportGenerator.ViewModels
{
    public class ConnectionSettingsWindowVM : ViewModel
    {
        public ConnectConfig Config { get; set; }

        public ConnectionSettingsWindowVM()
        {
            SaveSettingsCmd = new LambdaCommand(OnSaveSettingsCmdExecuted, CanSaveSettingsCmdExecute);
            LoadData();
        }

        public static ConnectionSettingsWindowVM Create()
        {
            return ViewModelSource.Create(() => new ConnectionSettingsWindowVM());
        }

        #region SaveSettingsCmd
        public ICommand SaveSettingsCmd { get; }
        private bool CanSaveSettingsCmdExecute(object p) => true;

        private void OnSaveSettingsCmdExecuted(object p)
        {
            SaveSettings(p);
        }
        #endregion

        private void SaveSettings(object currentWindow)
        {
            ConnectConfig config = new ConnectConfig()
            {
                Server = Config.Server,
                DbName = Config.DbName,
                Username = Config.Username,
                Password = Encoder.GetData(Config.Password)
            };
            string configJson = JsonSerializer.Serialize(config, typeof(ConnectConfig));
            StreamWriter file = File.CreateText(Constants.SettingsFileName);
            file.WriteLine(configJson);
            file.Close();

            Window wnd = currentWindow as Window;
            wnd.DialogResult = true;
            wnd.Close();
        }

        private void LoadData()
        {
            try
            {
                string data = File.ReadAllText(Constants.SettingsFileName);
                Config = JsonSerializer.Deserialize<ConnectConfig>(data);
                Config.Password = Encoder.GetData(Config.Password);
            }
            catch
            {
                Config.Server = "";
                Config.DbName = "";
                Config.Username = "";
                Config.Password = "";
            }
        }

    }
}
