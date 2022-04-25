using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using Dapper;
using ReportGenerator.Data.Models;
using ReportGenerator.Services;

namespace ReportGenerator.Data.Controls
{
    public class PlanControl
    {
        private readonly DbConnection _db = new DbConnection();

        public List<Plan> GetAllPlanList()
        {
            List<Plan> plans = new List<Plan>();
            try
            {
                SqlConnection connection = _db.GetConnection();
                plans = connection.Query<Plan>("Select * From plans").ToList();
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return plans;
        }
    }
}
