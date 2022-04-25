using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using Dapper;
using ReportGenerator.Data.Models;
using ReportGenerator.Services;

namespace ReportGenerator.Data.Controls
{
    public class RoleControl
    {
        private readonly DbConnection _db = new DbConnection();

        public string GetNameById(int id)
        {
            string name = "";
            try
            {
                SqlConnection connection = _db.GetConnection();
                Role role = connection.Query<Role>("SELECT name FROM roles WHERE id = @id", new { id }).FirstOrDefault();
                connection.Dispose();
                name = role.Name;
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return name;
        }

        public Role GetRoleById(int id)
        {
            Role role = new Role();
            try
            {
                SqlConnection connection = _db.GetConnection();
                role = connection.Query<Role>("SELECT * FROM roles WHERE id = @id", new { id }).FirstOrDefault();
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return role;
        }
    }
}
