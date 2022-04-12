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
    /// Класс управления Планом (работа с БД)
    /// </summary>
    public class PlanControl
    {
        private readonly DbConnection _db = new DbConnection();
        
        /// <summary>
        /// Возвращает список Планов, пренадлежащих пользователю с id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Plan> GetPlanListByUserId(int id)
        {
            List<Plan> plans = new List<Plan>();
            try
            {
                SqlConnection connection = _db.GetConnection();
                plans = connection.Query<Plan>("SELECT * FROM plans WHERE responsibleId = @id", new {id})
                    .ToList();
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return plans;
        }

        public List<Plan> GetWeekPlanListByProjectId(int id, DateTime start, DateTime end)
        {
            List<Plan> plans = new List<Plan>();
            try
            {
                SqlConnection connection = _db.GetConnection();
                plans = connection.Query<Plan>("SELECT * FROM plans WHERE projectId = @id and startDate >= @start and finishDate <= @end", new { id, start, end })
                    .ToList();
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return plans;
        }

        public List<Plan> GetPlanListByProjectId(int id)
        {
            List<Plan> plans = new List<Plan>();
            try
            {
                SqlConnection connection = _db.GetConnection();
                plans = connection.Query<Plan>("SELECT * FROM plans WHERE projectId = @id", new { id })
                    .ToList();
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return plans;
        }

        public List<Plan> GetPlanListBetweenDatesByUserId(int id, DateTime start, DateTime end)
        {
            List<Plan> plans = new List<Plan>();
            try
            {
                SqlConnection connection = _db.GetConnection();
                plans = connection.Query<Plan>("SELECT * FROM plans WHERE responsibleId = @id and startDate >= @start and startDate <= @end", new { id, start, end })
                    .ToList();
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
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
            Plan plan = null;
            try
            {
                SqlConnection connection = _db.GetConnection();
                plan = connection.Query<Plan>("SELECT * FROM plans WHERE id = @id", new {id}).FirstOrDefault();
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
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
            int responsibleId = 0;
            try
            {
                SqlConnection connection = _db.GetConnection();
                Plan plan = connection.Query<Plan>("SELECT * FROM plans WHERE id = @id", new {id}).FirstOrDefault();
                responsibleId = plan.responsibleId;
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return responsibleId;
        }

        /// <summary>
        /// Добавление нового Плана
        /// </summary>
        /// <param name="plan"></param>
        public void InsertNewPlan(Plan plan)
        {
            try
            {
                SqlConnection connection = _db.GetConnection();
                string insertQuery =
                    "INSERT INTO plans (name, startDate, finishDate, projectId, responsibleId, directorId, comment) VALUES (@name, @startDate, @finishDate, @projectId, @responsibleId, @directorId, @comment)";
                var result = connection.Execute(insertQuery, plan);
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }
        }

        /// <summary>
        /// Обновление текущего плана
        /// </summary>
        /// <param name="plan"></param>
        public void UpdateCurrentPlan(Plan plan)
        {
            try
            {
                SqlConnection connection = _db.GetConnection();
                string updatetQuery =
                    "UPDATE plans SET name = @name, startDate = @startDate, finishDate = @finishDate, projectId = @projectId, responsibleId = @responsibleId, directorId = @directorId, comment = @comment WHERE id = @id";
                var result = connection.Execute(updatetQuery, plan);
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }
        }

        /// <summary>
        /// Удаление плана по его id
        /// </summary>
        /// <param name="id"></param>
        public void DeleteCurrentPlan(int id)
        {
            try
            {
                SqlConnection connection = _db.GetConnection();
                string deleteQuery = "DELETE FROM plans WHERE id = @id";
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
