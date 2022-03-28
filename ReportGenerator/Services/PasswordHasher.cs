using System;
using System.Security.Cryptography;


namespace ReportGenerator.Services
{

    public static class PasswordHasher
    {
        
        private const int SaltSize = 16;
        private const int HashSize = 20;

        /// <summary>
        /// Создание хеша из пароля
        /// </summary>
        /// <param name="password">Пароль</param>
        /// <param name="iterations">Колличество итераций</param>
        /// <returns>Хеш</returns>
        public static string Hash(string password, int iterations)
        {
            
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltSize]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            var hash = pbkdf2.GetBytes(HashSize);

            var hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            var base64Hash = Convert.ToBase64String(hashBytes);

            return string.Format("$RGENHASH$V1${0}${1}", iterations, base64Hash);
        }

        /// <summary>
        /// Создание хеша из пароля в 10000 итераций
        /// </summary>
        /// <param name="password">Пароль</param>
        /// <returns>Хеш</returns>
        public static string Hash(string password)
        {
            return Hash(password, 10000);
        }

        /// <summary>
        /// Проверка поддержки хеша
        /// </summary>
        /// <param name="hashString">Хеш</param>
        /// <returns>Поддерживается ли</returns>
        public static bool IsHashSupported(string hashString)
        {
            return hashString.Contains("$RGENHASH$V1$");
        }

        /// <summary>
        /// Проверяет пароль на соответствие хэшу.
        /// </summary>
        /// <param name="password">Пароль</param>
        /// <param name="hashedPassword">Хеш</param>
        /// <returns>Соответствует ли</returns>
        public static bool Verify(string password, string hashedPassword)
        {
            
            if (!IsHashSupported(hashedPassword))
            {
                throw new NotSupportedException("Хеш не поддерживается");
            }

            var splittedHashString = hashedPassword.Replace("$RGENHASH$V1$", "").Split('$');
            var iterations = int.Parse(splittedHashString[0]);
            var base64Hash = splittedHashString[1];

            var hashBytes = Convert.FromBase64String(base64Hash);

            var salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            for (var i = 0; i < HashSize; i++)
            {
                if (hashBytes[i + SaltSize] != hash[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
