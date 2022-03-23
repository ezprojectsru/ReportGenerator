using Dapper;
using ReportGenerator.DataBase.Models;
using ReportGenerator.Services;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;


namespace ReportGenerator.DataBase.Controls
{
    /// <summary>
    /// Класс управления Задачей (работа с БД)
    /// </summary>
    public class TaskControl
    {
        private SqlConnection _connection;
        public TaskControl()
        {
            DbConnection db = new DbConnection();
            _connection = db.GetConnection();
        }
        /// <summary>
        /// Возвращает список задач по id Плана, к которому они относятся
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Task> GetTaskListByPlanId(int id)
        {
            List<Task> tasks = new List<Task>();
            using (_connection)
            {
                tasks = _connection.Query<Task>("SELECT * FROM tasks WHERE planId = @id", new { id }).ToList();
            }
            return tasks;
        }

        /// <summary>
        /// Обновление текущей задачи
        /// </summary>
        /// <param name="task"></param>
        public void UpdateCurrentTask(Task task)
        {
            using (_connection)
            {
                string updatetQuery = "UPDATE tasks SET name = @name, priority = @priority, type = @type, intensity = @intensity, startCompletion = @startCompletion, planCompletion = @planCompletion, comment = @comment WHERE id = @id";
                var result = _connection.Execute(updatetQuery, task);
            }            
        }

        /// <summary>
        /// Добавление новой задачи
        /// </summary>
        /// <param name="task"></param>
        public void InsertNewTask(Task task)
        {
            using (_connection)
            {                
                string insertQuery = "INSERT INTO tasks (name, planId, priority, type, intensity, startCompletion, planCompletion, comment) VALUES (@name, @planId, @priority, @type, @intensity, @startCompletion, @planCompletion, @comment)";
                var result = _connection.Execute(insertQuery, task);
            }
        }

        /// <summary>
        /// Удаление текущей задачи
        /// </summary>
        /// <param name="task"></param>
        public void DeleteCurrentTask(Task task)
        {
            using (_connection)
            {                
                string deleteQuery = "DELETE FROM tasks WHERE id = @id";
                var result = _connection.Execute(deleteQuery, new
                {
                    task.id
                });                
            }
        }

        /// <summary>
        /// Удаление всех задач, относящихся к Плану по его id
        /// </summary>
        /// <param name="id"></param>
        public void DeleteTasksByPlanId(int id)
        {
            using (_connection)
            {
                string deleteQuery = "DELETE FROM tasks WHERE planId = @id";
                var result = _connection.Execute(deleteQuery, new
                {
                   id
                });
            }
        }
    }
}
