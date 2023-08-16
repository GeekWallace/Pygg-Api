using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PyggApi.Business.Groups;
using PyggApi.Interfaces.Groups;
using PyggApi.Interfaces.Members;
using System.Threading.Tasks;

namespace PyggApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembersController : ControllerBase
    {
        private readonly IMembers _memberBusiness;

        public MembersController(IMembers memberBusiness)
        {
            _memberBusiness = memberBusiness;
        }

        [HttpGet("AllMembersbyGroupId")]
        [Authorize]
        public async Task<ActionResult<Member>> AllMembersbyGroupId(int groupId)
        {
            var result = await _memberBusiness.GetAllMembersbyGroupId(groupId);
            if (result is null)
                return NotFound("Hero not found.");

            return Ok(result);
        }

        [HttpGet("GetMembersbyGroupId")]
        [Authorize]
        public async Task<ActionResult<MemberAccountDetails>> GetMembersbyGroupId(int id)
        {
            var result = await _memberBusiness.GetMembersbyGroupId(id);
            if (result is null)
                return NotFound("Member not found.");

            return Ok(result);
        }


        [HttpGet("MemberAccountDetailsByGroup")]
        [Authorize]
        public async Task<ActionResult<MemberAccountDetails>> GetMemberAccountDetailsByGroup(int groupId, int memberNumber, int userId)
        {
            var result = await _memberBusiness.GetMemberAccountDetailsByGroup(groupId, memberNumber, userId);
            if (result is null)
                return NotFound("Member not found.");

            return Ok(result);
        }

        [HttpGet("MemberAccountDetailsAllGroups")]
        [Authorize]
        public async Task<ActionResult<MemberAccountDetails>> GetMemberAccountDetailsAllGroups(int memberNumber)
        {
            var result = await _memberBusiness.GetMemberAccountDetailsAllGroups(memberNumber);
            if (result is null)
                return NotFound("Member not found.");

            return Ok(result);
        }

        [HttpGet("GetServerTime")]
        [Authorize]
        public IActionResult GetServerTime()
        {
            DateTime serverTimeUtc = DateTime.UtcNow;
            //TimeZoneInfo eastAfricaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Africa/Nairobi");
            TimeZoneInfo eastAfricaTimeZone = TimeZoneInfo.FindSystemTimeZoneById(" E.Africa Standard Time");
            DateTime eastAfricaTime = TimeZoneInfo.ConvertTimeFromUtc(serverTimeUtc, eastAfricaTimeZone);

            return Ok(eastAfricaTime);
        }

        [HttpPost("AddFineTransaction")]
        [Authorize]
        public async Task<ActionResult<List<MemberTransaction>>> AddFineTransaction(MemberTransaction memberTransaction)
        {
            var result = await _memberBusiness.AddFineTransaction(memberTransaction);
            return Ok(result);
        }



        [HttpPost("AddMemberTransaction")]
        [Authorize]
        public async Task<ActionResult<List<MemberTransaction>>> AddMemberTransaction(MemberTransaction memberTransaction)
        {
            var result = await _memberBusiness.AddMemberTransaction(memberTransaction);
            return Ok(result);
        }


        [HttpGet("RunOctopusDeployment")]
        [Authorize]
        public async Task<ActionResult<Octopus>> RunOctopusDeployment(DateTime startDate, int groupId, string contributionType)
        {
            try
            {
                DateTime serverTimeUtc = Convert.ToDateTime(startDate);
                TimeZoneInfo eastAfricaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. Africa Standard Time");
                DateTime eastAfricaTime = TimeZoneInfo.ConvertTimeFromUtc(serverTimeUtc, eastAfricaTimeZone);
                var result =  await _memberBusiness.RunOctopusDeployment(startDate, groupId, contributionType);
                return Ok(result);
                //return Ok("Deployment completed successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred during deployment: " + ex.Message);
            }
        }

        [HttpGet("DisplayRoinSchedule")]
        [Authorize]
        public async Task<ActionResult<Octopus>> DisplayRoinSchedule()
        {
            try
            {
                //DateTime serverTimeUtc = Convert.ToDateTime(startDate);
                //TimeZoneInfo eastAfricaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. Africa Standard Time");
                //DateTime eastAfricaTime = TimeZoneInfo.ConvertTimeFromUtc(serverTimeUtc, eastAfricaTimeZone);
                var result = await _memberBusiness.DisplayRoinSchedule();
                return Ok(result);
                //return Ok("Deployment completed successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred during deployment: " + ex.Message);
            }
        }

        [HttpPost("UpdateMemberAccountsFromInstallments")]
        [Authorize]
        public async Task<ActionResult> UpdateMemberAccountsFromInstallments()
        {
            try
            {
                await _memberBusiness.UpdateMemberAccountsFromInstallments();
                return Ok("Update completed successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred during update: " + ex.Message);
            }
        }

        [HttpPost("UpdateMemberPendingContributionsFromInstallments")]
        [Authorize]
        public async Task<ActionResult> UpdateMemberPendingContributionsFromInstallments(int groupId)
        {
            try
            {
                await _memberBusiness.UpdateMemberPendingContributionsFromInstallments(groupId);
                return Ok("Update completed successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred during update: " + ex.Message);
            }
        }




        [HttpGet("CheckMemberJoinFeeBalance")]
        [Authorize]
        public async Task<ActionResult> CheckMemberJoinFeeBalance(int groupId, int memberId, int userId)
        {
            try
            {
                await _memberBusiness.CheckMemberJoinFeeBalance(groupId, memberId, userId);
                return Ok("Check completed successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred during check: " + ex.Message);
            }
        }


        [HttpGet("GetGroupMemberRoles")]
        [Authorize]
        public async Task<ActionResult<int>> GetGroupMemberRoles(int groupId, int memberNumber, int userId)
        {
            var result = await _memberBusiness.GetGroupMemberRoles(groupId, memberNumber, userId);
            if (result == 0)
                return NotFound("does not exist.");

            return Ok(result);
        }

   

        [HttpPost("PostFines")]
        [Authorize]
        public async Task<ActionResult> PostFines()
        {
            try
            {
                await _memberBusiness.PostFines();
                return Ok("Post completed successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred during post: " + ex.Message);
            }
        }

    }
}
