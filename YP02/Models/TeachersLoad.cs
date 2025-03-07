﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP02.Models
{
    public class TeachersLoad
    {
        [Key]
        public int id { get; set; }
        public int teacherId { get; set; }
        public int disciplineId { get; set; }
        public int studGroupId { get; set; }
        public int lectureHours { get; set; }
        public int practiceHours { get; set; }
        public int сonsultationHours { get; set; }
        public int courseprojectHours { get; set; }
        public int examHours { get; set; }

        public Teachers Teacher { get; set; }
    }
}