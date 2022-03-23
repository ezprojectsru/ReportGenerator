using ReportGenerator.DataBase.Controls;
using System;


namespace ReportGenerator.DataBase.Models
{
    /// <summary>
    /// Вспомогательный клас для конвертации полей с ID в названия полей. Будет упразнен после написания конвертора.
    /// </summary>
    public class ItemUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime Сreate_Date { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Departament { get; set; }
        public string Role { get; set; }
        public string Sector { get; set; }
        public string Group { get; set; }

        private DepartamentControl _departamentControl = new DepartamentControl();
        private GroupControl _groupControl = new GroupControl();
        private RoleControl _roleControl = new RoleControl();
        private SectorControl _sectorControl = new SectorControl();

        public ItemUser()
        {
        }

        public ItemUser(User user)
        {
            Id = user.id;
            Username = user.username;
            Password = user.password;
            Сreate_Date = user.create_date;
            FullName = user.fullName;
            Email = user.email;

            Departament = _departamentControl.GetNameById(user.departamentId);
            Role = _roleControl.GetNameById(user.roleId);
            Sector = _sectorControl.GetNameById(user.sectorId);
            Group = _groupControl.GetNameById(user.groupId);
        }
    }
}
