using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PyggApi.Data;
using PyggApi.Interfaces.Groups;

namespace PyggApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroups _groupBusiness;

        public GroupsController(IGroups groupBusiness)
        {
            _groupBusiness = groupBusiness;
        }
        //readonly ApiDbContext _dbContext = new();

        [HttpGet("GetAllGroups")]
        [Authorize]
        public async Task<ActionResult<List<Group>>> GetAllGroups()
        {
            return await _groupBusiness.GetAllGroups();
        }

        [HttpGet("GetGroupsByUserId")]
        [Authorize]
        public async Task<ActionResult<List<GroupMemberDetail>>> GetGroupsByUserId(int userId)
        {
            return await _groupBusiness.GetGroupsByUserId(userId);
        }


        [HttpGet("GetGroupById")]
        [Authorize]
        public async Task<ActionResult<Group>> GetGroupById(int id)
        {
            var result = await _groupBusiness.GetGroupById(id);
            if (result is null)
                return NotFound("Hero not found.");

            return Ok(result);
        }

        [HttpGet("GetGroupContributionByGroupId")]
        [Authorize]
        public async Task<ActionResult<GroupContribution>> GetGroupContributionByGroupId(int groupId)
        {
            var result = await _groupBusiness.GetGroupContributionByGroupId(groupId);
            if (result is null)
                return NotFound("Hero not found.");

            return Ok(result);
        }

        [HttpGet("GetGroupRobinsByGroupId")]
        [Authorize]
        public async Task<ActionResult<GroupContribution>> GetGroupRobinsByGroupId(int groupId)
        {
            var result = await _groupBusiness.GetGroupContributionByGroupId(groupId);
            if (result is null)
                return NotFound("Hero not found.");

            return Ok(result);
        }


        [HttpGet("GetGroupFinesByGroupId")]
        [Authorize]
        public async Task<ActionResult<GroupContribution>> GetGroupFinesByGroupId(int groupId)
        {
            var result = await _groupBusiness.GetGroupFinesByGroupId(groupId);
            if (result is null)
                return NotFound("Hero not found.");

            return Ok(result);
        }


        [HttpPost("AddGroup")]
        [Authorize]
        public async Task<ActionResult<List<Group>>> AddGroup(Group group)
        {
            var result = await _groupBusiness.AddGroup(group);
            return Ok(result);
        }

        [HttpPut("UpdateGroup")]
        [Authorize]
        public async Task<ActionResult<List<Group>>> UpdateGroup(int id, Group request)
        {
            var result = await _groupBusiness.UpdateGroup(id, request);
            if (result is null)
                return NotFound("Hero not found.");

            return Ok(result);
        }

        [HttpDelete("DeleteGroup")]
        [Authorize]
        public async Task<ActionResult<List<Group>>> DeleteGroup(int id)
        {
            var result = await _groupBusiness.DeleteGroup(id);
            if (result is null)
                return NotFound("Hero not found.");

            return Ok(result);
        }

    }
}
