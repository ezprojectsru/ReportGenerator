using Dapper;
using ReportGenerator.DataBase.Models;
using ReportGenerator.Services;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;


namespace ReportGenerator.DataBase.Controls
{
    /// <summary>
    /// Класс управления Ролью (работа с БД)
    /// </summary>
    public class RoleControl
    {
        private SqlConnection _connection;
        public RoleControl()
        {
            DbConnection db = new DbConnection();
            _connection = db.GetConnection();
        }
        /// <summary>
        /// Возвращает название роли по ее id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetNameById(int id)
        {
            Role role = _connection.Query<Role>("SELECT name FROM roles WHERE id = @id", new { id }).FirstOrDefault();            
            return role.name;
        }

        /// <summary>
        /// Возвращает список названий всех Ролей
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllNameRoles()
        {
            List<Role> roles = _connection.Query<Role>("Select name From roles").ToList();            
            List<string> roleNames = new List<string>();
            foreach (Role role in roles)
            {
                roleNames.Add(role.name);
            }
            return roleNames;
        }

        /// <summary>
        /// Возвращает id роли по ее названию
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetIddByName(string name)
        {
            Role role = _connection.Query<Role>("SELECT id FROM roles WHERE name = @name", new { name }).FirstOrDefault();            
            return role.id;
        }

        public List<Role> GetAllRolesList()
        {

            List<Role> roles = _connection.Query<Role>("Select * From roles").ToList();            
            return roles;
        }

        public void InsertNewRole(Role role)
        {
            string insertQuery = "INSERT INTO roles (name) VALUES (@name)";
            var result = _connection.Execute(insertQuery, role);            
        }

        public void UpdateCurrentRole(Role role)
        {            
            string updatetQuery = "UPDATE roles SET name = @name WHERE id = @id";
            var result = _connection.Execute(updatetQuery, role);            
        }

        public void DeleteCurrentRole(int id)
        {
            string deleteQuery = "DELETE FROM roles WHERE id = @id";
                var result = _connection.Execute(deleteQuery, new
                {
                    id
                });            
        }
    }
}
