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
            List<Task> tasks = _connection.Query<Task>("SELECT * FROM tasks WHERE planId = @id", new { id }).ToList();            
            return tasks;
        }

        /// <summary>
        /// Обновление текущей задачи
        /// </summary>
        /// <param name="task"></param>
        public void UpdateCurrentTask(Task task)
        {
            string updatetQuery = "UPDATE tasks SET name = @name, priority = @priority, typeId = @typeId, intensity = @intensity, startCompletion = @startCompletion, planCompletion = @planCompletion, comment = @comment WHERE id = @id";
            var result = _connection.Execute(updatetQuery, task);                       
        }

        /// <summary>
        /// Добавление новой задачи
        /// </summary>
        /// <param name="task"></param>
        public void InsertNewTask(Task task)
        {
            string insertQuery = "INSERT INTO tasks (name, planId, priority, typeId, intensity, startCompletion, planCompletion, comment) VALUES (@name, @planId, @priority, @typeId, @intensity, @startCompletion, @planCompletion, @comment)";
            var result = _connection.Execute(insertQuery, task);            
        }

        /// <summary>
        /// Удаление текущей задачи
        /// </summary>
        /// <param name="task"></param>
        public void DeleteCurrentTask(Task task)
        {
            string deleteQuery = "DELETE FROM tasks WHERE id = @id";
            var result = _connection.Execute(deleteQuery, new
                {
                    task.id
                });            
            
        }

        /// <summary>
        /// Удаление всех задач, относящихся к Плану по его id
        /// </summary>
        /// <param name="id"></param>
        public void DeleteTasksByPlanId(int id)
        {
            string deleteQuery = "DELETE FROM tasks WHERE planId = @id";
                var result = _connection.Execute(deleteQuery, new
                {
                   id
                });
            
        }
    }
}
