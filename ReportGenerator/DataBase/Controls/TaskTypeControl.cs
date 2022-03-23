﻿using Dapper;
using ReportGenerator.DataBase.Models;
using ReportGenerator.Services;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;


namespace ReportGenerator.DataBase.Controls
{
    /// <summary>
    /// Класс управления Типом (работа с БД)
    /// </summary>
    public class TaskTypeControl
    {
        private SqlConnection _connection;
        public TaskTypeControl()
        {
            DbConnection db = new DbConnection();
            _connection = db.GetConnection();
        }
        /// <summary>
        /// Возвращает список Типов по id Отдела, к которому они относятся
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<TaskType> GetTaskTypeListByDepartamentId(int id)
        {
            List<TaskType> taskTypes = new List<TaskType>();
            using (_connection)
            {
                taskTypes = _connection.Query<TaskType>("SELECT * FROM types WHERE departamentId = @id", new { id }).ToList();
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
            List<TaskType> taskTypes = new List<TaskType>();
            using (_connection)
            {
                taskTypes = _connection.Query<TaskType>("SELECT * FROM types WHERE departamentId = @id", new { id }).ToList();
            }
            foreach (TaskType task in taskTypes)
            {
                typeShortNames.Add(task.shortName);
            }
            return typeShortNames;
        }
    }
}
