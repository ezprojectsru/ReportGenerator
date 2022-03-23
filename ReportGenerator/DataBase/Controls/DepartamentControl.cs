using Dapper;
using ReportGenerator.DataBase.Models;
using ReportGenerator.Services;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;


namespace ReportGenerator.DataBase.Controls
{
    /// <summary>
    /// Класс управления Отделом (работа с БД)
    /// </summary>
    public class DepartamentControl
    {
        private SqlConnection _connection;
        public DepartamentControl()
        {
            DbConnection db = new DbConnection();
             _connection = db.GetConnection();
        }
        public string GetNameById(int id)
        {            
            Departament departament = _connection.Query<Departament>("SELECT name FROM departaments WHERE id = @id", new { id }).FirstOrDefault();            
            return departament.name;
        }

        public List<string> GetAllNameDepartaments()
        {

            List<Departament> departaments = _connection.Query<Departament>("Select name From departaments").ToList();            
            List<string> departamentNames = new List<string>();
            foreach (Departament departament in departaments)
            {
                departamentNames.Add(departament.name);
            }
            return departamentNames;
        }

        public int GetIddByName(string name)
        {

            Departament departament = _connection.Query<Departament>("SELECT id FROM departaments WHERE name = @name", new { name }).FirstOrDefault();            
            return departament.id;
        }

        public List<Departament> GetAllDepartamentsList()
        {
            List<Departament> departaments = _connection.Query<Departament>("Select * From departaments").ToList();            
            return departaments;
        }

        /// <summary>
        /// Добавить новый отдел
        /// </summary>
        /// <param name="departament"></param>
        public void InsertNewDepartament(Departament departament)
        {
            string insertQuery = "INSERT INTO departaments (name, shortName, description) VALUES (@name, @shortName, @description)";
            var result = _connection.Execute(insertQuery, departament);            
        }

        /// <summary>
        /// Обновить текущий отдел
        /// </summary>
        /// <param name="departament"></param>
        public void UpdateCurrentDepartament(Departament departament)
        {
            string updatetQuery = "UPDATE departaments SET name = @name, shortName = @shortName, description = @description WHERE id = @id";
            var result = _connection.Execute(updatetQuery, departament);            
        }

        /// <summary>
        /// Удалить текущий отдел
        /// </summary>
        /// <param name="id"></param>
        public void DeleteCurrentDepartament(int id)
        {
             string deleteQuery = "DELETE FROM departaments WHERE id = @id";
             var result = _connection.Execute(deleteQuery, new
                {
                    id
                });            
        }
    }
}
