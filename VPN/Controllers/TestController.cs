using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VPN.Contracts;

namespace VPN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IVPNRepository _reposity;
        private readonly ILogger<TestController> logger;
        public TestController(IVPNRepository reposity, ILogger<TestController> logger)
        {
            _reposity = reposity;
            this.logger = logger;   
        }
        [HttpGet("GetUserByCLI")]
        public async Task<IActionResult> GetUserByCLI(string cli="0544180550")
        {
            try
            {
                var user = await _reposity.GetUserByCLI(cli);
                if (user == null) return NotFound();
                return Ok(user);
            }

            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("GetRangeByPrivateRange")]
        public async Task<IActionResult> GetRangeByPrivateRange(int companyID=1, string number="054")
        {
            try
            {
                var range = await _reposity.GetRangeByPrivateRange(companyID,number);
                if (range == null) return NotFound();
                return Ok(range);
            }

            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("GetRangeByCLIRange")]
        public async Task<IActionResult> GetRangeByCLIRange(int companyID = 1, string number = "054")
        {
            try
            {
                var range = await _reposity.GetRangeByCLIRange(companyID, number);
                if (range == null) return NotFound();
                return Ok(range);
            }

            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("GetScreeningDataByNumber")]
        public async Task<IActionResult> GetScreeningDataByNumber(string screeningIDs, string number)
        {
            try
            {
                var range = await _reposity.GetScreeningDataByNumber(screeningIDs, number);
                if (range == null) return NotFound();
                return Ok(range);
            }

            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("GetUserByPrivateNumber")]
        public async Task<IActionResult> GetUserByPrivateNumber(int companyID, string number)
        {
            try
            {
                var user = await _reposity.GetUserByPrivateNumber(companyID, number);
                if (user == null) return NotFound();
                return Ok(user);
            }

            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetUserByCalledNumber")]
        public async Task<IActionResult> GetUserByCalledNumber(int companyID, string number)
        {
            try
            {
                var user = await _reposity.GetUserByCalledNumber(companyID, number);
                if (user == null) return NotFound();
                return Ok(user);
            }

            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
    }
}
