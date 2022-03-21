using System;


namespace ReportGenerator.DataBase.Models
{
    /// <summary>
    /// Класс модели Пользователя
    /// </summary>
    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public DateTime create_date { get; set; }
        public string fullName { get; set; }
        public string email { get; set; }
        public int departamentId { get; set; }
        public int roleId { get; set; }
        public int sectorId { get; set; }
        public int groupId { get; set; }

        public User()
        {
        }

        public User(int id, string username, string password, DateTime create_date, string fullName, string email, int departamentId, int roleId, int sectorId, int groupId)
        {
            this.id = id;
            this.username = username;
            this.password = password;
            this.create_date = create_date;
            this.fullName = fullName;
            this.email = email;
            this.departamentId = departamentId;
            this.roleId = roleId;
            this.sectorId = sectorId;
            this.groupId = groupId;
        }
    }
}
