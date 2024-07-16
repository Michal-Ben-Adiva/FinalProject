using AutoMapper;
using DAL.DTO;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using MODELS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class JobData : IJob
    {
        private readonly ModelsContext _Context;
        private readonly IMapper _mapper;

        public JobData(ModelsContext context, IMapper mapper)
        {
            _Context = context;
            _mapper = mapper;
        }
        public async Task<bool> CreateJob(JobDTO j)
        {
            await _Context.jobs.AddAsync(_mapper.Map<Job>(j));
            await _Context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteJob(long id)
        {
            Job j = await _Context.jobs.FindAsync(id);
            if (j == null)
            {
                return false;
            }
            _Context.jobs.Remove(j);
            await _Context.SaveChangesAsync();
            return true;
        }
        public async Task<Job> GetJob(long id)
        {
            Job j = await _Context.jobs.FindAsync(id);
            if (j == null)
            {
                return null;
            }
            return j;
        }
        public async Task<IEnumerable<Job>> GetAllJobs(long id)
        {
            var c = await _Context.jobs.Where(x => x.userId == id).ToListAsync();
            if (c == null)
            {
                return null; ;

            }
            return c;
        }
        public async Task<bool> UpdateJob(long id, JobDTO updatejob)
        {
            Job currentjob = await _Context.jobs.FindAsync(id);
            if (currentjob == null)
            {
                return false;
            }
            currentjob.title = updatejob.title;
            currentjob.description = updatejob.description;
            currentjob.requirements = updatejob.requirements;
            currentjob.jobId = id;
            currentjob.userId = updatejob.userId;
            currentjob.city = updatejob.city;
            currentjob.experience = updatejob.experience;

            await _Context.SaveChangesAsync();
            return true;
        }

    }
}
