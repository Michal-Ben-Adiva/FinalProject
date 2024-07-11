using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MODELS.Models
{
    public class ModelsContext : DbContext
    {
        public ModelsContext(DbContextOptions<ModelsContext> options) : base(options)
        {
        }
        public DbSet<CV> cv { get; set; }
        public DbSet<Users> users { get; set; }
        public DbSet<Job> jobs { get; set; }
    }
}
