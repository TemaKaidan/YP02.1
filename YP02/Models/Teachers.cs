using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP02.Models
{
    public class Teachers
    {
        [Key]
        public int id { get; set; }
        public string surname { get; set; }
        public string name { get; set; }
        public string lastname { get; set; }
        public int userId { get; set; }

        public ICollection<TeachersLoad> TeachersLoads { get; set; }
    }
}