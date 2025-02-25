using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP02.Models
{
    public class ConsultationResults
    {
        [Key]
        public int id { get; set; }
        public int consultationId { get; set; }
        public int studentId { get; set; }
        //ХЗ какой тип данных
        public bool presence { get; set; }
        public int submittedPractice { get; set; }
    }
}