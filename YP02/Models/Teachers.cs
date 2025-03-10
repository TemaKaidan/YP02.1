using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP02.Models
{
    public class Teachers
    {
        // Основной ключ для сущности "Преподаватель" (идентификатор записи)
        [Key]
        public int id { get; set; }

        // Фамилия преподавателя
        public string surname { get; set; }

        // Имя преподавателя
        public string name { get; set; }

        // Отчество преподавателя
        public string lastname { get; set; }

        // Логин преподавателя
        public string login { get; set; }

        // Пароль преподавателя
        public string password { get; set; }

        // Идентификатор пользователя (возможно, для связи с системой пользователей)
        public int userId { get; set; }

        // Навигационное свойство для связи с нагрузками преподавателя
        // Один преподаватель может иметь множество нагрузок
        public ICollection<TeachersLoad> TeachersLoads { get; set; }
    }

}