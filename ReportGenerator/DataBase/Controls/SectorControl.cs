using Dapper;
using ReportGenerator.DataBase.Models;
using ReportGenerator.Services;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;


namespace ReportGenerator.DataBase.Controls
{
    /// <summary>
    /// Класс управления Сектором (работа с БД)
    /// </summary>
    public class SectorControl
    {
        private SqlConnection _connection;
        public SectorControl()
        {
            DbConnection db = new DbConnection();
            _connection = db.GetConnection();
        }
        /// <summary>
        /// Возвращает название сектора по его id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetNameById(int id)
        {
            Sector sector = null;
            using (_connection)
            {
                sector = _connection.Query<Sector>("SELECT name FROM sectors WHERE id = @id", new { id }).FirstOrDefault();
            }
            return sector.name;
        }

        /// <summary>
        /// Возвращает список названий всех секторов
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllNameSectors()
        {
            List<Sector> sectors = new List<Sector>();            
            using (_connection)
            {
                sectors = _connection.Query<Sector>("Select name From sectors").ToList();
            }
            List<string> sectorNames = new List<string>();
            foreach (Sector sector in sectors)
            {
                sectorNames.Add(sector.name);
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
            Sector sector = new Sector();
            using (_connection)
            {
                sector = _connection.Query<Sector>("SELECT id FROM sectors WHERE name = @name", new { name }).FirstOrDefault();
            }
            return sector.id;
        }
    }
}
