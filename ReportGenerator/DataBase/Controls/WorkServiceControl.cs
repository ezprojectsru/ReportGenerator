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
   public class WorkServiceControl
    {
        private readonly DbConnection _db = new DbConnection();
        private ProjectControl _projectControl = new ProjectControl();

        public List<WorkService> GetAllWorkServicesByDepartamentId(int id)
        {
            List<WorkService> services = new List<WorkService>();
            try
            {
                SqlConnection connection = _db.GetConnection();
                services = connection.Query<WorkService>("Select * From services WHERE departamentId = @id", new { id }).ToList();
                connection.Dispose();

                if (services.Count > 0)
                {
                    foreach (var s in services )
                    {
                        s.Projects = new List<Project>(_projectControl.GetAllProjectsByServiceId(s.ID));
                    }
                }
                
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return services;
        }

        public WorkService GetServiceById(int id)
        {
            WorkService ws = new WorkService();
            try
            {

                SqlConnection connection = _db.GetConnection();
                ws = connection.Query<WorkService>("Select * From services WHERE id = @id", new { id }).FirstOrDefault();
                connection.Dispose();

            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return ws;
        }
    }
}
