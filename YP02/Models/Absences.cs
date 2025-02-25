using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP02.Models
{
    public class Absences
    {
        public int id { get; set; }
        public int studentId { get; set; }
        public int disciplineId { get; set; }
        public DateTime date { get; set; }
        public int delayMinutes { get; set; }
        public string explanatoryNote { get; set; }
    }
}