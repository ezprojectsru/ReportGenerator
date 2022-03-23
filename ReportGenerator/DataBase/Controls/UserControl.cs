
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
        private SqlConnection _connection;
        public UserControl()
        {
            DbConnection db = new DbConnection();
            _connection = db.GetConnection();
        }
        /// <summary>
        /// Возвращает полное имя пользователя по id пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetFullNameById(int id)
        {
            User user = null;
            using (_connection)
            {
                user = _connection.Query<User>("SELECT fullName FROM users WHERE id = @id", new { id }).FirstOrDefault();
            }
            return user.fullName;
        }

        /// <summary>
        /// Возвращает id отдела пользователя по id пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetDepartamentIdById(int id)
        {
            User user = new User();
            using (_connection)
            {
                user = _connection.Query<User>("SELECT * FROM users WHERE id = @id", new { id }).FirstOrDefault();
            }
            return user.departamentId;
        }

        /// <summary>
        /// Возвращает id пользователя по его полному имени
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns></returns>
        public int GetIddByFullName(string fullName)
        {
            User user = new User();
            using (_connection)
            {
                user = _connection.Query<User>("SELECT id FROM users WHERE fullName = @fullName", new { fullName }).FirstOrDefault();
            }
            return user.id;
        }

        /// <summary>
        /// Возвращает список всех пользователей
        /// </summary>
        /// <returns></returns>
        public List<User> GetAllUsersList()
        {
            List<User> users = new List<User>();
            using (_connection)
            {
                users = _connection.Query<User>("Select * From users").ToList();
            }            
            return users;
        }

        /// <summary>
        /// Возвращает пользователя по его имени (username)
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public User GetUser(string name)
        {
            User currentUser = null;
            using (_connection)
            {                
                currentUser = _connection.Query<User>("SELECT * FROM Users WHERE username = @name", new { name}).FirstOrDefault();
            }            
            return currentUser;
        }

        /// <summary>
        /// Возвращает список полных имен всех пользователей
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllFullNameUsers()
        {
            List<User> users = new List<User>();
            using (_connection)
            {
                users = _connection.Query<User>("Select fullName From users").ToList();
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
        public void InsertNewUser(User user)
        {
            using (_connection)
            {                
                string insertQuery = "INSERT INTO users (username, password, create_date, fullName, email, departamentId, roleId, sectorId, groupId) VALUES (@username, @password, @create_date, @fullName, @email, @departamentId, @roleId, @sectorId, @groupId)";
                var result = _connection.Execute(insertQuery, user);
            }
        }

        /// <summary>
        /// Обновление текущего пользователя
        /// </summary>
        /// <param name="user"></param>
        public void UpdateCurrentUser(User user)
        {
            using (_connection)
            {
                string updatetQuery = "UPDATE users SET username = @username, password = @password, create_date = @create_date, fullName = @fullName, email = @email, departamentId = @departamentId, roleId = @roleId, sectorId = @sectorId, groupId = @groupId WHERE id = @id";
                var result = _connection.Execute(updatetQuery, user);
            }
        }

        /// <summary>
        /// Удаление текущего пользователя
        /// </summary>
        /// <param name="id"></param>
        public void DeleteCurrentUser(int id)
        {
            using (_connection)
            {
                string deleteQuery = "DELETE FROM users WHERE id = @id";
                var result = _connection.Execute(deleteQuery, new
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
        public bool ExistsUserName(string name)
        {
            User currentUser = null;
            using (_connection)
            {
                currentUser = _connection.Query<User>("SELECT * FROM Users WHERE username = @name", new { name }).FirstOrDefault();
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
