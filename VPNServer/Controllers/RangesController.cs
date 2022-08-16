
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VPNServer.Contracts;
using VPNServer.Dto;

namespace VPNServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RangesController : ControllerBase
    {
        private readonly IRangeRepository _reposity;
        private readonly ILogger<RangesController> logger;
        public RangesController(IRangeRepository reposity, ILogger<RangesController> logger)
        {
            _reposity = reposity;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetRangesByPABXId(int pabxId)
        {
            try
            {
                var ranges = await _reposity.GetRangesByPABXId(pabxId);
                return Ok(ranges);

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}", Name = "RangeById")]
        public async Task<IActionResult> GetRange(int id)
        {
            try
            {
                var range = await _reposity.GetRange(id);
                if (range == null)
                    return NotFound();
                return Ok(range);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateRange(RangeForCreationDto range)
        {
            try
            {
                var createdRange = await _reposity.CreateRange(range);
                return CreatedAtRoute("RangeById", new { id = createdRange.Id }, createdRange);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRange(int id, RangeForUpdateDto range)
        {
            try
            {
                var dbrange = await _reposity.GetRange(id);
                if (dbrange == null)
                    return NotFound();
                await _reposity.UpdateRange(id, range);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRange(int id)
        {
            try
            {
                var dbrange = await _reposity.GetRange(id);
                if (dbrange == null)
                    return NotFound();
                await _reposity.DeleteRange(id);
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




