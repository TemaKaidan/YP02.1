using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP02.Models
{
    public class Students
    {
        [Key]
        public int id { get; set; }
        public string surname { get; set; }
        public string name { get; set; }
        public string lastname { get; set; }
        public int studGroupId { get; set; }
        public DateTime dateOfRemand { get; set; }
        public int userId { get; set; }


        /*public StudGroups StudGroup { get; set; }*/  // Навигационное свойство
    }
}