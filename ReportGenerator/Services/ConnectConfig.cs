
namespace ReportGenerator.Services
{
    /// <summary>
    /// Класс для сериализации настроек подключения к серверу
    /// </summary>
    public class ConnectConfig
    {
        public string Server { get; set; }
        public string DbName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }        
    }
}
