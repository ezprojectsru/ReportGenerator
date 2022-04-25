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
    public class UserControl
    {
        private readonly DbConnection _db = new DbConnection();

        public List<User> GetAllUsersList()
        {
            List<User> users = new List<User>();
            try
            {
                SqlConnection connection = _db.GetConnection();
                users = connection.Query<User>("Select * From users").ToList();
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return users;
        }
    }
}
