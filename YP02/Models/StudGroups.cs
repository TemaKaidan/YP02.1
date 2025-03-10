using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP02.Models
{
    public class StudGroups
    {
        // Основной ключ для сущности "Учебная группа" (идентификатор записи)
        [Key]
        public int id { get; set; }

        // Наименование учебной группы
        public string name { get; set; }
    }

}