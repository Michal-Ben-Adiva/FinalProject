using DAL.DTO;
using DAL.Interfaces;
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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CVDTO value)
        {
            bool creat = await _dbCV.CreateCV(value);
            if (creat)
                return Ok();
            return BadRequest();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            bool delete = await _dbCV.DeleteCV(id);
            if (delete == true)
                return Ok();
            return BadRequest();
        }
        [HttpGet("CV/{id}")]
        public async Task<CV> GetCVByID(long id)
        {
            CV cv = await _dbCV.GetCVByID(id);
            return cv;
        }

        [HttpGet("AllCV")]
        public async Task<IEnumerable<CV>> GetAllCV()
        {
            var cv = await _dbCV.GetAllCV();
            return cv;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] CVDTO value)
        {
            bool update = await _dbCV.UpdateCV(id, value);
            if (update)
                return Ok();
            return BadRequest();
        }


    }
}
