using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VPN.Contracts;

namespace VPN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IVPNRepository _reposity;
        private readonly ILogger<GroupsController> logger;
        public GroupsController(IVPNRepository reposity, ILogger<GroupsController> logger)
        {
            _reposity = reposity;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetGroups(int companyID)
        {
            try
            {
                var groups = await _reposity.GetGroups(companyID);
                return Ok(groups);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
    }
}
