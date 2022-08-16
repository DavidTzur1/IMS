

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VPNServer.Contracts;
using VPNServer.Dto;

namespace VPN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreeningItemController : ControllerBase
    {
        private readonly IScreeningItemRepository _reposity;
        private readonly ILogger<ScreeningItemController> logger;
        public ScreeningItemController(IScreeningItemRepository reposity, ILogger<ScreeningItemController> logger)
        {
            _reposity = reposity;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetScreeningItemByCompanyId(int screeningId)
        {
            try
            {
                var screeningsItem = await _reposity.GetScreeningItemByScreeningId(screeningId);
                return Ok(screeningsItem);

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}", Name = "ScreeningItemById")]
        public async Task<IActionResult> GetScreeningItem(int id)
        {
            try
            {
                var screeningItem = await _reposity.GetScreeningItem(id);
                if (screeningItem == null)
                    return NotFound();
                return Ok(screeningItem);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateScreeningItem(ScreeningItemForCreationDto screeningItem)
        {
            try
            {
                var createdScreeningItem = await _reposity.CreateScreeningItem(screeningItem);
                return CreatedAtRoute("ScreeningItemById", new { id = createdScreeningItem.Id }, createdScreeningItem);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateScreeningItem(int id, ScreeningItemForUpdateDto screeningItem)
        {
            try
            {
                var dbScreeningItem = await _reposity.GetScreeningItem(id);
                if (dbScreeningItem == null)
                    return NotFound();
                await _reposity.UpdateScreeningItem(id, screeningItem);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScreeningItem(int id)
        {
            try
            {
                var dbscreeningData = await _reposity.GetScreeningItem(id);
                if (dbscreeningData == null)
                    return NotFound();
                await _reposity.DeleteScreeningItem(id);
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



