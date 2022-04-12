using ReportGenerator.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using Dapper;
using ReportGenerator.DataBase.Models;

namespace ReportGenerator.DataBase.Controls
{
    public class WorkProjectControl
    {
        private readonly DbConnection _db = new DbConnection();

        public string GetNameById(int id)
        {
            if (id == 0) return "";
            string name = "";
            try
            {
                
                SqlConnection connection = _db.GetConnection();
                WorkProject wproject = connection.Query<WorkProject>("Select name From projects WHERE id = @id", new { id }).FirstOrDefault();
                name = wproject.Name;
                connection.Dispose();

            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return name;
        }

        public int GetIdByName(string name)
        {
            if (name == "") return 0;
            int id = 0;
            try
            {

                SqlConnection connection = _db.GetConnection();
                WorkProject wproject = connection.Query<WorkProject>("Select id From projects WHERE name = @name", new { name }).FirstOrDefault();
                id = wproject.Id;
                connection.Dispose();

            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return id;
        }

        public List<WorkProject> GetAllProjects()
        {
            List<WorkProject> wp = new List<WorkProject>();
            try
            {

                SqlConnection connection = _db.GetConnection();
                wp = connection.Query<WorkProject>("Select * From projects").ToList();
                connection.Dispose();

            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return wp;
        }
    }
}
