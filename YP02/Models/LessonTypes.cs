using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP02.Models
{
    public class LessonTypes
    {
        // Основной ключ для сущности "Тип занятия" (идентификатор записи)
        [Key]
        public int id { get; set; }

        // Наименование типа занятия (например, лекция, практическое занятие)
        public string typeName { get; set; }
    }

}