using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VPN.Contracts;
using VPN.DTO;

namespace VPN.Controllers
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
        public async Task<IActionResult> GetPABXsByGroupID(int groupID)
        {
            try
            {
                var pabxs = await _reposity.GetPABXsByGroupID(groupID);
                return Ok(pabxs);

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}", Name = "PABXById")]
        public async Task<IActionResult> GetGroup(int id)
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
        public async Task<IActionResult> CreatePABX(PABXDTO pabx)
        {
            try
            {
                var createdPABX = await _reposity.CreatePABX(pabx);
                return CreatedAtRoute("PABXById", new { id = createdPABX.ID }, createdPABX);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePABX(int id, PABXDTO pabx)
        {
            try
            {
                var dbpabx = await _reposity.GetPABX(id);
                if (dbpabx == null)
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
        public async Task<IActionResult> DeleteGroup(int id)
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

