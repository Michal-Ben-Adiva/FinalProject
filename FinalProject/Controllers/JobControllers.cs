using DAL.DTO;
using DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODELS.Models;
using Microsoft.EntityFrameworkCore;


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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] JobDTO value)
        {
            bool creat = await _dbJob.CreateJob(value);
            if (creat)
                return Ok();
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            bool delete = await _dbJob.DeleteJob(id);
            if (delete)
                return Ok();
            return BadRequest();
        }
        [HttpGet("job/{id}", Name = "GetJob")]
        public async Task<Job> GetJob(long id)
        {
            Job job = await _dbJob.GetJob(id);
            return job;

        }

        [HttpGet("AllJobsById/{id}", Name = "GetAllJobsById")]
        public async Task<IEnumerable<Job>> GetAllJobsById(long id)
        {
            var jobs = await _dbJob.GetAllJobsById(id);
            return jobs;
        }

        [HttpGet("AllJobs")]
        public async Task<IEnumerable<Job>> GetAllJobs()
        {
            var jobs = await _dbJob.GetAllJobs();
            return jobs;
        }

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
