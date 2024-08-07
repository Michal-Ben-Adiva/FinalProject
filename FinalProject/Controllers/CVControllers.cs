using DAL.DTO;
using DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODELS.Models;

namespace FinalProject.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CVControllers : ControllerBase
    {
        private readonly ICV _dbCV;
        public CVControllers(ICV cv)
        {
            _dbCV = cv;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CVDTO value)
        {
            bool creat = await _dbCV.CreateCV(value);
            if (creat)
                return Ok();
            return BadRequest();
        }
        [Authorize]
        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(string userId)
        {
            bool delete = await _dbCV.DeleteCV(userId);
            if (delete == true)
                return Ok();
            return BadRequest();
        }
        [Authorize]
        [HttpGet("CV/{userId}")]
        public async Task<CV> GetCVByID(string userId)
        {
            CV cv = await _dbCV.GetCVByID(userId);
            return cv;
        }
        [Authorize]
        [HttpGet("AllCV")]
        public async Task<IEnumerable<CV>> GetAllCV()
        {
            var cv = await _dbCV.GetAllCV();
            return cv;
        }
        [Authorize]
        [HttpPut("{userId}")]
        public async Task<IActionResult> Put(string userId, [FromBody] CVDTO value)
        {
            bool update = await _dbCV.UpdateCV(userId, value);
            if (update)
                return Ok();
            return BadRequest();
        }


    }
}
