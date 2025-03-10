using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP02.Models
{
    public class Users
    {
        // Основной ключ для сущности "Пользователь" (идентификатор записи)
        [Key]
        public int id { get; set; }

        // Логин пользователя, уникальный идентификатор для входа
        public string login { get; set; }

        // Пароль пользователя для аутентификации
        public string password { get; set; }

        // Идентификатор роли пользователя (ссылка на роль пользователя)
        public int role { get; set; }
    }
}