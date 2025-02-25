using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP02.Models
{
    public class Roles
    {
        [Key]
        public int id { get; set; }
        public string roleName { get; set; }
    }
}