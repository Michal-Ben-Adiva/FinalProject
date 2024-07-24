using DAL.DTO;
using DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MODELS.Models;

namespace FinalProject.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CVJobsControllers : ControllerBase
    {

        private readonly ICVJobs _dbCV;
        public CVJobsControllers(ICVJobs cv)
        {
            _dbCV = cv;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CVJobsDTO value)
        {
            bool creat = await _dbCV.CreateCVJobs(value);
            if (creat)
                return Ok();
            return BadRequest();
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            bool delete = await _dbCV.DeleteCVJobs(id);
            if (delete == true)
                return Ok();
            return BadRequest();
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IEnumerable<CVJobs>> Get(long id)
        {
            var cv = await _dbCV.GetCVJobs(id);
            return cv;
        }

    }
}
