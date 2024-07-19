using DAL.DTO;
using MODELS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IJob
    {
        Task<bool> CreateJob(JobDTO j);
        Task<bool> DeleteJob(long id);
        Task<Job> GetJob(long id);
        Task<bool> UpdateJob(long id, JobDTO updatejob);
        Task<IEnumerable<Job>> GetAllJobsById(long id);
        Task<IEnumerable<Job>> GetAllJobs();
    }
}
