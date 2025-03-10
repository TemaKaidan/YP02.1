using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP02.Models
{
    public class Consultations
    {
        // Основной ключ для сущности "Консультация" (идентификатор записи)
        [Key]
        public int id { get; set; }

        // Идентификатор дисциплины, к которой относится консультация
        public int disciplineId { get; set; }

        // Работы, которые студент должен был представить на консультацию
        public string submittedWorks { get; set; }

        // Дата проведения консультации
        public DateTime date { get; set; }
    }

}