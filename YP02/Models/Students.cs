using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP02.Models
{
    public class Students
    {
        // Основной ключ для сущности "Студент" (идентификатор записи)
        [Key]
        public int id { get; set; }

        // Фамилия студента
        public string surname { get; set; }

        // Имя студента
        public string name { get; set; }

        // Отчество студента
        public string lastname { get; set; }

        // Идентификатор учебной группы, к которой относится студент
        public int studGroupId { get; set; }

        // Дата, когда студент был отчислен или переведен
        public DateTime dateOfRemand { get; set; }

        // Идентификатор пользователя в системе (возможно для аутентификации)
        public int userId { get; set; }

        // Навигационное свойство для связи с сущностью "StudGroups" (учебная группа)
        public StudGroups StudGroup { get; set; }
    }

}