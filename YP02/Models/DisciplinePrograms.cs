using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP02.Models
{
    public class DisciplinePrograms
    {
        public int id { get; set; }
        public int disciplineId { get; set; }
        public string theme { get; set; }
        public int lessonTypeId { get; set; }
        public int hoursCount { get; set; }
    }
}