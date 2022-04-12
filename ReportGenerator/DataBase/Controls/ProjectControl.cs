using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using Dapper;
using ReportGenerator.DataBase.Models;
using ReportGenerator.Services;

namespace ReportGenerator.DataBase.Controls
{
    public class ProjectControl
    {
        private readonly DbConnection _db = new DbConnection();
        public List<Project> GetAllProjects()
        {
            List<Project> projects = new List<Project>();
            try
            {

                SqlConnection connection = _db.GetConnection();
                projects = connection.Query<Project>("Select * From projects").ToList();
                connection.Dispose();

                foreach (Project pr in projects)
                {
                    PlanControl pc = new PlanControl();
                    List<Plan> plans = pc.GetPlanListByProjectId(pr.Id);
                    if (plans.Count > 0)
                    {
                        pr.ProjectPlans = new List<Plan>(plans);
                       // pr.ProjectPlans.AddRange(plans);
                    }

                }

            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return projects;
        }

        public List<Project> GetAllWeekProjectsByServiceId(int id, DateTime start, DateTime end)
        {
            List<Project> projects = new List<Project>();
            try
            {

                SqlConnection connection = _db.GetConnection();
                projects = connection.Query<Project>("Select * From projects WHERE servicesId = @id", new { id }).ToList();
                connection.Dispose();

                foreach (Project pr in projects)
                {
                    PlanControl pc = new PlanControl();
                    List<Plan> plans = pc.GetWeekPlanListByProjectId(pr.Id,start, end);
                    if (plans.Count > 0)
                    {
                        pr.ProjectPlans = new List<Plan>(plans);
                        // pr.ProjectPlans.AddRange(plans);
                    }

                }

            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return projects;
        }

        public List<Project> GetAllProjectsByServiceId(int id)
        {
            List<Project> projects = new List<Project>();
            try
            {

                SqlConnection connection = _db.GetConnection();
                projects = connection.Query<Project>("Select * From projects WHERE servicesId = @id", new { id }).ToList();
                connection.Dispose();

                foreach (Project pr in projects)
                {
                    PlanControl pc = new PlanControl();
                    List<Plan> plans = pc.GetPlanListByProjectId(pr.Id);
                    if (plans.Count > 0)
                    {
                        pr.ProjectPlans = new List<Plan>(plans);
                        // pr.ProjectPlans.AddRange(plans);
                    }

                }

            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return projects;
        }

        public int GetServiceIdByProjectId(int id)
        {
            int servicesId = 0;
            try
            {

                SqlConnection connection = _db.GetConnection();
                Project project = connection.Query<Project>("Select servicesId From projects WHERE id = @id", new { id }).FirstOrDefault();
                connection.Dispose();

                servicesId = project.ServicesId;

            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return servicesId;
        }

        public Project GetProjectById(int id)
        {
            Project project = new Project();
            try
            {

                SqlConnection connection = _db.GetConnection();
                project = connection.Query<Project>("Select * From projects WHERE id = @id", new { id }).FirstOrDefault();
                connection.Dispose();

            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return project;
        }

        public void InsertNewProject(Project project)
        {
            try
            {
                SqlConnection connection = _db.GetConnection();
                string insertQuery =
                    "INSERT INTO projects (name, servicesId, projectStatusId, statusPercent, description) VALUES (@name, @servicesId, @projectStatusId, @statusPercent, @description)";
                var result = connection.Execute(insertQuery, project);
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }
        }

        public void UpdateCurrentProject(Project project)
        {
            try
            {
                SqlConnection connection = _db.GetConnection();
                string updatetQuery =
                    "UPDATE projects SET name = @name, servicesId = @servicesId, projectStatusId = @projectStatusId, statusPercent = @statusPercent, description = @description WHERE id = @id";
                var result = connection.Execute(updatetQuery, project);
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }
        }

        public void DeleteCurrentProject(int id)
        {
            try
            {
                SqlConnection connection = _db.GetConnection();
                string deleteQuery = "DELETE FROM projects WHERE id = @id";
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
