using MODELS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class JobDTO
    {
        public long id { get; set; }
        public string title { get; set; }
        public string city { get; set; }
        public string description { get; set; }
        public string requirements { get; set; }
        public string experience { get; set; }
        public virtual ICollection<CV> applicant { get; set; }
    }
}
