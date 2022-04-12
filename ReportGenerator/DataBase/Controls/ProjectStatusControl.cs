using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using Dapper;
using ReportGenerator.DataBase.Models;
using ReportGenerator.Services;

namespace ReportGenerator.DataBase.Controls
{
    public class ProjectStatusControl
    {
        private readonly DbConnection _db = new DbConnection();

        public string GetNameById(int id)
        {
            string name = "";
            try
            {

                SqlConnection connection = _db.GetConnection();
                ProjectStatus ps = connection.Query<ProjectStatus>("Select name From projectStatus WHERE id = @id", new { id }).FirstOrDefault();
                connection.Dispose();
                name = ps.Name;

            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return name;
        }

        public List<ProjectStatus> GetAllProjectStatus()
        {
            List<ProjectStatus> pss = new List<ProjectStatus>();
            try
            {

                SqlConnection connection = _db.GetConnection();
                pss = connection.Query<ProjectStatus>("Select * From projectStatus").ToList();
                connection.Dispose();
                

            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return pss;
        }

        public int GetIdByName(string name)
        {
            int id = 0;
            try
            {
                SqlConnection connection = _db.GetConnection();
                ProjectStatus ps = connection
                    .Query<ProjectStatus>("SELECT id FROM projectStatus WHERE name = @name", new { name })
                    .FirstOrDefault();
                id = ps.Id;
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return id;
        }

        public ProjectStatus GetProjectStatusById(int id)
        {
            ProjectStatus ps = new ProjectStatus();
            try
            {
                SqlConnection connection = _db.GetConnection();
                ps = connection
                    .Query<ProjectStatus>("SELECT * FROM projectStatus WHERE id = @id", new { id })
                    .FirstOrDefault();
                
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return ps;
        }
    }
}
