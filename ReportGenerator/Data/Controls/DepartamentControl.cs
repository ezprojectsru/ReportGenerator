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
    public class DepartamentControl
    {
        private readonly DbConnection _db = new DbConnection();

        public List<Departament> GetAllDepartamentsList()
        {
            List<Departament> departaments = new List<Departament>();
            try
            {
                SqlConnection connection = _db.GetConnection();
                departaments = connection.Query<Departament>("Select * From users").ToList();
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return departaments;
        }

        public string GetNameById(int id)
        {
            string name = "";
            try
            {
                SqlConnection connection = _db.GetConnection();
                Departament departament = connection
                    .Query<Departament>("SELECT name FROM departaments WHERE id = @id", new { id }).FirstOrDefault();
                name = departament.Name;
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return name;
        }

        public Departament GetDepartamentById(int id)
        {
            Departament departament = new Departament();
            try
            {
                SqlConnection connection = _db.GetConnection();
                departament = connection
                    .Query<Departament>("SELECT * FROM departaments WHERE id = @id", new { id }).FirstOrDefault();
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return departament;
        }
    }
}
