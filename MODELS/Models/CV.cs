using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.Models
{
    public class CV
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string gmail { get; set; }
        public int phone { get; set; }
        public string profile { get; set; }
        public string skills { get; set; }
        public string practicalExperience { get; set; }
        public string education { get; set; }
        public string languages { get; set; }
        

    }
}
