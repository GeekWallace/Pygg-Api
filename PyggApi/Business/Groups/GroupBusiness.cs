using Microsoft.EntityFrameworkCore;
using PyggApi.Interfaces.Groups;
using PyggApi.Models;

namespace PyggApi.Business.Groups
{
    public class GroupBusiness : IGroups
    {
        private readonly ApiDbContext _context;

        public GroupBusiness(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<List<Group>> GetAllGroups()
        {
            var groups = await _context.Groups.ToListAsync();
            return groups;
        }
        public async Task<List<GroupMemberDetail>> GetGroupsByUserId(int userId)
        {
   
            var groups =  from a in _context.Groups
                                join b in _context.GroupMembers on a.GroupId equals b.GroupId
                                where b.GroupMemberUserId == userId && b.IsActive == true
                                select new GroupMemberDetail
                                {
                                    GroupId = a.GroupId,
                                    GroupMemberId = b.GroupMemberId,
                                    GroupMemberUserId = b.GroupMemberUserId,
                                    GroupMemberRoleId = b.GroupMemberRoleId,
                                    IsActive = b.IsActive,
                                    GroupMemberUserRoleId = b.GroupMemberUserRoleId,    
                                    GroupName = a.GroupName,
                                    GroupMaxAmount = a.GroupMaxAmount,
                                    GroupMaxMembers = a.GroupMaxMembers,
                                    GroupStartDate = a.GroupStartDate,
                                    GroupEndDate = a.GroupEndDate,
                                    GroupPyggBalance = a.GroupPyggBalance,
                                    GroupCreatedOn = a.GroupCreatedOn,
                                    IsGroupClosed = a.IsGroupClosed,
                                    AvailableBalance = a.AvailableBalance,
                                    CurrentBalance = a.CurrentBalance,
                                    ExpensesAmount = a.ExpensesAmount,
                                    GroupComments = a.GroupComments
                                };

            return await groups.ToListAsync();
        }


        public async Task<Group> GetGroupById(int id)
        {
            var singleGroup = await _context.Groups.FindAsync(id);
            if (singleGroup is null)
                return null;

            return singleGroup;
        }


        public async Task<List<Group>> AddGroup(Group group)
        {
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();
            return await _context.Groups.ToListAsync();
        }

        public async Task<List<Group>> UpdateGroup(int id, Group request)
        {
            var group = await _context.Groups.FindAsync(id);
            if (group is null)
                return null;

            //group.FirstName = request.FirstName;
            //group.LastName = request.LastName;
            //group.Name = request.Name;
            //group.Place = request.Place;

            await _context.SaveChangesAsync();

            return await _context.Groups.ToListAsync();
        }
        public async Task<List<Group>> DeleteGroup(int id)
        {
            var group = await _context.Groups.FindAsync(id);
            if (group is null)
                return null;

            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();

            return await _context.Groups.ToListAsync();
        }

        public async Task<List<GroupContribution>> GetGroupContributionByGroupId(int groupId)
        {
            var query = from gc in _context.GroupContributions
                        where gc.GroupId == groupId && gc.IsContributionActive == true
                        select gc;

            return await query.ToListAsync();
        }

        public async Task<List<GroupContribution>> GetGroupRobinsByGroupId(int groupId)
        {
            var query = from gc in _context.GroupContributions
                        where gc.GroupId == groupId && gc.IsContributionActive == true && gc.IsRobin == true
                        select gc;

            return await query.ToListAsync();
        }

        public async Task<List<GroupContribution>> GetGroupsDailyRobins()
        {
            var query = from gc in _context.GroupContributions
                        where gc.ContributionFrequency == "FRQ01" && gc.IsContributionActive == true && gc.IsRobin == true
                        select gc;

            return await query.ToListAsync();
        }

        public async Task<List<GroupFineType>> GetGroupFinesByGroupId(int groupId)
        {
            var query =  from gf in _context.GroupFineTypes
                        where gf.GroupId == groupId && gf.IsActive == true
                        select gf;

            return await query.ToListAsync();
        }
    }
}
