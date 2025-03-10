using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP02.Models
{
    public class Disciplines
    {
        // Основной ключ для сущности "Дисциплина" (идентификатор записи)
        [Key]
        public int id { get; set; }

        // Наименование дисциплины
        public string name { get; set; }

        // Идентификатор преподавателя, который ведет дисциплину
        public int teacherId { get; set; }
    }

}