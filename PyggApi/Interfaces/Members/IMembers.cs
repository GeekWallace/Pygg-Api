using PyggApi.Models;

namespace PyggApi.Interfaces.Members
{
    public interface IMembers
    {
        Task<List<Member>> GetAllMembersbyGroupId(int id);
        Task<List<MemberAccountDetails>> GetMembersbyGroupId(int id);
        Task<MemberAccountDetails> GetMemberAccountDetailsByGroup(int groupId, int memberNumber, int userId);
        Task<List<MemberAccountDetails>> GetMemberAccountDetailsAllGroups (int memberNumber);
        Task<bool> AddMemberTransaction(MemberTransaction memberTransaction);
        Task<int> GetGroupMemberRoles(int groupId, int memberNumber, int userId);
        Task<DefaultDetail> CheckMemberJoinFeeBalance(int groupId, int memberId, int userId);
        Task<bool> AddFineTransaction(MemberTransaction memberTransaction);
        Task<List<Octopus>> RunOctopusDeployment(DateTime startDate, int groupId, string contributionTypeId);
        Task<int> GetNextSerialNumberAsync(int groupId);
        Task<int> GetGroupCurrentRobinSeries(int groupId);
        Task<int> GetNextTransactionSerialNo();
        Task UpdateMemberPendingContributionsFromInstallments(int groupId);
        Task<bool> UpdateMemberAccountsFromInstallments();
        Task<List<DefaultDetail>> GetPayingMemberDefaultDetails(int groupId, int memberNo, int userId);
        Task<List<Octopus>> DisplayRoinSchedule();
        Task<bool> PostFines();

        Task UpdateGroupBalances(string action, string ActionType, int groupId,
        decimal? GroupPyggBalance,
        decimal? CurrentBalance,
        decimal? AvailableBalance);
        Task UpdateMemberAccountBalances(string action, string ActionType, int accountGroupId, int accountMemberId, int accountUserId,
        decimal? AccountCurrentBalance,
        decimal? AccountAvailableBalance,
        decimal? AccountPrepaidBalance,
        decimal? AccountDefaultAmountToPay,
        decimal? AccountDefaultAmountToReceive,
        decimal? AccountTodayTotalPenalty,
        decimal? LastCreditAmount,
        decimal? LastDebitAmount,
        decimal? AccountTodayDeposit,
        decimal? AccountTodayPendingDeposit,
        decimal? AccountTodayTransactionFee,
        decimal? AccountTodayPendingTransactionFee);
    }
}
