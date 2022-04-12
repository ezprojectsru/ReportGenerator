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
    /// Класс управления Типом (работа с БД)
    /// </summary>
    public class TaskTypeControl
    {
        private readonly DbConnection _db = new DbConnection();
        
        /// <summary>
        /// Возвращает список Типов по id Отдела, к которому они относятся
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<TaskType> GetTaskTypeListByDepartamentId(int id)
        {
            List<TaskType> taskTypes = new List<TaskType>();
            try
            {
                SqlConnection connection = _db.GetConnection();
                taskTypes = connection
                    .Query<TaskType>("SELECT * FROM types WHERE departamentId = @id", new {id}).ToList();
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return taskTypes;
        }

        /// <summary>
        /// Возвращает список коротких названий типов по id Отдела, к которому они относятся
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<string> GetTaskShortNamesListByDepartamentId(int id)
        {

            List<string> typeShortNames = new List<string>();
            try
            {
                SqlConnection connection = _db.GetConnection();
                List<TaskType> taskTypes = connection
                    .Query<TaskType>("SELECT * FROM types WHERE departamentId = @id", new {id}).ToList();
                connection.Dispose();
                foreach (TaskType task in taskTypes)
                {
                    typeShortNames.Add(task.shortName);
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return typeShortNames;
        }

        public string GetShortNameById(int id)
        {
            string shortName = "";
            try
            {
                SqlConnection connection = _db.GetConnection();
                TaskType taskType = connection.Query<TaskType>("SELECT shortName FROM types WHERE id = @id", new {id})
                    .FirstOrDefault();
                shortName = taskType.shortName;
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return shortName;
        }

        public TaskType GetTaskTypeById(int id)
        {
            TaskType tt = new TaskType();
            try
            {
                SqlConnection connection = _db.GetConnection();
                tt = connection.Query<TaskType>("SELECT * FROM types WHERE id = @id", new { id })
                    .FirstOrDefault();
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return tt;
        }

        public int GetIdByShortName(string shortName)
        {
            int taskTypeId = 0;
            try
            {
                SqlConnection connection = _db.GetConnection();
                TaskType taskType = connection
                    .Query<TaskType>("SELECT id FROM types WHERE shortName = @shortName", new {shortName})
                    .FirstOrDefault();
                taskTypeId = taskType.id;
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return taskTypeId;
        }
    }
}
