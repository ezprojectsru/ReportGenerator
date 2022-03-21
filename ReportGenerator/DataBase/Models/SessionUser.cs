

namespace ReportGenerator.DataBase.Models
{
    /// <summary>
    /// Класс пользователя текущей сесии. Будет перемещен из этой папки.
    /// </summary>
    public class SessionUser
    {
        public User user { get; set; }
        public SessionUser(User user)
        {
            this.user = user;
        }
    }
}
