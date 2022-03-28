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
    /// Класс управления Группы (работа с БД)
    /// </summary>
    public class GroupControl
    {
        private readonly DbConnection _db = new DbConnection();
       
        /// <summary>
        /// Возвращает имя группы по id группы
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetNameById(int id)
        {
            string name = "";
            try
            {
                SqlConnection connection = _db.GetConnection();
                Group group = connection.Query<Group>("SELECT name FROM groups WHERE id = @id", new {id})
                    .FirstOrDefault();
                name = group.name;
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return name;
            }

        /// <summary>
        /// Возвращает список имен всех групп
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllNameGroups()
            {
                List<string> groupNames = new List<string>();
                try
                {
                    SqlConnection connection = _db.GetConnection();
                    List<Group> groups = connection.Query<Group>("Select name From groups").ToList();
                    connection.Dispose();
                    foreach (Group group in groups)
                    {
                        groupNames.Add(group.name);
                    }
                }
                catch (Exception ex)
                {
                    File.AppendAllText(Constants.LogFileName, ex.ToString());
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
            int groupId = 0;
            try
            {
                SqlConnection connection = _db.GetConnection();
                Group group = connection.Query<Group>("SELECT id FROM groups WHERE name = @name", new {name})
                    .FirstOrDefault();
                groupId = group.id;
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return groupId;
        }
    }    
}
