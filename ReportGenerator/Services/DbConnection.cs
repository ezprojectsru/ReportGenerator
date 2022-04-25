using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.Json;

namespace ReportGenerator.Services
{
    public class DbConnection
    {
        private SqlConnection _connection { get; set; }

        private void Init()
        {
            _connection = null;

            if (!File.Exists(Constants.SettingsFileName))
            {
                ConnectConfig config = new ConnectConfig()
                {
                    Server = "",
                    DbName = "",
                    Username = "",
                    Password = ""
                };
                string configJson = JsonSerializer.Serialize(config, typeof(ConnectConfig));
                StreamWriter file = File.CreateText(Constants.SettingsFileName);
                file.WriteLine(configJson);
                file.Close();
            }

            try
            {
                string data = File.ReadAllText(Constants.SettingsFileName);
                ConnectConfig connectConfig = JsonSerializer.Deserialize<ConnectConfig>(data);

                var builder = new SqlConnectionStringBuilder();
                builder.DataSource = connectConfig.Server;
                builder.InitialCatalog = connectConfig.DbName;
                builder.UserID = connectConfig.Username;
                builder.Password = Encoder.GetData(connectConfig.Password);
                _connection = new SqlConnection(builder.ToString());
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }
        }

        /// <summary>
        /// Проверка есть ли соединение
        /// </summary>
        /// <returns></returns>
        public bool CheckConnect()
        {
            Init();
            if (_connection == null) return false;
            bool result = true;
            try
            {
                _connection.Open();
            }
            catch (Exception ex)
            {
                result = false;
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            if (_connection != null && _connection.State != ConnectionState.Closed)
            {
                _connection.Close();
            }
            return result;
        }


        /// <summary>
        /// Возвращает созданное соединение
        /// </summary>        
        public SqlConnection GetConnection()
        {
            Init();
            return _connection;
        }
    }
}
