

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VPN.Contracts;
using VPN.DTO;

namespace VPN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreeningDataController : ControllerBase
    {
        private readonly IScreeningDataRepository _reposity;
        private readonly ILogger<ScreeningDataController> logger;
        public ScreeningDataController(IScreeningDataRepository reposity, ILogger<ScreeningDataController> logger)
        {
            _reposity = reposity;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetScreeningDataByCompanyID(int screeningID)
        {
            try
            {
                var screeningsData = await _reposity.GetScreeningDataByScreeningID(screeningID);
                return Ok(screeningsData);

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}", Name = "ScreeningDataById")]
        public async Task<IActionResult> GetScreeningData(int id)
        {
            try
            {
                var screeningData = await _reposity.GetScreeningData(id);
                if (screeningData == null)
                    return NotFound();
                return Ok(screeningData);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateScreeningData(ScreeningDataForCreationDto screeningData)
        {
            try
            {
                var createdScreeningData = await _reposity.CreateScreeningData(screeningData);
                return CreatedAtRoute("ScreeningDataById", new { id = createdScreeningData.ID }, screeningData);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateScreeningData(int id, ScreeningDataForUpdateDto screeningData)
        {
            try
            {
                var dbScreeningData = await _reposity.GetScreeningData(id);
                if (dbScreeningData == null)
                    return NotFound();
                await _reposity.UpdateScreeningData(id, screeningData);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScreeningData(int id)
        {
            try
            {
                var dbscreeningData = await _reposity.GetScreeningData(id);
                if (dbscreeningData == null)
                    return NotFound();
                await _reposity.DeleteScreeningData(id);
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


