using DAL.DTO;
using MODELS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ICVJobs
    {
        Task<bool> CreateCVJobs(CVJobsDTO c);
        Task<bool> DeleteCVJobs(long id);
        Task<IEnumerable<CVJobs>> GetCVJobs(long id);

    }
}
