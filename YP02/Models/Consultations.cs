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
        [Key]
        public int id { get; set; }
        public int disciplineId { get; set; }
        public DateTime date { get; set; }
    }
}