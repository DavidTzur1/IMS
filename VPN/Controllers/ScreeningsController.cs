using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VPN.Contracts;
using VPN.DTO;

namespace VPN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreeningsController : ControllerBase
    {
        private readonly IScreeningRepository _reposity;
        private readonly ILogger<ScreeningsController> logger;
        public ScreeningsController(IScreeningRepository reposity, ILogger<ScreeningsController> logger)
        {
            _reposity = reposity;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetScreeningsByCompanyID(int companyID)
        {
            try
            {
                var screenings = await _reposity.GetScreeningsByCompanyID(companyID);
                return Ok(screenings);

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}", Name = "ScreeningById")]
        public async Task<IActionResult> GetScreening(int id)
        {
            try
            {
                var screening = await _reposity.GetScreening(id);
                if (screening == null)
                    return NotFound();
                return Ok(screening);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateScreening(ScreeningForCreationDto screening)
        {
            try
            {
                var createdGroup = await _reposity.CreateScreening(screening);
                return CreatedAtRoute("ScreeningById", new { id = createdGroup.ID }, createdGroup);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateScreening(int id, ScreeningForUpdateDto screening)
        {
            try
            {
                var dbScreening = await _reposity.GetScreening(id);
                if (dbScreening == null)
                    return NotFound();
                await _reposity.UpdateScreening(id, screening);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScreening(int id)
        {
            try
            {
                var dbscreening = await _reposity.GetScreening(id);
                if (dbscreening == null)
                    return NotFound();
                await _reposity.DeleteScreening(id);
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

