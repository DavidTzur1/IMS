
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VPNServer.Contracts;
using VPNServer.Dto;

namespace VPNServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupRepository _reposity;
        private readonly ILogger<GroupsController> logger;
        public GroupsController(IGroupRepository reposity, ILogger<GroupsController> logger)
        {
            _reposity = reposity;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetGroupsByCompanyId(int companyId)
        {
            try
            {
                var groups = await _reposity.GetGroupsByCompanyId(companyId);
                return Ok(groups);

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}", Name = "GroupById")]
        public async Task<IActionResult> GetGroup(int id)
        {
            try
            {
                var group = await _reposity.GetGroupById(id);
                if (group == null)
                    return NotFound();
                return Ok(group);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroup(GroupForCreationDto group)
        {
            try
            {
                var createdGroup = await _reposity.CreateGroup(group);
                return CreatedAtRoute("GroupById", new { id = createdGroup.Id}, createdGroup);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGroup(int id, GroupForUpdateDto group)
        {
            try
            {
                var dbGroup = await _reposity.GetGroupById(id);
                if (dbGroup == null)
                    return NotFound();
                await _reposity.UpdateGroup(id, group);
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
                var dbgroup = await _reposity.GetGroupById(id);
                if (dbgroup == null)
                    return NotFound();
                await _reposity.DeleteGroup(id);
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

