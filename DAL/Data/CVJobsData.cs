using AutoMapper;
using DAL.DTO;
using DAL.Interfaces;
using MODELS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace DAL.Data
{
    public class CVJobsData : ICVJobs
    {
        private readonly ModelsContext _Context;
        private readonly IMapper _mapper;

        public CVJobsData(ModelsContext context, IMapper mapper)
        {
            _Context = context;
            _mapper = mapper;
        }
        public async Task<bool> CreateCVJobs(CVJobsDTO c)
        {
            await _Context.cvjobs.AddAsync(_mapper.Map<CVJobs>(c));
            await _Context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteCVJobs(long id)
        {
            CVJobs c = await _Context.cvjobs.FindAsync(id);
            if (c == null)
            {
                return false;
            }
            _Context.cvjobs.Remove(c);
            await _Context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<CVJobs>> GetCVJobs(long id)
        {
            var c = await _Context.cvjobs.Where(x => x.jobId == id).ToListAsync();
            if (c == null)
            {
                return null;
            }
            return c;
        }
  

    }
}
