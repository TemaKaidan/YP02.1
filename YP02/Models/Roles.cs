using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP02.Models
{
    public class Roles
    {
        // Основной ключ для сущности "Роль" (идентификатор записи)
        [Key]
        public int id { get; set; }

        // Название роли (например, "Преподаватель", "Студент", "Администратор")
        public string roleName { get; set; }
    }

}