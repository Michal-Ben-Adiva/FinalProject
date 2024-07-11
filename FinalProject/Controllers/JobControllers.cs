using DAL.DTO;
using DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODELS.Models;

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
        //[Route("api/JobControllers/Post/{id}")]
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
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            Job job = await _dbJob.GetJob(id);
            if (job == null)
                return BadRequest();
            return Ok();
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
