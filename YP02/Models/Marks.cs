using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP02.Models
{
    public class Marks
    {
        // Основной ключ для сущности "Оценка" (идентификатор записи)
        [Key]
        public int id { get; set; }

        // Оценка, полученная студентом
        public string mark { get; set; }

        // Идентификатор дисциплины, к которой относится эта оценка
        public int disciplineProgramId { get; set; }

        // Идентификатор студента, которому была поставлена оценка
        public int studentId { get; set; }

        // Описание, дополнительная информация о оценке (например, комментарий преподавателя)
        public string description { get; set; }
    }

}