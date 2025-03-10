using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP02.Models
{
    public class Absences
    {
        // Основной ключ для сущности "Отсутствие" (будет использоваться для идентификации каждой записи)
        [Key]
        public int id { get; set; }

        // Идентификатор студента, который пропустил занятие
        public int studentId { get; set; }

        // Идентификатор дисциплины, по которой студент пропустил занятие
        public int disciplineId { get; set; }

        // Время задержки (в минутах), если студент пришел позже
        public int delayMinutes { get; set; }

        // Объяснительная записка студента по поводу отсутствия
        public string explanatoryNote { get; set; }
    }

}