﻿
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MODELS.Models
{
    public class Job
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }
        public string title { get; set; }
        public string city { get; set; }
        public string description { get; set; }
        public string requirements { get; set; }
        public string experience { get; set; }
        public virtual ICollection<CV> applicant { get; set; }

    }
}