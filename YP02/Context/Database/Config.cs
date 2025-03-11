using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP02.Context.Database
{
    public class Config
    {
        // Строка подключения к базе данных MySQL, содержащая сервер, порт, базу данных, пользователя и пароль
        public static readonly string connection = "server=127.0.0.1;port=3312;database=UP02;user=root;pwd=;";

        // Версия MySQL-сервера, с которым будет происходить соединение
        public static readonly MySqlServerVersion version = new MySqlServerVersion(new Version(8, 0, 11));
    }
}