using MODELS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class JobDTO
    {
       
        public long jobId { get; set; }
        public string title { get; set; }
        public string city { get; set; }
        public string description { get; set; }
        public string requirements { get; set; }
        public string experience { get; set; }
     
        public string userId { get; set; }
    }
}
