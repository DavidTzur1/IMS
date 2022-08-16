

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VPNServer.Contracts;
using VPNServer.Dto;

namespace VPNServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PABXsController : ControllerBase
    {
        private readonly IPABXRepository _reposity;
        private readonly ILogger<PABXsController> logger;
        public PABXsController(IPABXRepository reposity, ILogger<PABXsController> logger)
        {
            _reposity = reposity;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetPABXsByGroupId(int groupId)
        {
            try
            {
                var pabxs = await _reposity.GetPABXsByGroupId(groupId);
                return Ok(pabxs);

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}", Name = "PABXById")]
        public async Task<IActionResult> GetPABX(int id)
        {
            try
            {
                var pabx = await _reposity.GetPABX(id);
                if (pabx == null)
                    return NotFound();
                return Ok(pabx);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePABX(PABXForCreationDto pabx)
        {
            try
            {
                var createdPABX = await _reposity.CreatePABX(pabx);
                return CreatedAtRoute("PABXById", new { id = createdPABX.Id }, createdPABX);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePABX(int id, PABXForUpdateDto pabx)
        {
            try
            {
                var dbPABX = await _reposity.GetPABX(id);
                if (dbPABX == null)
                    return NotFound();
                await _reposity.UpdatePABX(id, pabx);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePABX(int id)
        {
            try
            {
                var dbpabx = await _reposity.GetPABX(id);
                if (dbpabx == null)
                    return NotFound();
                await _reposity.DeletePABX(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }


    }

}


