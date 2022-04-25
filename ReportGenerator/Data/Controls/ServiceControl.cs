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
    public class ServiceControl
    {
        private readonly DbConnection _db = new DbConnection();

        public List<Service> GetAllServicesList()
        {
            List<Service> services = new List<Service>();
            try
            {
                SqlConnection connection = _db.GetConnection();
                services = connection.Query<Service>("Select * From services").ToList();
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return services;
        }

        public List<Service> GetAllServicesListByDepartamentId(int id)
        {
            List<Service> services = new List<Service>();
            try
            {
                SqlConnection connection = _db.GetConnection();
                services = connection.Query<Service>("Select * From services WHERE departamentId = @id", new { id }).ToList();
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return services;
        }
    }
}
