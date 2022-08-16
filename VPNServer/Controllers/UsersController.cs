

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VPNServer.Contracts;
using VPNServer.Dto;

namespace VPNServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _reposity;
        private readonly ILogger<UsersController> logger;
        public UsersController(IUserRepository reposity, ILogger<UsersController> logger)
        {
            _reposity = reposity;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersByPABXId(int pabxId)
        {
            try
            {
                var users = await _reposity.GetUsersByPABXId(pabxId);
                return Ok(users);

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}", Name = "UserById")]
        public async Task<IActionResult> GetUser(int id)
        {
            try
            {
                var pabx = await _reposity.GetUser(id);
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

        [HttpGet("ByCLI/{cli}", Name = "UserByCLI")]
        public async Task<IActionResult> GetUserByCLI(string cli)
        {
            try
            {
                var pabx = await _reposity.GetUserByCLI(cli);
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
        public async Task<IActionResult> CreateUser(UserForCreationDto user)
        {
            try
            {
                var createdUser = await _reposity.CreateUser(user);
                return CreatedAtRoute("UserById", new { id = createdUser.Id }, createdUser);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserForUpdateDto user)
        {
            try
            {
                var dbpabx = await _reposity.GetUser(id);
                if (dbpabx == null)
                    return NotFound();
                await _reposity.UpdateUser(id, user);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var dbpabx = await _reposity.GetUser(id);
                if (dbpabx == null)
                    return NotFound();
                await _reposity.DeleteUser(id);
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



