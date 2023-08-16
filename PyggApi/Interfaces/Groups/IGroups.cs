namespace PyggApi.Interfaces.Groups
{
    public interface IGroups
    {
        Task<List<Group>> GetAllGroups();
        Task<Group> GetGroupById(int id);

        Task<List<Group>> AddGroup(Group group);
        Task<List<Group>> UpdateGroup(int id, Group request);
        Task<List<Group>> DeleteGroup(int id);
        Task<List<GroupContribution>> GetGroupContributionByGroupId(int groupId);
        Task<List<GroupFineType>> GetGroupFinesByGroupId(int groupId);
        Task<List<GroupContribution>> GetGroupRobinsByGroupId(int groupId);
        Task<List<GroupContribution>> GetGroupsDailyRobins();
        Task<List<GroupMemberDetail>> GetGroupsByUserId(int userId);
    }
}
