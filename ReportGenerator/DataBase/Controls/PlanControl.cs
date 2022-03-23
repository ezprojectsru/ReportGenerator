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
        /// <summary>
        /// Возвращает список Планов, пренадлежащих пользователю с id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Plan> GetPlanListByUserId(int id)
        {
            List<Plan> plans = new List<Plan>();
            DbConnection db = new DbConnection();
            SqlConnection conn = db.GetConnection();

            using (conn)
            {
                plans = conn.Query<Plan>("SELECT * FROM plans WHERE responsibleId = @id", new { id }).ToList();
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
            DbConnection db = new DbConnection();
            SqlConnection conn = db.GetConnection();

            using (conn)
            {
                plan = conn.Query<Plan>("SELECT * FROM plans WHERE id = @id", new { id }).FirstOrDefault();
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
            DbConnection db = new DbConnection();
            SqlConnection conn = db.GetConnection();

            using (conn)
            {
                plan = conn.Query<Plan>("SELECT * FROM plans WHERE id = @id", new { id }).FirstOrDefault();
            }

            return plan.responsibleId;
        }

        /// <summary>
        /// Добавление нового Плана
        /// </summary>
        /// <param name="plan"></param>
        public void InsertNewPlan(Plan plan)
        {
            DbConnection db = new DbConnection();
            SqlConnection conn = db.GetConnection();

            using (conn)
            {               

                string insertQuery = "INSERT INTO plans (name, startDate, finishDate, responsibleId, directorId, comment) VALUES (@name, @startDate, @finishDate, @responsibleId, @directorId, @comment)";
                var result = conn.Execute(insertQuery, plan);
            }
        }

        /// <summary>
        /// Обновление текущего плана
        /// </summary>
        /// <param name="plan"></param>
        public void UpdateCurrentPlan(Plan plan)
        {
            DbConnection db = new DbConnection();
            SqlConnection conn = db.GetConnection();

            using (conn)
            {
                string updatetQuery = "UPDATE plans SET name = @name, startDate = @startDate, finishDate = @finishDate, responsibleId = @responsibleId, directorId = @directorId, comment = @comment WHERE id = @id";
                var result = conn.Execute(updatetQuery, plan);
            }
        }

        /// <summary>
        /// Удаление плана по его id
        /// </summary>
        /// <param name="id"></param>
        public void DeleteCurrentPlan(int id)
        {
            DbConnection db = new DbConnection();
            SqlConnection conn = db.GetConnection();

            using (conn)
            {
                string deleteQuery = "DELETE FROM plans WHERE id = @id";
                var result = conn.Execute(deleteQuery, new
                {
                    id
                });
            }
        }
    }
}
