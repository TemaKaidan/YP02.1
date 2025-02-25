using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP02.Models
{
    public class Marks
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public string mark { get; set; }
        public int disciplineProgramId { get; set; }
        public int studentId { get; set; }
        public string description { get; set; }
    }
}