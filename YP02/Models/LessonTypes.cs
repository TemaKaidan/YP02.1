using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP02.Models
{
    public class LessonTypes
    {
        [Key]
        public int id { get; set; }
        public string typeName { get; set; }
    }
}