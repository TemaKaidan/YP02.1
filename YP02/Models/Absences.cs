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
        [Key]
        public int id { get; set; }
        public int studentId { get; set; }
        public int disciplineId { get; set; }
        public DateTime date { get; set; }
        public int delayMinutes { get; set; }
        public string explanatoryNote { get; set; }
    }
}