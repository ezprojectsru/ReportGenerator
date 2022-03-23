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
        /// <summary>
        /// Возвращает список задач по id Плана, к которому они относятся
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Task> GetTaskListByPlanId(int id)
        {
            List<Task> tasks = new List<Task>();
            DbConnection db = new DbConnection();
            SqlConnection conn = db.GetConnection();

            using (conn)
            {
                tasks = conn.Query<Task>("SELECT * FROM tasks WHERE planId = @id", new { id }).ToList();
            }

            return tasks;
        }

        /// <summary>
        /// Обновление текущей задачи
        /// </summary>
        /// <param name="task"></param>
        public void UpdateCurrentTask(Task task)
        {            
            DbConnection db = new DbConnection();
            SqlConnection conn = db.GetConnection();

            using (conn)
            {
                string updatetQuery = "UPDATE tasks SET name = @name, priority = @priority, type = @type, intensity = @intensity, startCompletion = @startCompletion, planCompletion = @planCompletion, comment = @comment WHERE id = @id";
                var result = conn.Execute(updatetQuery, task);
            }            
        }

        /// <summary>
        /// Добавление новой задачи
        /// </summary>
        /// <param name="task"></param>
        public void InsertNewTask(Task task)
        {
            DbConnection db = new DbConnection();
            SqlConnection conn = db.GetConnection();

            using (conn)
            {                
                string insertQuery = "INSERT INTO tasks (name, planId, priority, type, intensity, startCompletion, planCompletion, comment) VALUES (@name, @planId, @priority, @type, @intensity, @startCompletion, @planCompletion, @comment)";
                var result = conn.Execute(insertQuery, task);
            }
        }

        /// <summary>
        /// Удаление текущей задачи
        /// </summary>
        /// <param name="task"></param>
        public void DeleteCurrentTask(Task task)
        {
            DbConnection db = new DbConnection();
            SqlConnection conn = db.GetConnection();

            using (conn)
            {                
                string deleteQuery = "DELETE FROM tasks WHERE id = @id";
                var result = conn.Execute(deleteQuery, new
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
            DbConnection db = new DbConnection();
            SqlConnection conn = db.GetConnection();

            using (conn)
            {
                string deleteQuery = "DELETE FROM tasks WHERE planId = @id";
                var result = conn.Execute(deleteQuery, new
                {
                   id
                });
            }
        }
    }
}
