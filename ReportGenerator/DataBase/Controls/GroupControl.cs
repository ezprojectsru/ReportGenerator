using Dapper;
using ReportGenerator.DataBase.Models;
using ReportGenerator.Services;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;


namespace ReportGenerator.DataBase.Controls
{    
    /// <summary>
    /// Класс управления Группы (работа с БД)
    /// </summary>
    public class GroupControl
    {
        private SqlConnection _connection;
        public GroupControl()
        {
            DbConnection db = new DbConnection();
            _connection = db.GetConnection();
        }
        /// <summary>
        /// Возвращает имя группы по id группы
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetNameById(int id)
            {
                Group group = _connection.Query<Group>("SELECT name FROM groups WHERE id = @id", new { id }).FirstOrDefault();                
                return group.name;
            }

        /// <summary>
        /// Возвращает список имен всех групп
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllNameGroups()
            {
                List<Group> groups = _connection.Query<Group>("Select name From groups").ToList();                
                List<string> groupNames = new List<string>();
                foreach (Group group in groups)
                {
                    groupNames.Add(group.name);
                }
                return groupNames;
            }

        /// <summary>
        /// Возвращает id группы по имени группы
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetIddByName(string name)
        {
            Group group = _connection.Query<Group>("SELECT id FROM groups WHERE name = @name", new { name }).FirstOrDefault();            
            return group.id;
        }
    }    
}
