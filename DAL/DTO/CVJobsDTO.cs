using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class CVJobsDTO
    {
        public long cvJobsId { get; set; }
        public long userId { get; set; }
       
        public long jobId { get; set; }
    }
}
