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
    /// Класс управления Задачей (работа с БД)
    /// </summary>
    public class TaskControl
    {
        private readonly DbConnection _db = new DbConnection();
        
        /// <summary>
        /// Возвращает список задач по id Плана, к которому они относятся
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Task> GetTaskListByPlanId(int id)
        {
            List<Task> tasks = new List<Task>();
            try
            {
                SqlConnection connection = _db.GetConnection();
                tasks = connection.Query<Task>("SELECT * FROM tasks WHERE planId = @id", new {id}).ToList();
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return tasks;
        }

        /// <summary>
        /// Обновление текущей задачи
        /// </summary>
        /// <param name="task"></param>
        public void UpdateCurrentTask(Task task)
        {
            try
            {
                SqlConnection connection = _db.GetConnection();
                string updatetQuery =
                    "UPDATE tasks SET name = @name, priority = @priority, typeId = @typeId, intensity = @intensity, startCompletion = @startCompletion, planCompletion = @planCompletion, comment = @comment WHERE id = @id";
                var result = connection.Execute(updatetQuery, task);
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }
        }

        /// <summary>
        /// Добавление новой задачи
        /// </summary>
        /// <param name="task"></param>
        public void InsertNewTask(Task task)
        {
            try
            {
                SqlConnection connection = _db.GetConnection();
                string insertQuery =
                    "INSERT INTO tasks (name, planId, priority, typeId, intensity, startCompletion, planCompletion, comment) VALUES (@name, @planId, @priority, @typeId, @intensity, @startCompletion, @planCompletion, @comment)";
                var result = connection.Execute(insertQuery, task);
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }
        }

        /// <summary>
        /// Удаление текущей задачи
        /// </summary>
        /// <param name="task"></param>
        public void DeleteCurrentTask(Task task)
        {
            try
            {
                SqlConnection connection = _db.GetConnection();
                string deleteQuery = "DELETE FROM tasks WHERE id = @id";
                var result = connection.Execute(deleteQuery, new
                {
                    task.id
                });
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

        }

        /// <summary>
        /// Удаление всех задач, относящихся к Плану по его id
        /// </summary>
        /// <param name="id"></param>
        public void DeleteTasksByPlanId(int id)
        {
            try
            {
                SqlConnection connection = _db.GetConnection();
                string deleteQuery = "DELETE FROM tasks WHERE planId = @id";
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
