
using System.Collections.Generic;
using ReportGenerator.DataBase.Models;
using ReportGenerator.Services;
using System.Data.SqlClient;
using Dapper;
using System.Linq;

namespace ReportGenerator.DataBase.Controls
{
    /// <summary>
    /// Класс управления Пользователем (работа с БД)
    /// </summary>
    public class UserControl
    {
        /// <summary>
        /// Возвращает полное имя пользователя по id пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetFullNameById(int id)
        {
            User user = null;
            DbConnection db = new DbConnection();
            SqlConnection conn = db.GetConnection();

            using (conn)
            {
                user = conn.Query<User>("SELECT fullName FROM users WHERE id = @id", new { id }).FirstOrDefault();
            }

            return user.fullName;
        }

        /// <summary>
        /// Возвращает id отдела пользователя по id пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int GetDepartamentIdById(int id)
        {
            User user = new User();
            DbConnection db = new DbConnection();
            SqlConnection conn = db.GetConnection();

            using (conn)
            {
                user = conn.Query<User>("SELECT * FROM users WHERE id = @id", new { id }).FirstOrDefault();
            }

            return user.departamentId;
        }

        /// <summary>
        /// Возвращает id пользователя по его полному имени
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns></returns>
        public static int GetIddByFullName(string fullName)
        {
            User user = new User();
            DbConnection db = new DbConnection();
            SqlConnection conn = db.GetConnection();

            using (conn)
            {
                user = conn.Query<User>("SELECT id FROM users WHERE fullName = @fullName", new { fullName }).FirstOrDefault();
            }

            return user.id;
        }

        /// <summary>
        /// Возвращает список всех пользователей
        /// </summary>
        /// <returns></returns>
        public static List<User> GetAllUsersList()
        {
            List<User> users = new List<User>();
            DbConnection db = new DbConnection();
            SqlConnection conn = db.GetConnection();
            
            using (conn)
            {
                users = conn.Query<User>("Select * From users").ToList();
            }                
            
            return users;
        }

        /// <summary>
        /// Возвращает пользователя по его имени (username)
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static User GetUser(string name)
        {
            User currentUser = null;
            DbConnection db = new DbConnection();
            SqlConnection conn = db.GetConnection();

            using (conn)
            {                
                currentUser = conn.Query<User>("SELECT * FROM Users WHERE username = @name", new { name}).FirstOrDefault();
            }
            
            return currentUser;
        }

        /// <summary>
        /// Возвращает список полных имен всех пользователей
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAllFullNameUsers()
        {
            List<User> users = new List<User>();
            DbConnection db = new DbConnection();
            SqlConnection conn = db.GetConnection();

            using (conn)
            {
                users = conn.Query<User>("Select fullName From users").ToList();
            }

            List<string> userFullNames = new List<string>();
            foreach(User user in users)
            {
                userFullNames.Add(user.fullName);
            }

            return userFullNames;
        }

        /// <summary>
        /// Добавление нового пользователя
        /// </summary>
        /// <param name="user"></param>
        public static void InsertNewUser(User user)
        {
            DbConnection db = new DbConnection();
            SqlConnection conn = db.GetConnection();

            using (conn)
            {                
                string insertQuery = "INSERT INTO users (username, password, create_date, fullName, email, departamentId, roleId, sectorId, groupId) VALUES (@username, @password, @create_date, @fullName, @email, @departamentId, @roleId, @sectorId, @groupId)";
                var result = conn.Execute(insertQuery, user);
            }
        }

        /// <summary>
        /// Обновление текущего пользователя
        /// </summary>
        /// <param name="user"></param>
        public static void UpdateCurrentUser(User user)
        {
            DbConnection db = new DbConnection();
            SqlConnection conn = db.GetConnection();

            using (conn)
            {
                string updatetQuery = "UPDATE users SET username = @username, password = @password, create_date = @create_date, fullName = @fullName, email = @email, departamentId = @departamentId, roleId = @roleId, sectorId = @sectorId, groupId = @groupId WHERE id = @id";
                var result = conn.Execute(updatetQuery, user);
            }
        }

        /// <summary>
        /// Удаление текущего пользователя
        /// </summary>
        /// <param name="id"></param>
        public static void DeleteCurrentUser(int id)
        {
            DbConnection db = new DbConnection();
            SqlConnection conn = db.GetConnection();

            using (conn)
            {
                string deleteQuery = "DELETE FROM users WHERE id = @id";
                var result = conn.Execute(deleteQuery, new
                {
                    id
                });
            }
        }

        /// <summary>
        /// Возвращает TRUE, если пользователь с таким именем (username) существует. Иначе возвращает FALSE.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool ExistsUserName(string name)
        {
            User currentUser = null;
            DbConnection db = new DbConnection();
            SqlConnection conn = db.GetConnection();

            using (conn)
            {
                currentUser = conn.Query<User>("SELECT * FROM Users WHERE username = @name", new { name }).FirstOrDefault();
            }

            if(currentUser != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
