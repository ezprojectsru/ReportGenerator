
using System;
using System.Collections.Generic;
using ReportGenerator.DataBase.Models;
using ReportGenerator.Services;
using System.Data.SqlClient;
using System.IO;
using Dapper;
using System.Linq;

namespace ReportGenerator.DataBase.Controls
{
    /// <summary>
    /// Класс управления Пользователем (работа с БД)
    /// </summary>
    public class UserControl
    {
        private readonly DbConnection _db = new DbConnection();
        
        /// <summary>
        /// Возвращает полное имя пользователя по id пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetFullNameById(int id)
        {
            string fullName = "";
            try
            {
                SqlConnection connection = _db.GetConnection();
                User user = connection.Query<User>("SELECT fullName FROM users WHERE id = @id", new {id})
                    .FirstOrDefault();
                connection.Dispose();
                fullName = user.fullName;
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return fullName;
        }

        public User GetUserById(int id)
        {
            User user = new User();
            try
            {
                SqlConnection connection = _db.GetConnection();
                user = connection.Query<User>("SELECT * FROM users WHERE id = @id", new { id })
                    .FirstOrDefault();
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return user;
        }



        /// <summary>
        /// Возвращает id отдела пользователя по id пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetDepartamentIdById(int id)
        {
            int departamentId = 0;
            try
            {
                SqlConnection connection = _db.GetConnection();
                User user = connection.Query<User>("SELECT * FROM users WHERE id = @id", new {id}).FirstOrDefault();
                connection.Dispose();
                departamentId = user.departamentId;
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return departamentId;
        }

        /// <summary>
        /// Возвращает id пользователя по его полному имени
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns></returns>
        public int GetIddByFullName(string fullName)
        {
            int userId = 0;
            try
            {
                SqlConnection connection = _db.GetConnection();
                User user = connection.Query<User>("SELECT id FROM users WHERE fullName = @fullName", new {fullName})
                    .FirstOrDefault();
                connection.Dispose();
                userId = user.id;
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return userId;
        }

        /// <summary>
        /// Возвращает список всех пользователей
        /// </summary>
        /// <returns></returns>
        public List<User> GetAllUsersList()
        {
            List<User> users = new List<User>();
            try
            {
                SqlConnection connection = _db.GetConnection();
                users = connection.Query<User>("Select * From users").ToList();
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
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
            try
            {
                SqlConnection connection = _db.GetConnection();
                currentUser = connection.Query<User>("SELECT * FROM Users WHERE username = @name", new {name})
                    .FirstOrDefault();
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return currentUser;
        }

        /// <summary>
        /// Возвращает список полных имен всех пользователей
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllFullNameUsers()
        {
            List<string> userFullNames = new List<string>();
            try
            {
                SqlConnection connection = _db.GetConnection();
                List<User> users = connection.Query<User>("Select fullName From users").ToList();
                connection.Dispose();
                
                foreach (User user in users)
                {
                    userFullNames.Add(user.fullName);
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            return userFullNames;
        }

        /// <summary>
        /// Добавление нового пользователя
        /// </summary>
        /// <param name="user"></param>
        public void InsertNewUser(User user)
        {
            try
            {
                SqlConnection connection = _db.GetConnection();
                string insertQuery =
                    "INSERT INTO users (username, password, create_date, fullName, email, departamentId, roleId, sectorId, groupId) VALUES (@username, @password, @create_date, @fullName, @email, @departamentId, @roleId, @sectorId, @groupId)";
                var result = connection.Execute(insertQuery, user);
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

        }

        /// <summary>
        /// Обновление текущего пользователя с новым паролем
        /// </summary>
        /// <param name="user"></param>
        public void UpdateCurrentUserWithPassword(User user)
        {
            try
            {
                SqlConnection connection = _db.GetConnection();
                string updatetQuery =
                    "UPDATE users SET username = @username, password = @password, create_date = @create_date, fullName = @fullName, email = @email, departamentId = @departamentId, roleId = @roleId, sectorId = @sectorId, groupId = @groupId WHERE id = @id";
                var result = connection.Execute(updatetQuery, user);
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

        }

        /// <summary>
        /// Обновление текущего пользователя с новым паролем
        /// </summary>
        /// <param name="user"></param>
        public void UpdateCurrentUserWithOutPassword(User user)
        {
            try
            {
                SqlConnection connection = _db.GetConnection();
                string updatetQuery =
                    "UPDATE users SET username = @username, create_date = @create_date, fullName = @fullName, email = @email, departamentId = @departamentId, roleId = @roleId, sectorId = @sectorId, groupId = @groupId WHERE id = @id";
                var result = connection.Execute(updatetQuery, user);
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

        }

        /// <summary>
        /// Удаление текущего пользователя
        /// </summary>
        /// <param name="id"></param>
        public void DeleteCurrentUser(int id)
        {
            try
            {
                SqlConnection connection = _db.GetConnection();
                string deleteQuery = "DELETE FROM users WHERE id = @id";
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

        /// <summary>
        /// Возвращает TRUE, если пользователь с таким именем (username) существует. Иначе возвращает FALSE.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool ExistsUserName(string name)
        {
            User currentUser = null;
            try
            {
                SqlConnection connection = _db.GetConnection();
                currentUser = connection.Query<User>("SELECT * FROM Users WHERE username = @name", new {name})
                    .FirstOrDefault();
                connection.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Constants.LogFileName, ex.ToString());
            }

            if (currentUser != null)
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
