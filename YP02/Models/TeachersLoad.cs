using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP02.Models
{
    public class TeachersLoad
    {
        // Основной ключ для сущности "Нагрузка преподавателя" (идентификатор записи)
        [Key]
        public int id { get; set; }

        // Идентификатор преподавателя (ссылка на преподавателя)
        public int teacherId { get; set; }

        // Идентификатор дисциплины (ссылка на дисциплину, которую ведет преподаватель)
        public int disciplineId { get; set; }

        // Идентификатор учебной группы (ссылка на группу, которой преподаватель преподает)
        public int studGroupId { get; set; }

        // Количество часов лекций
        public int lectureHours { get; set; }

        // Количество часов практических занятий
        public int practiceHours { get; set; }

        // Количество часов консультаций
        public int сonsultationHours { get; set; }

        // Количество часов на курсовой проект
        public int courseprojectHours { get; set; }

        // Количество часов экзаменов
        public int examHours { get; set; }

        // Навигационное свойство для связи с преподавателем
        // Каждый преподаватель может иметь множество нагрузок
        public Teachers Teacher { get; set; }
    }

}