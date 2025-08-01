﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Experience
    {
        [Key]  // ID için key atamsı yapıyoruz. ID birincil anahtar olması için
        public int? ExperienceId { get; set; }
        public string? Name { get; set; }
        public string? Date { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
    }
}
