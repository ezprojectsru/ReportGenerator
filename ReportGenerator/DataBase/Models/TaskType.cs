

namespace ReportGenerator.DataBase.Models
{
    /// <summary>
    /// Класс модели Типа
    /// </summary>
    public class TaskType
    {
        public int id { get; set; }
        public string name { get; set; }
        public string shortName { get; set; }
        public int departamentId { get; set; }
        public string description { get; set; }
    }
}
