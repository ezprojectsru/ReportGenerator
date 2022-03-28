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
    /// Класс управления Сектором (работа с БД)
    /// </summary>
    public class SectorControl
    {
        private readonly DbConnection _db = new DbConnection();
        
        /// <summary>
        /// Возвращает название сектора по его id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetNameById(int id)
        {
            string name = "";
            try
            {
                SqlConnection connection = _db.GetConnection();
                Sector sector = connection.Query<Sector>("SELECT name FROM sectors WHERE id = @id", new { id }).FirstOrDefault();
                name =  sector.name;
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }
            return name;
        }

        /// <summary>
        /// Возвращает список названий всех секторов
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllNameSectors()
        {
            List<string> sectorNames = new List<string>();
            try
            {
                SqlConnection connection = _db.GetConnection();
                List<Sector> sectors = connection.Query<Sector>("Select name From sectors").ToList();
                connection.Dispose();

                foreach (Sector sector in sectors)
                {
                    sectorNames.Add(sector.name);
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }
            return sectorNames;
        }

        /// <summary>
        /// Возвращает id сектора по его названию
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetIddByName(string name)
        {
            int id = 0;
            try
            {
                SqlConnection connection = _db.GetConnection();
                Sector sector = connection.Query<Sector>("SELECT id FROM sectors WHERE name = @name", new {name})
                    .FirstOrDefault();
                id =  sector.id;
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }
            return id;
        }
    }
}
