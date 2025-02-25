using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP02.Models
{
    public class Users
    {
        public int id { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public int role { get; set; }
    }
}