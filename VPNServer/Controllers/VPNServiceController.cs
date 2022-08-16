using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VPNServer.Contracts;

namespace VPNServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VPNServiceController : ControllerBase
    {
        private readonly IVPNServiceRepository _reposity;
        private readonly ILogger<VPNServiceController> logger;
        public VPNServiceController(IVPNServiceRepository reposity, ILogger<VPNServiceController> logger)
        {
            _reposity = reposity;
            this.logger = logger;
        }

        [HttpGet("UserModelByCLI/{cli}")]
        public async Task<IActionResult> GetUserModelByCLI(string cli)
        {
            try
            {
                var user = await _reposity.GetUserModelByCLI(cli);
                if (user == null)
                    return NotFound();
                return Ok(user);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("BestMatchScreeningItem/")]
        public async Task<IActionResult> GetBestMatchScreeningItemByNumber(string screeningIds,string number)
        {
            try
            {
                var item = await _reposity.BestMatchScreeningItemByNumber(screeningIds, number);
                if (item == null)
                    return NotFound();
                return Ok(item);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("UserModelByCalledNumber/")]
        public async Task<IActionResult> GetUserModelByCalledNumber(int companyId, string dn)
        {
            try
            {
                var item = await _reposity.GetUserModelByCalledNumber(companyId,dn);
                if (item == null)
                    return NotFound();
                return Ok(item);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("UserModelByPrivateNumber/")]
        public async Task<IActionResult> GetUserModelByPrivateNumber(int companyId, string dn)
        {
            try
            {
                var item = await _reposity.GetUserModelByPrivateNumber(companyId, dn);
                if (item == null)
                    return NotFound();
                return Ok(item);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("RangeModelByPublicRange/")]
        public async Task<IActionResult> GetRangeModelByPublicRange(int companyId, string dn)
        {
            try
            {
                var item = await _reposity.GetRangeModelByPublicRange(companyId, dn);
                if (item == null)
                    return NotFound();
                return Ok(item);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("RangeModelByPrivateRange/")]
        public async Task<IActionResult> GetRangeModelByPrivateRange(int companyId, string dn)
        {
            try
            {
                var item = await _reposity.GetRangeModelByPrivateRange(companyId, dn);
                if (item == null)
                    return NotFound();
                return Ok(item);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
    }
}
