using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.Models
{
    public class CVJobs
    {
        [Key]
        public long cvJobsId { get; set; }

        [ForeignKey("cvId")]
        public string userId { get; set; }

        [ForeignKey("jobId")]
        public long jobId { get; set; }


    }
}
