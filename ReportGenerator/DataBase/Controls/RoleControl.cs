using System;
using Dapper;
using ReportGenerator.DataBase.Models;
using ReportGenerator.Services;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;


namespace ReportGenerator.DataBase.Controls
{
    /// <summary>
    /// Класс управления Ролью (работа с БД)
    /// </summary>
    public class RoleControl
    {
        private readonly DbConnection _db = new DbConnection();
        
        /// <summary>
        /// Возвращает название роли по ее id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetNameById(int id)
        {
            string name = "";
            try
            {
                SqlConnection connection = _db.GetConnection();
                Role role = connection.Query<Role>("SELECT name FROM roles WHERE id = @id", new {id}).FirstOrDefault();
                connection.Dispose();
                name = role.name;
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return name;
        }

        /// <summary>
        /// Возвращает список названий всех Ролей
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllNameRoles()
        {
            List<string> roleNames = new List<string>();
            try
            {
                SqlConnection connection = _db.GetConnection();
                List<Role> roles = connection.Query<Role>("Select name From roles").ToList();
                connection.Dispose();
                foreach (Role role in roles)
                {
                    roleNames.Add(role.name);
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
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
            int roleId = 0;
            try
            {
                SqlConnection connection = _db.GetConnection();
                Role role = connection.Query<Role>("SELECT id FROM roles WHERE name = @name", new {name})
                    .FirstOrDefault();
                connection.Dispose();
                roleId = role.id;
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return roleId;
        }

        public List<Role> GetAllRolesList()
        {
            List<Role> roles = new List<Role>();
            try
            {
                SqlConnection connection = _db.GetConnection();
                roles = connection.Query<Role>("Select * From roles").ToList();
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return roles;
        }

        public void InsertNewRole(Role role)
        {
            try
            {
                SqlConnection connection = _db.GetConnection();
                string insertQuery = "INSERT INTO roles (name) VALUES (@name)";
                var result = connection.Execute(insertQuery, role);
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }
        }

        public void UpdateCurrentRole(Role role)
        {
            try
            {
                SqlConnection connection = _db.GetConnection();
                string updatetQuery = "UPDATE roles SET name = @name WHERE id = @id";
                var result = connection.Execute(updatetQuery, role);
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }
        }

        public void DeleteCurrentRole(int id)
        {
            try
            {
                SqlConnection connection = _db.GetConnection();
                string deleteQuery = "DELETE FROM roles WHERE id = @id";
                var result = connection.Execute(deleteQuery, new
                {
                    id
                });
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }
        }
    }
}
