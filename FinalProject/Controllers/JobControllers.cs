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
        [HttpGet("job/{id}", Name = "Get")]
        public async Task<Job> Get(long id)
        {
            Job job = await _dbJob.GetJob(id);
            return job;

        }

        [HttpGet("jobs/{id1}", Name = "GetAllJobs")]
        public async Task<IEnumerable<Job>> GetAllJobs(long id1)
        {
            var jobs = await _dbJob.GetAllJobs(id1);
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
