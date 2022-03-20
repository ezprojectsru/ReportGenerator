
namespace ReportGenerator.DataBase.Models
{
    /// <summary>
    /// Класс модели Отдела
    /// </summary>
    public class Departament
    {
        public int id { get; set; }
        public string name { get; set; }
        public string shortName { get; set; }
        public string description { get; set; }

        public Departament()
        { }

        public Departament(int id, string name, string shortName, string description)
        {
            this.id = id;
            this.name = name;
            this.shortName = shortName;
            this.description = description;
        }
    }
}
