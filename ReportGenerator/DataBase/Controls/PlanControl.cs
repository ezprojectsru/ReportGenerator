using Dapper;
using ReportGenerator.DataBase.Models;
using ReportGenerator.Services;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;


namespace ReportGenerator.DataBase.Controls
{
    /// <summary>
    /// Класс управления Планом (работа с БД)
    /// </summary>
    public class PlanControl
    {
        private SqlConnection _connection;
        public PlanControl()
        {
            DbConnection db = new DbConnection();
            _connection = db.GetConnection();
        }
        /// <summary>
        /// Возвращает список Планов, пренадлежащих пользователю с id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Plan> GetPlanListByUserId(int id)
        {
            List<Plan> plans = new List<Plan>();
            using (_connection)
            {
                plans = _connection.Query<Plan>("SELECT * FROM plans WHERE responsibleId = @id", new { id }).ToList();
            }
            return plans;
        }

        /// <summary>
        /// Возвращает План по его id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Plan GetPlanById(int id)
        {
            Plan plan = new Plan(); 
            using (_connection)
            {
                plan = _connection.Query<Plan>("SELECT * FROM plans WHERE id = @id", new { id }).FirstOrDefault();
            }
            return plan;
        }

        /// <summary>
        /// Возвращает id пользователя, ответственного за выполнение плана, по id Плана
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetResponsibleIdByPlanId(int id)
        {
            Plan plan = new Plan();
            using (_connection)
            {
                plan = _connection.Query<Plan>("SELECT * FROM plans WHERE id = @id", new { id }).FirstOrDefault();
            }
            return plan.responsibleId;
        }

        /// <summary>
        /// Добавление нового Плана
        /// </summary>
        /// <param name="plan"></param>
        public void InsertNewPlan(Plan plan)
        {            
            using (_connection)
            {               

                string insertQuery = "INSERT INTO plans (name, startDate, finishDate, responsibleId, directorId, comment) VALUES (@name, @startDate, @finishDate, @responsibleId, @directorId, @comment)";
                var result = _connection.Execute(insertQuery, plan);
            }
        }

        /// <summary>
        /// Обновление текущего плана
        /// </summary>
        /// <param name="plan"></param>
        public void UpdateCurrentPlan(Plan plan)
        {  
            using (_connection)
            {
                string updatetQuery = "UPDATE plans SET name = @name, startDate = @startDate, finishDate = @finishDate, responsibleId = @responsibleId, directorId = @directorId, comment = @comment WHERE id = @id";
                var result = _connection.Execute(updatetQuery, plan);
            }
        }

        /// <summary>
        /// Удаление плана по его id
        /// </summary>
        /// <param name="id"></param>
        public void DeleteCurrentPlan(int id)
        {
            using (_connection)
            {
                string deleteQuery = "DELETE FROM plans WHERE id = @id";
                var result = _connection.Execute(deleteQuery, new
                {
                    id
                });
            }
        }
    }
}
