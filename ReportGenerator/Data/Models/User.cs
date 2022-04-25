using System;
using System.Collections.Generic;
using System.Text;

namespace ReportGenerator.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime CreateDate { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int DepartamentId { get; set; }
        public int RoleId { get; set; }
        public int SectorId { get; set; }
        public int GroupId { get; set; }

        public User()
        {
        }

        public User(int id, string username, string password, DateTime create_date, string fullName, string email, int departamentId, int roleId, int sectorId, int groupId)
        {
            Id = id;
            Username = username;
            Password = password;
            CreateDate = create_date;
            FullName = fullName;
            Email = email;
            DepartamentId = departamentId;
            RoleId = roleId;
            SectorId = sectorId;
            GroupId = groupId;
        }
    }
}
