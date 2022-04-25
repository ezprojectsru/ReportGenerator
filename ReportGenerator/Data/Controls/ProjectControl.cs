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
    public class ProjectControl
    {
        private readonly DbConnection _db = new DbConnection();

        public List<Project> GetAllProjectListByServiceId(int id)
        {
            List<Project> projects = new List<Project>();
            try
            {
                SqlConnection connection = _db.GetConnection();
                projects = connection.Query<Project>("Select * From projects WHERE serviceId = @id", new { id }).ToList();
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return projects;
        }

        public List<Project> GetAllProjectList()
        {
            List<Project> projects = new List<Project>();
            try
            {
                SqlConnection connection = _db.GetConnection();
                projects = connection.Query<Project>("Select * From projects").ToList();
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return projects;
        }
    }
}
