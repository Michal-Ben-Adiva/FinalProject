using DAL.DTO;
using DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODELS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace FinalProject.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class JobControllers : ControllerBase
    {
        private readonly IJob _dbJob;
        public JobControllers(IJob job)
        {
            _dbJob = job;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] JobDTO value)
        {
            bool creat = await _dbJob.CreateJob(value);
            if (creat)
                return Ok();
            return BadRequest();
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            bool delete = await _dbJob.DeleteJob(id);
            if (delete)
                return Ok();
            return BadRequest();
        }
        [Authorize]
        [HttpGet("job/{id}", Name = "GetJob")]
        public async Task<Job> GetJob(long id)
        {
            Job job = await _dbJob.GetJob(id);
            return job;

        }
        [Authorize]
        [HttpGet("AllJobsById/{id}", Name = "GetAllJobsById")]
        public async Task<IEnumerable<Job>> GetAllJobsById(string userId)
        {
            var jobs = await _dbJob.GetAllJobsById(userId);
            return jobs;
        }
        [Authorize]
        [HttpGet("AllJobs")]
        public async Task<IEnumerable<Job>> GetAllJobs()
        {
            var jobs = await _dbJob.GetAllJobs();
            return jobs;
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] JobDTO value)
        {
            bool update = await _dbJob.UpdateJob(id, value);
            if (update)
                return Ok();
            return BadRequest();
        }




    }
}
