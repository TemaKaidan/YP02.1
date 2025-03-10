using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP02.Models
{
    public class ConsultationResults
    {
        // Основной ключ для сущности "Результат консультации" (идентификатор записи)
        [Key]
        public int id { get; set; }

        // Идентификатор консультации, к которой относится результат
        public int consultationId { get; set; }

        // Идентификатор студента, который присутствовал на консультации
        public int studentId { get; set; }

        // Присутствовал ли студент на консультации (например, "Присутствует", "Отсутствует")
        public string presence { get; set; }

        // Был ли представлен студентом практический материал на консультации
        public string submittedPractice { get; set; }

        // Дата проведения консультации
        public DateTime date { get; set; }

        // Объяснительная записка, если студент не смог присутствовать на консультации
        public string explanatoryNote { get; set; }
    }

}