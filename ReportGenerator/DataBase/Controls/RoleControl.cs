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
        /// <summary>
        /// Возвращает название роли по ее id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetNameById(int id)
        {
            Role role = null;
            DbConnection db = new DbConnection();
            SqlConnection conn = db.GetConnection();

            using (conn)
            {
                role = conn.Query<Role>("SELECT name FROM roles WHERE id = @id", new { id }).FirstOrDefault();
            }

            return role.name;
        }

        /// <summary>
        /// Возвращает список названий всех Ролей
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAllNameRoles()
        {
            List<Role> roles = new List<Role>();
            DbConnection db = new DbConnection();
            SqlConnection conn = db.GetConnection();

            using (conn)
            {
                roles = conn.Query<Role>("Select name From roles").ToList();
            }

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
        public static int GetIddByName(string name)
        {
            Role role = new Role();
            DbConnection db = new DbConnection();
            SqlConnection conn = db.GetConnection();

            using (conn)
            {
                role = conn.Query<Role>("SELECT id FROM roles WHERE name = @name", new { name }).FirstOrDefault();
            }

            return role.id;
        }

        public static List<Role> GetAllRolesList()
        {

            List<Role> roles = new List<Role>();
            DbConnection db = new DbConnection();
            SqlConnection conn = db.GetConnection();

            using (conn)
            {
                roles = conn.Query<Role>("Select * From roles").ToList();
            }

            return roles;
        }

        public static void InsertNewRole(Role role)
        {
            DbConnection db = new DbConnection();
            SqlConnection conn = db.GetConnection();

            using (conn)
            {
                string insertQuery = "INSERT INTO roles (name) VALUES (@name)";
                var result = conn.Execute(insertQuery, role);
            }
        }

        public static void UpdateCurrentRole(Role role)
        {
            DbConnection db = new DbConnection();
            SqlConnection conn = db.GetConnection();

            using (conn)
            {
                string updatetQuery = "UPDATE roles SET name = @name WHERE id = @id";
                var result = conn.Execute(updatetQuery, role);
            }
        }

        public static void DeleteCurrentRole(int id)
        {
            DbConnection db = new DbConnection();
            SqlConnection conn = db.GetConnection();

            using (conn)
            {
                string deleteQuery = "DELETE FROM roles WHERE id = @id";
                var result = conn.Execute(deleteQuery, new
                {
                    id
                });
            }
        }
    }
}
