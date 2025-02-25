using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP02.Models
{
    public class Disciplines
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public int teacherId { get; set; }
    }
}