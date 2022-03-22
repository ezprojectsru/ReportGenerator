using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace ReportGenerator.Services
{
    /// <summary>
    /// Класс соединения с базой данных
    /// </summary>
    public class DbConnection
    {

        public SqlConnection Connection { get; set; }        
        public DbConnection()
        {
            if (!File.Exists("settings.json"))
            {
                ConnectConfig config = new ConnectConfig()
                {
                    Server = "",
                    DbName = "",
                    Username = "",
                    Password = ""
                };
                string configJson = JsonSerializer.Serialize(config, typeof(ConnectConfig));
                StreamWriter file = File.CreateText("settings.json");
                file.WriteLine(configJson);
                file.Close();
                
            }
                try
                {
                    string data = File.ReadAllText("settings.json");
                    ConnectConfig connectConfig = JsonSerializer.Deserialize<ConnectConfig>(data);

                    var builder = new SqlConnectionStringBuilder();
                    builder.DataSource = connectConfig.Server;
                    builder.InitialCatalog = connectConfig.DbName;
                    builder.UserID = connectConfig.Username;
                    builder.Password = connectConfig.Password;
                    Connection = new SqlConnection(builder.ToString());                    
                    
                }
                catch 
                {
                    
                }                       
        }

        /// <summary>
        /// Проверка есть ли соединение
        /// </summary>
        /// <returns></returns>
        public bool CheckConnect()
        {
            bool result = true;
            try
            {
                Connection.Open();                
            }
            catch (Exception ex)
            {
                result = false;
            }

            if (Connection != null && Connection.State != ConnectionState.Closed)
            {
                Connection.Close();
            }
            return result;
        }
        

        /// <summary>
        /// Возвращает созданное соединение
        /// </summary>        
        public SqlConnection GetConnection()
        {
            return Connection;
        }

        

        



    }
}
