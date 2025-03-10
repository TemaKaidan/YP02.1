using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP02.Models
{
    public class DisciplinePrograms
    {
        // Основной ключ для сущности "Программа дисциплины" (идентификатор записи)
        [Key]
        public int id { get; set; }

        // Идентификатор дисциплины, к которой относится программа
        public int disciplineId { get; set; }

        // Тема программы дисциплины
        public string theme { get; set; }

        // Идентификатор типа занятия (лекция, семинар и т.д.)
        public int lessonTypeId { get; set; }

        // Количество часов, отведенных для дисциплины
        public int hoursCount { get; set; }
    }

}