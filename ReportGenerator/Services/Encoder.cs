using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportGenerator.Services
{
    public class Encoder
    {

            public static string GetData(string data)
            {

                ushort secretKey = 0x0088; // Секретный ключ (длина - 16 bit).
                string str = data; //это строка которую мы зашифруем

                str = EncodeDecrypt(str, secretKey); //производим шифрование
                return str;
            }

            public static string EncodeDecrypt(string str, ushort secretKey)
            {
                var ch = str.ToArray(); //преобразуем строку в символы
                string newStr = "";      //переменная которая будет содержать зашифрованную строку
                foreach (var c in ch)  //выбираем каждый элемент из массива символов нашей строки
                    newStr += TopSecret(c, secretKey);  //производим шифрование каждого отдельного элемента и сохраняем его в строку
                return newStr;
            }

            public static char TopSecret(char character, ushort secretKey)
            {
                character = (char)(character ^ secretKey); //Производим XOR операцию
                return character;
            }



        
    }
}
