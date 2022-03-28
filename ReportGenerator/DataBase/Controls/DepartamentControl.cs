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
    /// Класс управления Отделом (работа с БД)
    /// </summary>
    public class DepartamentControl
    {
        private readonly DbConnection _db = new DbConnection();
       
        public string GetNameById(int id)
        {
            string name = "";
            try
            {
                SqlConnection connection = _db.GetConnection();
                Departament departament = connection
                    .Query<Departament>("SELECT name FROM departaments WHERE id = @id", new {id}).FirstOrDefault();
                name = departament.name;
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return name;
        }

        public List<string> GetAllNameDepartaments()
        {
            List<string> departamentNames = new List<string>();
            try
            {
                SqlConnection connection = _db.GetConnection();
                List<Departament> departaments =
                    connection.Query<Departament>("Select name From departaments").ToList();
                connection.Dispose();

                foreach (Departament departament in departaments)
                {
                    departamentNames.Add(departament.name);
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return departamentNames;
        }

        public int GetIddByName(string name)
        {
            int departamentId = 0;
            try
            {
                SqlConnection connection = _db.GetConnection();
                Departament departament = connection
                    .Query<Departament>("SELECT id FROM departaments WHERE name = @name", new {name}).FirstOrDefault();
                departamentId = departament.id;
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return departamentId;
        }

        public List<Departament> GetAllDepartamentsList()
        {
            List<Departament> departaments = new List<Departament>();
            try
            {
                SqlConnection connection = _db.GetConnection();
                departaments = connection.Query<Departament>("Select * From departaments").ToList();
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }
            return departaments;
        }

        /// <summary>
        /// Добавить новый отдел
        /// </summary>
        /// <param name="departament"></param>
        public void InsertNewDepartament(Departament departament)
        {
            try
            {
                SqlConnection connection = _db.GetConnection();
                string insertQuery =
                    "INSERT INTO departaments (name, shortName, description) VALUES (@name, @shortName, @description)";
                var result = connection.Execute(insertQuery, departament);
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }
        }

        /// <summary>
        /// Обновить текущий отдел
        /// </summary>
        /// <param name="departament"></param>
        public void UpdateCurrentDepartament(Departament departament)
        {
            try
            {
                SqlConnection connection = _db.GetConnection();
                string updatetQuery =
                    "UPDATE departaments SET name = @name, shortName = @shortName, description = @description WHERE id = @id";
                var result = connection.Execute(updatetQuery, departament);
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }
        }

        /// <summary>
        /// Удалить текущий отдел
        /// </summary>
        /// <param name="id"></param>
        public void DeleteCurrentDepartament(int id)
        {
            try
            {
                SqlConnection connection = _db.GetConnection();
                string deleteQuery = "DELETE FROM departaments WHERE id = @id";
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
