using Microsoft.EntityFrameworkCore;
using PyggApi.Interfaces.Members;
using System.Globalization;
using PyggApi.Interfaces.Groups;

namespace PyggApi.Business.Members
{
    public class MemberBusiness : IMembers
    {

        private readonly ApiDbContext _context;
        private readonly IGroups _groupBusiness;
        public MemberBusiness(ApiDbContext context, IGroups business)
        {
            _context = context;
            _groupBusiness = business;
        }

        public async Task<List<MemberAccountDetails>> GetMembersbyGroupId(int id)
        {
            var query = from a in _context.Members
                        join b in _context.MemberAccounts on a.MemberId equals b.AccountMemberId
                        where b.AccountGroupId == id && a.MemberIsActive == true && b.AccountIsActive == true
                        select new MemberAccountDetails
                        {
                            AccountMemberId = a.MemberId,
                            AccountGroupId = b.AccountGroupId,
                            AccountNumber = b.AccountNumber,
                            AccountUserId = b.AccountUserId,
                            MemberFirstName = a.MemberFirstName,
                            MemberMiddleName = a.MemberMiddleName,
                            MemberLastName = a.MemberLastName,
                            MemberFullName = a.MemberMiddleName + " " + a.MemberLastName,
                            Email = a.Email,
                            MemberPhoneNumber = a.MemberPhoneNumber,
                            PhotoUrl = a.PhotoUrl,
                            AccountTodayPendingDeposit = (decimal)b.AccountTodayPendingDeposit,
                            AccountTodayDeposit = (decimal)b.AccountTodayDeposit,
                            DepositBalanceToPay = (decimal)(b.AccountTodayDeposit - b.AccountTodayDeposit),
                            AccountTodayPendingTransactionFee = (decimal)b.AccountTodayPendingTransactionFee,
                            AccountTodayTransactionFee = (decimal)b.AccountTodayTransactionFee,
                            FeeBalanceToPay = (decimal)(b.AccountTodayPendingTransactionFee - b.AccountTodayTransactionFee),
                            AccountPrepaidBalance = (decimal)b.AccountPrepaidBalance,
                            AccountDefaultAmountToPay = (decimal)b.AccountDefaultAmountToPay,
                            AccountDefaultAmountToReceive = (decimal)b.AccountDefaultAmountToReceive,
                            DefaultDueDate = b.DefaultDueDate,
                            AccountTodayTotalPenalty = (decimal)b.AccountTodayTotalPenalty,
                            AccountLoanBalance = (decimal)b.AccountLoanBalance,
                            LoanDueDate = b.LoanDueDate,
                            AccountAvailableBalance = (decimal)b.AccountAvailableBalance,
                            AccountCurrentBalance = (decimal)b.AccountCurrentBalance
                        };

            return await query.ToListAsync();
        }

        public async Task<List<Member>> GetAllMembersbyGroupId(int groupId)
        {
            var members = await _context.Members
                .Join(
             _context.GroupMembers,
             member => member.MemberId,
             groupMember => groupMember.GroupMemberId,
             (member, groupMember) => new { Member = member, GroupMember = groupMember })
              .Where(x => x.GroupMember.GroupId == groupId)
              .Select(x => x.Member)
              .ToListAsync();

            return members;

            //var query = @"SELECT * FROM Members a
            //      INNER JOIN GroupMembers b ON a.MemberId = b.GroupMemberId
            //      WHERE groupId = @groupId";

            //return await _context.Members.FromSqlInterpolated(FormattableStringFactory.Create(query, groupId)).ToListAsync();


            //var query = $"SELECT * FROM Members a " +
            //    $"INNER JOIN GroupMembers b ON a.MemberId = b.GroupMemberId " +
            //    $"WHERE groupId = {groupId}";

            //return await _context.Members.FromSqlInterpolated(FormattableStringFactory.Create(query)).ToListAsync();
        }

        public async Task<List<MemberAccountDetails>> GetMemberAccountDetailsAllGroups(int memberNumber)
        {
            var query = from a in _context.MemberAccounts
                        join b in _context.Members on a.AccountMemberId equals b.MemberId
                        join c in _context.Groups on a.AccountGroupId equals c.GroupId
                        where a.AccountMemberId == memberNumber
                        select new MemberAccountDetails
                        {
                            AccountMemberId = a.AccountMemberId,
                            AccountGroupId = a.AccountGroupId,
                            MemberFirstName = b.MemberFirstName,
                            MemberLastName = b.MemberLastName,
                            AccountTodayDeposit = (decimal)a.AccountTodayDeposit,
                            AccountTodayPendingDeposit = (decimal)a.AccountTodayPendingDeposit,
                            AccountTodayPendingTransactionFee = (decimal)a.AccountTodayPendingTransactionFee,
                            AccountTodayTransactionFee = (decimal)a.AccountTodayTransactionFee,
                            GroupName = c.GroupName,
                            AccountNumber = a.AccountNumber,
                            AccountUserId = a.AccountUserId,
                            AccountAvailableBalance = (decimal)a.AccountAvailableBalance,
                            AccountCurrentBalance = (decimal)a.AccountCurrentBalance,
                            AccountPrepaidBalance = (decimal)a.AccountPrepaidBalance,
                            AccountDefaultAmountToPay = (decimal)a.AccountDefaultAmountToPay,
                            AccountDefaultAmountToReceive = (decimal)a.AccountDefaultAmountToReceive,
                            DefaultDueDate = a.DefaultDueDate,
                            AccountTodayTotalPenalty = (decimal)a.AccountTodayTotalPenalty,
                            LastCreditTrxDate = a.LastCreditTrxDate,
                            LastCreditTrxAmount = (decimal)a.LastCreditAmount,
                            LastDebitTrxDate = a.LastDebitTrxDate,
                            LastDebitTrxAmount = (decimal)a.LastDebitAmount,
                            AccountIsActive = (bool)a.AccountIsActive
                        };
            return await query.ToListAsync();
        }

        public async Task<MemberAccountDetails> GetMemberAccountDetailsByGroup(int groupId, int memberNumber, int userId)
        {
            var query = from a in _context.MemberAccounts
                        join b in _context.Members on a.AccountMemberId equals b.MemberId
                        join c in _context.Groups on a.AccountGroupId equals c.GroupId
                        where a.AccountGroupId == groupId && a.AccountMemberId == memberNumber && a.AccountUserId == userId
                        select new MemberAccountDetails
                        {
                            AccountMemberId = a.AccountMemberId,
                            AccountGroupId = a.AccountGroupId,
                            MemberFirstName = b.MemberFirstName,
                            MemberLastName = b.MemberLastName,
                            AccountTodayDeposit = (decimal)a.AccountTodayDeposit,
                            AccountTodayPendingDeposit = (decimal)a.AccountTodayPendingDeposit,
                            AccountTodayPendingTransactionFee = (decimal)a.AccountTodayPendingTransactionFee,
                            AccountTodayTransactionFee = (decimal)a.AccountTodayTransactionFee,
                            GroupName = c.GroupName,
                            AccountNumber = a.AccountNumber,
                            AccountUserId = a.AccountUserId,
                            AccountAvailableBalance = (decimal)a.AccountAvailableBalance,
                            AccountCurrentBalance = (decimal)a.AccountCurrentBalance,
                            AccountPrepaidBalance = (decimal)a.AccountPrepaidBalance,
                            AccountDefaultAmountToPay = (decimal)a.AccountDefaultAmountToPay,
                            AccountDefaultAmountToReceive = (decimal)a.AccountDefaultAmountToReceive,
                            DefaultDueDate = a.DefaultDueDate,
                            AccountTodayTotalPenalty = (decimal)a.AccountTodayTotalPenalty,
                            LastCreditTrxDate = a.LastCreditTrxDate,
                            LastCreditTrxAmount = (decimal)a.LastCreditAmount,
                            LastDebitTrxDate = a.LastDebitTrxDate,
                            LastDebitTrxAmount = (decimal)a.LastDebitAmount,
                            AccountIsActive = (bool)a.AccountIsActive
                        };

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<DefaultDetail>> GetPayingMemberDefaultDetails(int groupId, int memberNo, int userId)
        {
            var query = from dd in _context.DefaultDetails
                        where dd.PenaltyGroupId == groupId && dd.PenaltyMemberNumber == memberNo && dd.PenaltyUserId == userId && dd.IsPending == true && dd.PenaltyAmount != 0 && dd.PenaltyTransactionFee != 0
                        select dd;

            return await query.ToListAsync();
        }

        public async Task<List<GroupDepositInstallment>> GetMemberInstallmentDetailsByDate(int groupId, int memberNo, int userId, DateTime installmentDate)
        {
            var query = from dd in _context.GroupDepositInstallments
                        where dd.InstallmentGroupId == groupId && dd.InstallmentMemberId == memberNo && dd.InstallmentUserId == userId && dd.InstallmentDate == Convert.ToDateTime(installmentDate.ToString("yyyy-MM-dd"))
                        select dd;

            return await query.ToListAsync();
        }



        public async Task UpdateGroupBalances(string action, string ActionType, int groupId,
        decimal? GroupPyggBalance,
        decimal? CurrentBalance,
        decimal? AvailableBalance)

        {
            try
            {
                using var context = new ApiDbContext();
                var group = await context.Groups.FirstOrDefaultAsync(a => a.GroupId == groupId);
                if (group == null)
                    return;

                var existingPyggBalance = group.GroupPyggBalance;
                var existingCurrentBalance = group.CurrentBalance;
                var existingAvailableBalance = group.AvailableBalance;

                if (action == "Update Pygg Balance")
                {
                    if (GroupPyggBalance.HasValue)
                    {

                        if (string.Equals(ActionType, "Decrease"))
                        {
                            group.GroupPyggBalance = existingPyggBalance - GroupPyggBalance.Value;
                        }
                        else if (string.Equals(ActionType, "Increase"))
                        {
                            group.GroupPyggBalance = existingPyggBalance + GroupPyggBalance.Value;
                        }
                    }
                }

                if (action == "Update Available Balance")
                {
                    if (AvailableBalance.HasValue)
                    {

                        if (string.Equals(ActionType, "Decrease"))
                        {
                            group.AvailableBalance = existingAvailableBalance - AvailableBalance.Value;
                        }
                        else if (string.Equals(ActionType, "Increase"))
                        {
                            group.AvailableBalance = existingAvailableBalance + AvailableBalance.Value;
                        }
                    }
                }

                if (action == "Update Current Balance")
                {
                    if (CurrentBalance.HasValue)
                    {

                        if (string.Equals(ActionType, "Decrease"))
                        {
                            group.CurrentBalance = existingCurrentBalance - CurrentBalance.Value;
                        }
                        else if (string.Equals(ActionType, "Increase"))
                        {
                            group.CurrentBalance = existingCurrentBalance + CurrentBalance.Value;
                        }
                    }
                }

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public async Task UpdateMemberAccountBalances(string action, string ActionType, int accountGroupId, int accountMemberId, int accountUserId,
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
        decimal? AccountTodayPendingTransactionFee)

        {
            try
            {
                //var memberAccount = await GetMemberAccountDetailsByGroup(accountGroupId, accountMemberId, accountUserId);
                var memberAccount = await _context.MemberAccounts
            .FirstOrDefaultAsync(a => a.AccountGroupId == accountGroupId &&
                                      a.AccountMemberId == accountMemberId &&
                                      a.AccountUserId == accountUserId);

                if (memberAccount == null)
                    return;


                var existingCurrentBalance = memberAccount.AccountCurrentBalance;
                var existingAvailableBalance = memberAccount.AccountAvailableBalance;
                var existingPrepaidBalance = memberAccount.AccountPrepaidBalance;
                var existingDefaultAmountToPay = memberAccount.AccountDefaultAmountToPay;
                var existingDefaultAmountToReceive = memberAccount.AccountDefaultAmountToReceive;
                var exisitingAccountTodayTotalPenalty = memberAccount.AccountTodayTotalPenalty;
                var existingLastCreditAmount = memberAccount.LastCreditAmount;
                var existingLastDebitAmount = memberAccount.LastDebitAmount;
                var existingAccountTodayDeposit = memberAccount.AccountTodayDeposit;
                var existingAccountTodayPendingDeposit = memberAccount.AccountTodayPendingDeposit;
                var existingAccountTodayTransactionFee = memberAccount.AccountTodayTransactionFee;
                var existingAccountTodayPendingTransactionFee = memberAccount.AccountTodayPendingTransactionFee;


                if (string.Equals(action, "Update Today Deposit"))
                {
                    if (AccountTodayDeposit.HasValue)
                    {

                        if (string.Equals(ActionType, "Decrease"))
                        {
                            memberAccount.AccountTodayDeposit = existingAccountTodayDeposit - AccountTodayDeposit.Value;//includes initial deposit plus any debits and credit to the account

                        }
                        else if (string.Equals(ActionType, "Increase"))
                        {
                            memberAccount.AccountTodayDeposit = existingAccountTodayDeposit + AccountTodayDeposit.Value;//includes initial deposit plus any debits and credit to the account
                        }
                    }
                }


                if (string.Equals(action, "Update Today Pending Deposit"))
                {
                    if (AccountTodayPendingDeposit.HasValue)
                    {

                        if (string.Equals(ActionType, "Decrease"))
                        {
                            memberAccount.AccountTodayPendingDeposit = existingAccountTodayPendingDeposit - AccountTodayPendingDeposit.Value;
                        }
                        else if (string.Equals(ActionType, "Increase"))
                        {
                            memberAccount.AccountTodayPendingDeposit = existingAccountTodayPendingDeposit + AccountTodayPendingDeposit.Value;
                        }
                    }
                }


                if (string.Equals(action, "Update Today Transaction Fee"))
                {
                    if (AccountTodayTransactionFee.HasValue)
                    {

                        if (string.Equals(ActionType, "Decrease"))
                        {
                            memberAccount.AccountTodayTransactionFee = existingAccountTodayTransactionFee - AccountTodayTransactionFee.Value;
                        }
                        else if (string.Equals(ActionType, "Increase"))
                        {
                            memberAccount.AccountTodayTransactionFee = existingAccountTodayTransactionFee + AccountTodayTransactionFee.Value;
                        }
                    }
                }


                if (string.Equals(action, "Update Today Pending Transaction Fee"))
                {
                    if (AccountTodayPendingTransactionFee.HasValue)
                    {

                        if (string.Equals(ActionType, "Decrease"))
                        {
                            memberAccount.AccountTodayPendingTransactionFee = existingAccountTodayPendingTransactionFee - AccountTodayPendingTransactionFee.Value;
                        }
                        else if (string.Equals(ActionType, "Increase"))
                        {
                            memberAccount.AccountTodayPendingTransactionFee = existingAccountTodayPendingTransactionFee + AccountTodayPendingTransactionFee.Value;
                        }
                    }
                }


                if (string.Equals(action, "Update Total Penalty"))
                {
                    if (AccountTodayTotalPenalty.HasValue)
                    {

                        if (string.Equals(ActionType, "Decrease"))
                        {
                            memberAccount.AccountTodayTotalPenalty = exisitingAccountTodayTotalPenalty - AccountTodayTotalPenalty.Value;//includes initial deposit plus any debits and credit to the account

                        }
                        else if (string.Equals(ActionType, "Increase"))
                        {
                            memberAccount.AccountTodayTotalPenalty = exisitingAccountTodayTotalPenalty + AccountTodayTotalPenalty.Value;//includes initial deposit plus any debits and credit to the account
                        }
                    }
                }


                if (string.Equals(action, "Update Prepaid Balance"))
                {
                    if (AccountPrepaidBalance.HasValue)
                    {

                        if (string.Equals(ActionType, "Decrease"))
                        {
                            memberAccount.AccountPrepaidBalance = existingPrepaidBalance - AccountPrepaidBalance.Value;
                        }
                        else if (string.Equals(ActionType, "Increase"))
                        {
                            memberAccount.AccountPrepaidBalance = existingPrepaidBalance + AccountPrepaidBalance.Value;
                        }
                    }
                }


                if (string.Equals(action, "Update Available Balance"))
                {
                    if (AccountAvailableBalance.HasValue)
                    {

                        if (string.Equals(ActionType, "Decrease"))
                        {

                            memberAccount.LastDebitAmount = AccountAvailableBalance.Value;
                            memberAccount.LastDebitTrxDate = DateTime.Now;
                            memberAccount.AccountAvailableBalance = existingAvailableBalance - AccountAvailableBalance.Value;
                        }
                        else if (string.Equals(ActionType, "Increase"))
                        {

                            memberAccount.LastCreditAmount = AccountAvailableBalance.Value;
                            memberAccount.LastCreditTrxDate = DateTime.Now;
                            memberAccount.AccountAvailableBalance = existingAvailableBalance + AccountAvailableBalance.Value;
                        }
                    }
                }


                if (string.Equals(action, "Update Current Balance"))
                {
                    if (AccountCurrentBalance.HasValue)
                    {

                        if (string.Equals(ActionType, "Decrease"))
                        {

                            memberAccount.LastDebitAmount = AccountCurrentBalance.Value;
                            memberAccount.LastDebitTrxDate = DateTime.Now;
                            memberAccount.AccountCurrentBalance = existingCurrentBalance - AccountCurrentBalance.Value;
                        }
                        else if (string.Equals(ActionType, "Increase"))
                        {

                            memberAccount.LastCreditAmount = AccountCurrentBalance.Value;
                            memberAccount.LastCreditTrxDate = DateTime.Now;
                            memberAccount.AccountCurrentBalance = existingCurrentBalance + AccountCurrentBalance.Value;
                        }
                    }
                }

                if (string.Equals(action, "Update Default Amount To Receive"))
                {
                    if (AccountDefaultAmountToReceive.HasValue)
                    {

                        if (string.Equals(ActionType, "Decrease"))
                        {
                            memberAccount.AccountDefaultAmountToReceive = existingDefaultAmountToReceive - AccountDefaultAmountToReceive.Value;
                        }
                        else if (string.Equals(ActionType, "Increase"))
                            memberAccount.AccountDefaultAmountToReceive = existingDefaultAmountToReceive + AccountDefaultAmountToReceive.Value;
                    }
                }


                if (string.Equals(action, "Update Default Amount To Pay"))
                {
                    if (AccountDefaultAmountToPay.HasValue)
                    {


                        if (string.Equals(ActionType, "Decrease"))
                        {
                            memberAccount.AccountDefaultAmountToPay = existingDefaultAmountToPay - AccountDefaultAmountToPay.Value;
                        }
                        else if (string.Equals(ActionType, "Increase"))
                        {
                            memberAccount.AccountDefaultAmountToPay = existingDefaultAmountToPay + AccountDefaultAmountToPay.Value;
                        }
                    }
                }

                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");

            }
        }

        public async Task<bool> AddFineTransaction(MemberTransaction memberTransaction)
        {
            if (memberTransaction == null) return false;


            int serialNumber = await GetNextTransactionSerialNo();
            var memberGroupId = memberTransaction.TransactionGroupId;
            var memberNumber = memberTransaction.TransactionMemberId;
            var memberUserId = memberTransaction.TransactionUserId;
            var fineTransaction = memberTransaction.TransactionAmount;


            MemberTransaction payingMember = new()
            {
                TransactionSerialId = serialNumber,
                TransactionAccountNumber = Convert.ToInt32(memberTransaction.TransactionAccountNumber),
                TransactionMemberId = Convert.ToInt32(memberTransaction.TransactionMemberId),
                TransactionGroupId = Convert.ToInt32(memberTransaction.TransactionGroupId),
                TransactionUserId = Convert.ToInt32(memberTransaction.TransactionUserId),
                ContributionId = Convert.ToInt32(memberTransaction.ContributionId),
                ContributionTypeId = memberTransaction.ContributionTypeId,
                FineId = 0,
                PaymentTypeId = memberTransaction.PaymentTypeId,
                TransactionCode = memberTransaction.TransactionCode,
                TransactionTypeId = memberTransaction.TransactionTypeId,
                TransactionAmount = (decimal?)Convert.ToDouble(memberTransaction.TransactionAmount),
                TransactionDate = Convert.ToDateTime(Convert.ToDateTime(memberTransaction.TransactionDate)),
                TransactionValueDate = Convert.ToDateTime(Convert.ToDateTime(DateTime.Now.Date)),
                TransactionGroupFineAccount = null,
                TransactionContributionAccount = memberTransaction.TransactionContributionAccount,
                TransactionNarration = memberTransaction.TransactionNarration + "Fine Payment",
                TransactionIsConfirmed = false,
                TransactionIsDefaultRecovery = false,
                TransactionDefaultRecoveryMemberId = 0,
                TransactionDefaultRecoveryGroupId = 0,
                TransactionDefaultRecoveryUserId = 0,
                TransactionDefaultRecoveryContributionId = 0,
                TransactionFineRecoveryFineId = 0,
                TransactionCreatedBy = memberTransaction.TransactionCreatedBy,
                TransactionCreatedOn = DateTime.Now,

                SafInvoiceId = memberTransaction.SafInvoiceId,
                SafTransState = memberTransaction.SafTransState,
                SafCharges = (decimal?)Convert.ToDouble(memberTransaction.SafCharges),
                SafNetAmount = (decimal?)Convert.ToDouble(memberTransaction.SafNetAmount),
                SafCurrency = memberTransaction.SafCurrency,
                SafValue = (decimal?)Convert.ToDouble(memberTransaction.SafValue),
                SafAccount = memberTransaction.SafAccount,
                SafApiRef = memberTransaction.SafApiRef,
                SafMpesaRef = memberTransaction.SafMpesaRef,
                SafHost = memberTransaction.SafHost,
                SafRetry = memberTransaction.SafRetry,
                SafFailedReason = memberTransaction.SafFailedReason,
                SafFailedCode = memberTransaction.SafFailedCode,
                SafCreatedDate = memberTransaction.SafCreatedDate,
                SafCreatedTime = memberTransaction.SafCreatedTime,
                SafCustomerPhoneNumber = memberTransaction.SafCustomerPhoneNumber,
                SafCustomerCountry = memberTransaction.SafCustomerCountry,
                SafCustomerEmail = memberTransaction.SafCustomerEmail,
                SafCustomerLastname = memberTransaction.SafCustomerLastname
            };

            _context.MemberTransactions.Add(payingMember);
            await _context.SaveChangesAsync();

            if (fineTransaction > 0)
            {
                var memberFines = await _context.DefaultDetails
                     .Where(a => a.PenaltyGroupId == memberGroupId && a.PenaltyMemberNumber == memberNumber &&
                              a.PenaltyUserId == memberUserId && a.FineAmount != 0)
                     .ToListAsync();

                if (memberFines.Any())
                {

                    foreach (var fine in memberFines)
                    {
                        if (fineTransaction > 0)
                        {
                            var fineToPay = fine.FineAmount;
                            var penaltyId = fine.PenaltyId;
                            if (fineTransaction >= fineToPay)
                            {

                                await UpdateGroupBalances("Update Pygg Balance", "Increase", memberGroupId, fineToPay, null, null);
                                await UpdateGroupBalances("Update Current Balance", "Increase", memberGroupId, null, fineToPay, null);
                                await UpdateGroupBalances("Update Available Balance", "Decrease", memberGroupId, null, null, fineToPay);
                                await UpdateMemberAccountBalances("Update Total Penalty", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, fineToPay, null, null, null, null, null, null);

                                var paidFine = await _context.DefaultDetails
                             .FirstOrDefaultAsync(a => a.PenaltyId == penaltyId && a.PenaltyGroupId == memberGroupId && a.PenaltyMemberNumber == memberNumber &&
                               a.PenaltyUserId == memberUserId);
                                paidFine.FineAmount -= fineToPay;
                                await _context.SaveChangesAsync();

                                fineTransaction -= fineToPay;
                            }
                            else
                            {
                                await UpdateGroupBalances("Update Pygg Balance", "Increase", memberGroupId, fineTransaction, null, null);
                                await UpdateGroupBalances("Update Current Balance", "Increase", memberGroupId, null, fineTransaction, null);
                                await UpdateGroupBalances("Update Available Balance", "Decrease", memberGroupId, null, null, fineTransaction);
                                await UpdateMemberAccountBalances("Update Total Penalty", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, fineTransaction, null, null, null, null, null, null);

                                var paidFine = await _context.DefaultDetails
                                .FirstOrDefaultAsync(a => a.PenaltyId == penaltyId && a.PenaltyGroupId == memberGroupId && a.PenaltyMemberNumber == memberNumber &&
                                a.PenaltyUserId == memberUserId);
                                paidFine.FineAmount -= fineTransaction;
                                await _context.SaveChangesAsync();

                                fineTransaction -= fineTransaction;
                            }
                        }
                    }
                    if (fineTransaction > 0)
                    {
                        await UpdateMemberAccountBalances("Update Current Balance", "Increase", memberGroupId, memberNumber, memberUserId, fineTransaction, null, null, null, null, null, fineTransaction, null, null, null, null, null);
                        await UpdateMemberAccountBalances("Update Prepaid Balance", "Increase", memberGroupId, memberNumber, memberUserId, null, null, fineTransaction, null, null, null, null, null, null, null, null, null);
                    }

                }

            }

            return true;
        }

        public async Task<DefaultDetail> CheckMemberJoinFeeBalance(int groupId, int memberId, int userId)
        {
            var joinFee = await _context.DefaultDetails
         .FirstOrDefaultAsync(a => a.PenaltyGroupId == groupId && a.PenaltyMemberNumber == memberId &&
                                    a.PenaltyUserId == userId && a.JoinFeeAmount != 0);

            return joinFee;
        }

        /// <summary>
        /// Checks if member paying has any pending defaults to other members before settling their contribution
        /// required for contributions.
        /// </summary>
        /// <param name="a">groupId.</param>
        /// <returns></returns>
        public async Task<bool> AddMemberTransaction(MemberTransaction memberTransaction)
        {
            var installmentNo = 0;
            var isInstallmentPaid = false;

            DateTime memberDueDate = DateTime.Today;
            DateTime serverTimeUtc = DateTime.UtcNow;
            TimeZoneInfo eastAfricaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. Africa Standard Time");
            DateTime eastAfricaTime = TimeZoneInfo.ConvertTimeFromUtc(serverTimeUtc, eastAfricaTimeZone);

            if (memberTransaction == null) return false;

            if (memberTransaction.PaymentTypeId == "PYT03")
            {
                int serialNumber = await GetNextTransactionSerialNo();
                var JoinFeeTransactionAmount = memberTransaction.TransactionAmount;

                MemberTransaction payingMember = new()
                {
                    TransactionSerialId = serialNumber,
                    TransactionAccountNumber = Convert.ToInt32(memberTransaction.TransactionAccountNumber),
                    TransactionMemberId = Convert.ToInt32(memberTransaction.TransactionMemberId),
                    TransactionGroupId = Convert.ToInt32(memberTransaction.TransactionGroupId),
                    TransactionUserId = Convert.ToInt32(memberTransaction.TransactionUserId),
                    ContributionId = Convert.ToInt32(memberTransaction.ContributionId),
                    ContributionTypeId = memberTransaction.ContributionTypeId,
                    FineId = 0,
                    PaymentTypeId = memberTransaction.PaymentTypeId,
                    TransactionCode = memberTransaction.TransactionCode,
                    TransactionTypeId = memberTransaction.TransactionTypeId,
                    TransactionAmount = (decimal?)Convert.ToDouble(memberTransaction.TransactionAmount),
                    TransactionDate = Convert.ToDateTime(Convert.ToDateTime(memberTransaction.TransactionDate)),
                    TransactionValueDate = Convert.ToDateTime(Convert.ToDateTime(DateTime.Now.Date)),
                    TransactionGroupFineAccount = null,
                    TransactionContributionAccount = memberTransaction.TransactionContributionAccount,
                    TransactionNarration = memberTransaction.TransactionNarration + "Joining Fee Payment",
                    TransactionIsConfirmed = false,
                    TransactionIsDefaultRecovery = false,
                    TransactionDefaultRecoveryMemberId = 0,
                    TransactionDefaultRecoveryGroupId = 0,
                    TransactionDefaultRecoveryUserId = 0,
                    TransactionDefaultRecoveryContributionId = 0,
                    TransactionFineRecoveryFineId = 0,
                    TransactionCreatedBy = memberTransaction.TransactionCreatedBy,
                    TransactionCreatedOn = DateTime.Now,

                    SafInvoiceId = memberTransaction.SafInvoiceId,
                    SafTransState = memberTransaction.SafTransState,
                    SafCharges = (decimal?)Convert.ToDouble(memberTransaction.SafCharges),
                    SafNetAmount = (decimal?)Convert.ToDouble(memberTransaction.SafNetAmount),
                    SafCurrency = memberTransaction.SafCurrency,
                    SafValue = (decimal?)Convert.ToDouble(memberTransaction.SafValue),
                    SafAccount = memberTransaction.SafAccount,
                    SafApiRef = memberTransaction.SafApiRef,
                    SafMpesaRef = memberTransaction.SafMpesaRef,
                    SafHost = memberTransaction.SafHost,
                    SafRetry = memberTransaction.SafRetry,
                    SafFailedReason = memberTransaction.SafFailedReason,
                    SafFailedCode = memberTransaction.SafFailedCode,
                    SafCreatedDate = memberTransaction.SafCreatedDate,
                    SafCreatedTime = memberTransaction.SafCreatedTime,
                    SafCustomerPhoneNumber = memberTransaction.SafCustomerPhoneNumber,
                    SafCustomerCountry = memberTransaction.SafCustomerCountry,
                    SafCustomerEmail = memberTransaction.SafCustomerEmail,
                    SafCustomerLastname = memberTransaction.SafCustomerLastname
                };

                _context.MemberTransactions.Add(payingMember);
                await _context.SaveChangesAsync();

                var memberGroupId = payingMember.TransactionGroupId;
                var memberNumber = payingMember.TransactionMemberId;
                var memberUserId = payingMember.TransactionUserId;
                var memberTransactionAmount = payingMember.TransactionAmount;

                if (JoinFeeTransactionAmount > 0)
                {
                    var joinFee = await _context.DefaultDetails
                         .FirstOrDefaultAsync(a => a.PenaltyGroupId == memberGroupId && a.PenaltyMemberNumber == memberNumber &&
                                  a.PenaltyUserId == memberUserId && a.JoinFeeAmount != 0);

                    if (joinFee == null) return false;

                    if (joinFee != null)
                    {
                        if (JoinFeeTransactionAmount > 0)
                        {

                            var feeToPay = joinFee.JoinFeeAmount;
                            if (JoinFeeTransactionAmount > feeToPay)
                            {

                                await UpdateGroupBalances("Update Pygg Balance", "Increase", memberGroupId, feeToPay, null, null);
                                await UpdateGroupBalances("Update Current Balance", "Increase", memberGroupId, null, feeToPay, null);
                                JoinFeeTransactionAmount -= feeToPay;
                                feeToPay -= feeToPay;
                                if (JoinFeeTransactionAmount > 0)
                                {
                                    await UpdateMemberAccountBalances("Update Current Balance", "Increase", memberGroupId, memberNumber, memberUserId, JoinFeeTransactionAmount, null, null, null, null, null, JoinFeeTransactionAmount, null, null, null, null, null);
                                    await UpdateMemberAccountBalances("Update Prepaid Balance", "Increase", memberGroupId, memberNumber, memberUserId, null, null, JoinFeeTransactionAmount, null, null, null, null, null, null, null, null, null);
                                }

                                var paidFee = await _context.DefaultDetails
                                      .Where(a => a.PenaltyGroupId == memberGroupId && a.PenaltyMemberNumber == memberNumber &&
                                             a.PenaltyUserId == memberUserId)
                                            .ToListAsync();

                                foreach (var fee in paidFee)
                                {
                                    fee.JoinFeeAmount = 0;
                                }
                                await _context.SaveChangesAsync();

                                //uncomment this when you add functionality to add user where will increase availble balance before member pays the joining fee
                                //await UpdateGroupBalances("Update Available Balance", "Decrease", memberGroupId, null, null, feeToPay);
                            }
                            else
                            {
                                await UpdateGroupBalances("Update Pygg Balance", "Increase", memberGroupId, JoinFeeTransactionAmount, null, null);
                                await UpdateGroupBalances("Update Current Balance", "Increase", memberGroupId, null, JoinFeeTransactionAmount, null);

                                feeToPay -= JoinFeeTransactionAmount;
                                var paidFee = await _context.DefaultDetails
                                      .Where(a => a.PenaltyGroupId == memberGroupId && a.PenaltyMemberNumber == memberNumber &&
                                             a.PenaltyUserId == memberUserId)
                                            .ToListAsync();

                                foreach (var fee in paidFee)
                                {
                                    fee.JoinFeeAmount = feeToPay;
                                }
                                await _context.SaveChangesAsync();
                                //uncomment this when you add functionality to add user where will increase availble balance before member pays the joining fee
                                //await UpdateGroupBalances("Update Available Balance", "Decrease", memberGroupId, null, null, JoinFeeTransactionAmount);
                            }

                        }
                    }
                }

                return true;
            }



            if (memberTransaction.PaymentTypeId == "PYT01")
            {

                int serialNumber = await GetNextTransactionSerialNo();

                //Capture depositing member initial transaction
                MemberTransaction payingMember = new()
                {
                    TransactionSerialId = serialNumber,
                    TransactionAccountNumber = Convert.ToInt32(memberTransaction.TransactionAccountNumber),
                    TransactionMemberId = Convert.ToInt32(memberTransaction.TransactionMemberId),
                    TransactionGroupId = Convert.ToInt32(memberTransaction.TransactionGroupId),
                    TransactionUserId = Convert.ToInt32(memberTransaction.TransactionUserId),
                    ContributionId = Convert.ToInt32(memberTransaction.ContributionId),
                    ContributionTypeId = memberTransaction.ContributionTypeId,
                    FineId = 0,
                    PaymentTypeId = memberTransaction.PaymentTypeId,
                    TransactionCode = memberTransaction.TransactionCode,
                    TransactionTypeId = memberTransaction.TransactionTypeId,
                    TransactionAmount = (decimal?)Convert.ToDouble(memberTransaction.TransactionAmount),
                    TransactionDate = Convert.ToDateTime(Convert.ToDateTime(memberTransaction.TransactionDate)),
                    TransactionValueDate = Convert.ToDateTime(Convert.ToDateTime(DateTime.Now.Date)),
                    TransactionGroupFineAccount = null,
                    TransactionContributionAccount = memberTransaction.TransactionContributionAccount,
                    TransactionNarration = memberTransaction.TransactionNarration + "Initial Direct Transaction Serial No " + serialNumber.ToString(),
                    TransactionIsConfirmed = false,
                    TransactionIsDefaultRecovery = false,
                    TransactionDefaultRecoveryMemberId = 0,
                    TransactionDefaultRecoveryGroupId = 0,
                    TransactionDefaultRecoveryUserId = 0,
                    TransactionDefaultRecoveryContributionId = 0,
                    TransactionFineRecoveryFineId = 0,
                    TransactionCreatedBy = memberTransaction.TransactionCreatedBy,
                    TransactionCreatedOn = DateTime.Now,

                    SafInvoiceId = memberTransaction.SafInvoiceId,
                    SafTransState = memberTransaction.SafTransState,
                    SafCharges = (decimal?)Convert.ToDouble(memberTransaction.SafCharges),
                    SafNetAmount = (decimal?)Convert.ToDouble(memberTransaction.SafNetAmount),
                    SafCurrency = memberTransaction.SafCurrency,
                    SafValue = (decimal?)Convert.ToDouble(memberTransaction.SafValue),
                    SafAccount = memberTransaction.SafAccount,
                    SafApiRef = memberTransaction.SafApiRef,
                    SafMpesaRef = memberTransaction.SafMpesaRef,
                    SafHost = memberTransaction.SafHost,
                    SafRetry = memberTransaction.SafRetry,
                    SafFailedReason = memberTransaction.SafFailedReason,
                    SafFailedCode = memberTransaction.SafFailedCode,
                    SafCreatedDate = memberTransaction.SafCreatedDate,
                    SafCreatedTime = memberTransaction.SafCreatedTime,
                    SafCustomerPhoneNumber = memberTransaction.SafCustomerPhoneNumber,
                    SafCustomerCountry = memberTransaction.SafCustomerCountry,
                    SafCustomerEmail = memberTransaction.SafCustomerEmail,
                    SafCustomerLastname = memberTransaction.SafCustomerLastname
                };

                _context.MemberTransactions.Add(payingMember);
                await _context.SaveChangesAsync();

                var memberGroupId = payingMember.TransactionGroupId;
                var memberNumber = payingMember.TransactionMemberId;
                var memberUserId = payingMember.TransactionUserId;
                var memberTransactionAmount = payingMember.TransactionAmount;

                //Use this to calculate penalties
                var payingMemberInstallmentDetails = await _context.GroupDepositInstallments
               .FirstOrDefaultAsync(a => a.InstallmentGroupId == memberGroupId &&
                                a.InstallmentMemberId == memberNumber &&
                                 a.InstallmentUserId == memberUserId &&
                                 a.InstallmentDate == Convert.ToDateTime(eastAfricaTime.ToString("yyyy-MM-dd")));

                if (payingMemberInstallmentDetails != null)
                {
                    installmentNo = payingMemberInstallmentDetails.InstallmentNo;
                    isInstallmentPaid = (bool)payingMemberInstallmentDetails.IsInstallmentPaid;
                    memberDueDate = (DateTime)payingMemberInstallmentDetails.MemberDueDate;
                }

                //Update paying member current balance
                await UpdateMemberAccountBalances("Update Current Balance", "Increase", memberGroupId, memberNumber, memberUserId, memberTransactionAmount, null, null, null, null, null, memberTransactionAmount, null, null, null, null, null);
                await UpdateMemberAccountBalances("Update Prepaid Balance", "Increase", memberGroupId, memberNumber, memberUserId, null, null, memberTransactionAmount, null, null, null, null, null, null, null, null, null);

                var payingMemberDefaults = await GetPayingMemberDefaultDetails(memberGroupId, memberNumber, memberUserId);

                if (payingMemberDefaults.Count > 0)
                {
                    foreach (var payingMemberDefault in payingMemberDefaults)
                    {
                        if (memberTransactionAmount > 0)
                        {
                            var penaltyId = payingMemberDefault.PenaltyId;
                            var penaltyAmount = payingMemberDefault.PenaltyAmount;
                            var penaltyTransactionFee = payingMemberDefault.PenaltyTransactionFee;
                            var isPending = payingMemberDefault.IsPending;
                            var contributionTypeId = payingMemberDefault.ContributionTypeId;
                            var amountToDeduct = penaltyAmount + penaltyTransactionFee;
                            int penaltyOwedToMemberId = (int)payingMemberDefault.PenaltyOwedToMemberId;
                            int penaltyOwedToMemberGroupId = (int)payingMemberDefault.PenaltyOwedToGroupId;
                            int penaltyOwedToUserId = (int)payingMemberDefault.PenaltyOwedToUserId;
                            var penaltyOwedContributionId = payingMemberDefault.PenaltyOwedToContributionId;
                            var penaltyOwedToFineId = payingMemberDefault.PenaltyOwedToFineId;

                            //memberTransactionAmount -= amountToDeduct;

                            MemberTransaction defaultTransaction = new()
                            {
                                TransactionSerialId = serialNumber,
                                TransactionAccountNumber = Convert.ToInt32(payingMember.TransactionAccountNumber),
                                TransactionMemberId = Convert.ToInt32(payingMember.TransactionMemberId),
                                TransactionGroupId = Convert.ToInt32(payingMember.TransactionGroupId),
                                TransactionUserId = Convert.ToInt32(payingMember.TransactionUserId),
                                ContributionId = Convert.ToInt32(payingMember.ContributionId),
                                ContributionTypeId = contributionTypeId,
                                FineId = 0,
                                PaymentTypeId = payingMember.PaymentTypeId,
                                TransactionCode = payingMember.TransactionCode,
                                TransactionTypeId = payingMember.TransactionTypeId,
                                TransactionAmount = (decimal?)Convert.ToDouble(memberTransactionAmount),
                                TransactionDate = Convert.ToDateTime(Convert.ToDateTime(payingMember.TransactionDate)),
                                TransactionValueDate = Convert.ToDateTime(Convert.ToDateTime(DateTime.Now.Date)),
                                TransactionGroupFineAccount = null,
                                TransactionContributionAccount = payingMember.TransactionContributionAccount,
                                TransactionNarration = payingMember.TransactionNarration + " Amount deducted for default recovery from direct deposit from transaction serial No: " + serialNumber.ToString(),
                                TransactionIsConfirmed = false,
                                TransactionIsDefaultRecovery = true,
                                TransactionDefaultRecoveryMemberId = penaltyOwedToMemberId,
                                TransactionDefaultRecoveryGroupId = penaltyOwedToMemberGroupId,
                                TransactionDefaultRecoveryUserId = penaltyOwedToUserId,
                                TransactionDefaultRecoveryContributionId = penaltyOwedContributionId,
                                TransactionFineRecoveryFineId = penaltyOwedToFineId,
                                TransactionCreatedBy = payingMember.TransactionCreatedBy,
                                TransactionCreatedOn = DateTime.Now,

                                SafInvoiceId = payingMember.SafInvoiceId,
                                SafTransState = payingMember.SafTransState,
                                SafCharges = (decimal?)Convert.ToDouble(payingMember.SafCharges),
                                SafNetAmount = (decimal?)Convert.ToDouble(payingMember.SafNetAmount),
                                SafCurrency = payingMember.SafCurrency,
                                SafValue = (decimal?)Convert.ToDouble(payingMember.SafValue),
                                SafAccount = payingMember.SafAccount,
                                SafApiRef = payingMember.SafApiRef,
                                SafMpesaRef = payingMember.SafMpesaRef,
                                SafHost = payingMember.SafHost,
                                SafRetry = payingMember.SafRetry,
                                SafFailedReason = payingMember.SafFailedReason,
                                SafFailedCode = payingMember.SafFailedCode,
                                SafCreatedDate = payingMember.SafCreatedDate,
                                SafCreatedTime = payingMember.SafCreatedTime,
                                SafCustomerPhoneNumber = payingMember.SafCustomerPhoneNumber,
                                SafCustomerCountry = payingMember.SafCustomerCountry,
                                SafCustomerEmail = payingMember.SafCustomerEmail,
                                SafCustomerLastname = payingMember.SafCustomerLastname

                            };

                            _context.MemberTransactions.Add(defaultTransaction);
                            await _context.SaveChangesAsync();

                            if (penaltyAmount != 0)
                            {
                                if (memberTransactionAmount >= penaltyAmount)
                                {
                                    await UpdateMemberAccountBalances("Update Current Balance", "Decrease", memberGroupId, memberNumber, memberUserId, penaltyAmount, null, null, null, null, null, null, penaltyAmount, null, null, null, null);
                                    await UpdateMemberAccountBalances("Update Prepaid Balance", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, penaltyAmount, null, null, null, null, null, null, null, null, null);
                                    //await UpdateMemberAccountBalances("Update Total Penalty", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, penaltyAmount, null, null, null, null, null, null);
                                    await UpdateMemberAccountBalances("Update Default Amount To Pay", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, null, penaltyAmount, null, null, null, penaltyAmount, null, null, null, null);
                                    await UpdateMemberAccountBalances("Update Current Balance", "Increase", penaltyOwedToMemberGroupId, penaltyOwedToMemberId, penaltyOwedToUserId, penaltyAmount, null, null, null, null, null, null, penaltyAmount, null, null, null, null);
                                    await UpdateMemberAccountBalances("Update Default Amount To Receive", "Decrease", penaltyOwedToMemberGroupId, penaltyOwedToMemberId, penaltyOwedToUserId, null, null, null, null, penaltyAmount, null, null, null, null, null, null, null);
                                    await UpdateMemberAccountBalances("Update Prepaid Balance", "Increase", penaltyOwedToMemberGroupId, penaltyOwedToMemberId, penaltyOwedToUserId, null, null, penaltyAmount, null, null, null, null, null, null, null, null, null);
                                    await UpdateMemberAccountBalances("Update Available Balance", "Decrease", penaltyOwedToMemberGroupId, penaltyOwedToMemberId, penaltyOwedToUserId, null, penaltyAmount, null, null, null, null, null, null, null, null, null, null);
                                    //await UpdateMemberAccountBalances("Update Available Balance", "Decrease", penaltyOwedToMemberGroupId, penaltyOwedToMemberId, penaltyOwedToUserId, null, penaltyAmount, null, null, null, null, null, null, null, null, null, null);
                                    //consider updating available balance as well
                                    memberTransactionAmount = (decimal)(memberTransactionAmount - penaltyAmount);
                                    penaltyAmount = (decimal)(penaltyAmount - penaltyAmount);
                                }
                                else
                                {
                                    await UpdateMemberAccountBalances("Update Current Balance", "Decrease", memberGroupId, memberNumber, memberUserId, memberTransactionAmount, null, null, null, null, null, null, memberTransactionAmount, null, null, null, null);
                                    await UpdateMemberAccountBalances("Update Prepaid Balance", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, memberTransactionAmount, null, null, null, null, null, null, null, null, null);
                                    await UpdateMemberAccountBalances("Update Default Amount To Pay", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, null, memberTransactionAmount, null, null, null, memberTransactionAmount, null, null, null, null);
                                    await UpdateMemberAccountBalances("Update Current Balance", "Increase", penaltyOwedToMemberGroupId, penaltyOwedToMemberId, penaltyOwedToUserId, memberTransactionAmount, null, null, null, null, null, null, memberTransactionAmount, null, null, null, null);
                                    await UpdateMemberAccountBalances("Update Default Amount To Receive", "Decrease", penaltyOwedToMemberGroupId, penaltyOwedToMemberId, penaltyOwedToUserId, null, null, null, null, memberTransactionAmount, null, null, null, null, null, null, null);
                                    await UpdateMemberAccountBalances("Update Prepaid Balance", "Increase", penaltyOwedToMemberGroupId, penaltyOwedToMemberId, penaltyOwedToUserId, null, null, memberTransactionAmount, null, null, null, null, null, null, null, null, null);
                                    await UpdateMemberAccountBalances("Update Available Balance", "Decrease", penaltyOwedToMemberGroupId, penaltyOwedToMemberId, penaltyOwedToUserId, null, memberTransactionAmount, null, null, null, null, null, null, null, null, null, null);

                                    memberTransactionAmount = (decimal)(memberTransactionAmount - memberTransactionAmount);
                                    penaltyAmount = (decimal)(penaltyAmount - memberTransactionAmount);

                                }
                            }

                            if (memberTransactionAmount > 0 && penaltyTransactionFee != 0)
                            {
                                if (penaltyTransactionFee != 0)
                                {
                                    if (memberTransactionAmount >= penaltyTransactionFee)
                                    {
                                        await UpdateMemberAccountBalances("Update Current Balance", "Decrease", memberGroupId, memberNumber, memberUserId, penaltyTransactionFee, null, null, null, null, null, null, penaltyTransactionFee, null, null, null, null);
                                        await UpdateMemberAccountBalances("Update Prepaid Balance", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, penaltyTransactionFee, null, null, null, null, null, null, null, null, null);
                                        await UpdateMemberAccountBalances("Update Default Amount To Pay", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, null, penaltyTransactionFee, null, null, null, penaltyTransactionFee, null, null, null, null);
                                        await UpdateGroupBalances("Update Current Balance", "Increase", memberGroupId, null, penaltyTransactionFee, null);
                                        await UpdateGroupBalances("Update Pygg Balance", "Increase", memberGroupId, penaltyTransactionFee, null, null);
                                        await UpdateGroupBalances("Update Available Balance", "Decrease", memberGroupId, null, null, penaltyTransactionFee);
                                        memberTransactionAmount = (decimal)(memberTransactionAmount - penaltyTransactionFee);
                                        penaltyTransactionFee = (decimal)(penaltyTransactionFee - penaltyTransactionFee);
                                    }
                                    else
                                    {
                                        await UpdateMemberAccountBalances("Update Current Balance", "Decrease", memberGroupId, memberNumber, memberUserId, memberTransactionAmount, null, null, null, null, null, null, memberTransactionAmount, null, null, null, null);
                                        await UpdateMemberAccountBalances("Update Prepaid Balance", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, memberTransactionAmount, null, null, null, null, null, null, null, null, null);
                                        await UpdateMemberAccountBalances("Update Default Amount To Pay", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, null, memberTransactionAmount, null, null, null, memberTransactionAmount, null, null, null, null);
                                        await UpdateGroupBalances("Update Current Balance", "Increase", memberGroupId, null, memberTransactionAmount, null);
                                        await UpdateGroupBalances("Update Pygg Balance", "Increase", memberGroupId, memberTransactionAmount, null, null);
                                        await UpdateGroupBalances("Update Available Balance", "Decrease", memberGroupId, null, null, memberTransactionAmount);
                                        memberTransactionAmount = (decimal)(memberTransactionAmount - memberTransactionAmount);
                                        penaltyTransactionFee = (decimal)(penaltyTransactionFee - memberTransactionAmount);
                                    }

                                }
                            }
                            if (penaltyAmount == 0 && penaltyTransactionFee == 0)
                            {
                                var penalty = await _context.DefaultDetails.FindAsync(penaltyId);
                                if (penalty != null)
                                {
                                    penalty.IsPending = false;
                                    await _context.SaveChangesAsync();

                                }
                            }
                        }

                    }
                    //end of for each loop
                    //if there is still amount left we make today's deposit
                    // update today pending depo
                    //update today pending fee
                    //update prepayments
                    if (memberTransactionAmount > 0)
                    {
                        if (isInstallmentPaid == true)
                        {

                        }
                        else
                        {
                            if (memberDueDate != Convert.ToDateTime(eastAfricaTime.ToString("yyyy-MM-dd")))
                            {
                                var payingMemberDailyContribitions = await _context.MemberAccounts
                                   .FirstOrDefaultAsync(a => a.AccountGroupId == memberGroupId &&
                                              a.AccountMemberId == memberNumber &&
                                              a.AccountUserId == memberUserId);

                                if (payingMemberDailyContribitions != null)
                                {
                                    var pendingContributionAmount = payingMemberDailyContribitions.AccountTodayPendingDeposit;
                                    var contributedAmount = payingMemberDailyContribitions.AccountTodayDeposit;
                                    var pendingContributionTransactionFee = payingMemberDailyContribitions.AccountTodayPendingTransactionFee;
                                    var contributedTransactionFee = payingMemberDailyContribitions.AccountTodayTransactionFee;
                                    var amountToDeduct = pendingContributionAmount + pendingContributionTransactionFee;

                                    if (pendingContributionAmount != 0)
                                    {
                                        if (memberTransactionAmount >= pendingContributionAmount)
                                        {

                                            await UpdateMemberAccountBalances("Update Current Balance", "Decrease", memberGroupId, memberNumber, memberUserId, pendingContributionAmount, null, null, null, null, null, null, pendingContributionAmount, null, null, null, null);
                                            await UpdateMemberAccountBalances("Update Prepaid Balance", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, pendingContributionAmount, null, null, null, null, null, null, null, null, null);
                                            await UpdateMemberAccountBalances("Update Today Deposit", "Increase", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, null, null, null, pendingContributionAmount, null, null, null);
                                            await UpdateMemberAccountBalances("Update Today Pending Deposit", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, null, null, null, null, pendingContributionAmount, null, null);
                                            memberTransactionAmount = (decimal)(memberTransactionAmount - pendingContributionAmount);
                                            pendingContributionAmount = (decimal)(pendingContributionAmount - pendingContributionAmount);
                                        }
                                        else
                                        {
                                            await UpdateMemberAccountBalances("Update Current Balance", "Decrease", memberGroupId, memberNumber, memberUserId, memberTransactionAmount, null, null, null, null, null, null, memberTransactionAmount, null, null, null, null);
                                            await UpdateMemberAccountBalances("Update Prepaid Balance", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, memberTransactionAmount, null, null, null, null, null, null, null, null, null);
                                            await UpdateMemberAccountBalances("Update Today Deposit", "Increase", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, null, null, null, memberTransactionAmount, null, null, null);
                                            await UpdateMemberAccountBalances("Update Today Pending Deposit", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, null, null, null, null, memberTransactionAmount, null, null);
                                            memberTransactionAmount = (decimal)(memberTransactionAmount - memberTransactionAmount);
                                            pendingContributionAmount = (decimal)(pendingContributionAmount - memberTransactionAmount);
                                        }
                                    }

                                    if (memberTransactionAmount > 0 && pendingContributionTransactionFee != 0)
                                    {
                                        if (pendingContributionTransactionFee != 0)
                                        {
                                            if (memberTransactionAmount >= pendingContributionTransactionFee)
                                            {
                                                await UpdateMemberAccountBalances("Update Current Balance", "Decrease", memberGroupId, memberNumber, memberUserId, pendingContributionTransactionFee, null, null, null, null, null, null, pendingContributionTransactionFee, null, null, null, null);
                                                await UpdateMemberAccountBalances("Update Prepaid Balance", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, pendingContributionTransactionFee, null, null, null, null, null, null, null, null, null);
                                                await UpdateMemberAccountBalances("Update Today Transaction Fee", "Increase", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, null, null, null, null, null, pendingContributionTransactionFee, null); ;
                                                await UpdateMemberAccountBalances("Update Today Pending Transaction Fee", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, null, null, null, null, null, null, pendingContributionTransactionFee);
                                                await UpdateGroupBalances("Update Current Balance", "Increase", memberGroupId, null, pendingContributionTransactionFee, null);
                                                await UpdateGroupBalances("Update Pygg Balance", "Increase", memberGroupId, pendingContributionTransactionFee, null, null);
                                                memberTransactionAmount = (decimal)(memberTransactionAmount - pendingContributionTransactionFee);
                                                pendingContributionTransactionFee = (decimal)(pendingContributionTransactionFee - pendingContributionTransactionFee);
                                            }
                                            else
                                            {
                                                await UpdateMemberAccountBalances("Update Current Balance", "Decrease", memberGroupId, memberNumber, memberUserId, memberTransactionAmount, null, null, null, null, null, null, memberTransactionAmount, null, null, null, null);
                                                await UpdateMemberAccountBalances("Update Prepaid Balance", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, memberTransactionAmount, null, null, null, null, null, null, null, null, null);
                                                await UpdateMemberAccountBalances("Update Today Transaction Fee", "Increase", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, null, null, null, null, null, memberTransactionAmount, null); ;
                                                await UpdateMemberAccountBalances("Update Today Pending Transaction Fee", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, null, null, null, null, null, null, memberTransactionAmount);
                                                await UpdateGroupBalances("Update Current Balance", "Increase", memberGroupId, null, pendingContributionTransactionFee, null);
                                                await UpdateGroupBalances("Update Pygg Balance", "Increase", memberGroupId, pendingContributionTransactionFee, null, null);
                                                memberTransactionAmount = (decimal)(memberTransactionAmount - memberTransactionAmount);
                                                pendingContributionTransactionFee = (decimal)(pendingContributionTransactionFee - memberTransactionAmount);
                                            }
                                        }
                                    }

                                    if (pendingContributionAmount == 0 && pendingContributionTransactionFee == 0)
                                    {
                                        var installmentUpdate = await _context.GroupDepositInstallments.FindAsync(installmentNo);
                                        if (installmentUpdate != null)
                                        {
                                            installmentUpdate.IsInstallmentPaid = true;
                                            await _context.SaveChangesAsync();
                                        }
                                    }
                                }
                            }
                            else if (memberDueDate == Convert.ToDateTime(eastAfricaTime.ToString("yyyy-MM-dd")))
                            {
                                var payingMemberDailyContribitions = await _context.MemberAccounts
                                   .FirstOrDefaultAsync(a => a.AccountGroupId == memberGroupId &&
                                              a.AccountMemberId == memberNumber &&
                                              a.AccountUserId == memberUserId);
                                if (payingMemberDailyContribitions != null)
                                {
                                    var contributedAmount = payingMemberDailyContribitions.AccountTodayDeposit;
                                    var pendingContributionTransactionFee = payingMemberDailyContribitions.AccountTodayPendingTransactionFee;
                                    var contributedTransactionFee = payingMemberDailyContribitions.AccountTodayTransactionFee;
                                    var amountToDeduct = pendingContributionTransactionFee;


                                    if (memberTransactionAmount > 0 && pendingContributionTransactionFee != 0)
                                    {
                                        if (pendingContributionTransactionFee != 0)
                                        {
                                            if (memberTransactionAmount >= pendingContributionTransactionFee)
                                            {
                                                await UpdateMemberAccountBalances("Update Current Balance", "Decrease", memberGroupId, memberNumber, memberUserId, pendingContributionTransactionFee, null, null, null, null, null, null, pendingContributionTransactionFee, null, null, null, null);
                                                await UpdateMemberAccountBalances("Update Prepaid Balance", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, pendingContributionTransactionFee, null, null, null, null, null, null, null, null, null); await UpdateMemberAccountBalances("Update Today Transaction Fee", "Increase", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, null, null, null, null, null, pendingContributionTransactionFee, null); ;
                                                await UpdateMemberAccountBalances("Update Today Pending Transaction Fee", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, null, null, null, null, null, null, pendingContributionTransactionFee);
                                                await UpdateGroupBalances("Update Current Balance", "Increase", memberGroupId, null, pendingContributionTransactionFee, null);
                                                await UpdateGroupBalances("Update Pygg Balance", "Increase", memberGroupId, pendingContributionTransactionFee, null, null);
                                                memberTransactionAmount = (decimal)(memberTransactionAmount - pendingContributionTransactionFee);
                                                pendingContributionTransactionFee = (decimal)(pendingContributionTransactionFee - pendingContributionTransactionFee);
                                            }
                                            else
                                            {
                                                await UpdateMemberAccountBalances("Update Current Balance", "Decrease", memberGroupId, memberNumber, memberUserId, memberTransactionAmount, null, null, null, null, null, null, memberTransactionAmount, null, null, null, null);
                                                await UpdateMemberAccountBalances("Update Prepaid Balance", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, memberTransactionAmount, null, null, null, null, null, null, null, null, null);
                                                await UpdateMemberAccountBalances("Update Today Transaction Fee", "Increase", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, null, null, null, null, null, memberTransactionAmount, null); ;
                                                await UpdateMemberAccountBalances("Update Today Pending Transaction Fee", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, null, null, null, null, null, null, memberTransactionAmount);
                                                await UpdateGroupBalances("Update Current Balance", "Increase", memberGroupId, null, pendingContributionTransactionFee, null);
                                                await UpdateGroupBalances("Update Pygg Balance", "Increase", memberGroupId, pendingContributionTransactionFee, null, null);
                                                memberTransactionAmount = (decimal)(memberTransactionAmount - memberTransactionAmount);
                                                pendingContributionTransactionFee = (decimal)(pendingContributionTransactionFee - memberTransactionAmount);
                                            }
                                        }
                                    }

                                    if (pendingContributionTransactionFee == 0)
                                    {
                                        var installmentUpdate = await _context.GroupDepositInstallments.FindAsync(installmentNo);
                                        if (installmentUpdate != null)
                                        {
                                            installmentUpdate.IsInstallmentPaid = true;
                                            await _context.SaveChangesAsync();
                                        }
                                    }

                                }
                            }
                        }
                    }
                    return true;
                }//*************************************************************************************************************
                else //Payment for member with no defaults, doesnt have to go through the defaults deductions but comes directly here
                {
                    // update today pending depo
                    //update today pending fee
                    //update prepayments
                    if (memberTransactionAmount > 0)
                    {
                        if (isInstallmentPaid == true)
                        {

                        }
                        else
                        {
                            if (memberDueDate != Convert.ToDateTime(eastAfricaTime.ToString("yyyy-MM-dd")))
                            {
                                var payingMemberDailyContribitions = await _context.MemberAccounts
                                .FirstOrDefaultAsync(a => a.AccountGroupId == memberGroupId &&
                                          a.AccountMemberId == memberNumber &&
                                          a.AccountUserId == memberUserId);

                                if (payingMemberDailyContribitions != null)
                                {
                                    var pendingContributionAmount = payingMemberDailyContribitions.AccountTodayPendingDeposit;
                                    var contributedAmount = payingMemberDailyContribitions.AccountTodayDeposit;
                                    var pendingContributionTransactionFee = payingMemberDailyContribitions.AccountTodayPendingTransactionFee;
                                    var contributedTransactionFee = payingMemberDailyContribitions.AccountTodayTransactionFee;
                                    var amountToDeduct = pendingContributionAmount + pendingContributionTransactionFee;

                                    if (pendingContributionAmount != 0)
                                    {
                                        if (memberTransactionAmount >= pendingContributionAmount)
                                        {

                                            await UpdateMemberAccountBalances("Update Current Balance", "Decrease", memberGroupId, memberNumber, memberUserId, pendingContributionAmount, null, null, null, null, null, null, pendingContributionAmount, null, null, null, null);
                                            await UpdateMemberAccountBalances("Update Prepaid Balance", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, pendingContributionAmount, null, null, null, null, null, null, null, null, null);
                                            await UpdateMemberAccountBalances("Update Today Deposit", "Increase", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, null, null, null, pendingContributionAmount, null, null, null);
                                            await UpdateMemberAccountBalances("Update Today Pending Deposit", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, null, null, null, null, pendingContributionAmount, null, null);
                                            memberTransactionAmount = (decimal)(memberTransactionAmount - pendingContributionAmount);
                                            pendingContributionAmount = (decimal)(pendingContributionAmount - pendingContributionAmount);
                                        }
                                        else
                                        {
                                            await UpdateMemberAccountBalances("Update Current Balance", "Decrease", memberGroupId, memberNumber, memberUserId, memberTransactionAmount, null, null, null, null, null, null, memberTransactionAmount, null, null, null, null);
                                            await UpdateMemberAccountBalances("Update Prepaid Balance", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, memberTransactionAmount, null, null, null, null, null, null, null, null, null);
                                            await UpdateMemberAccountBalances("Update Today Deposit", "Increase", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, null, null, null, memberTransactionAmount, null, null, null);
                                            await UpdateMemberAccountBalances("Update Today Pending Deposit", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, null, null, null, null, memberTransactionAmount, null, null);
                                            memberTransactionAmount = (decimal)(memberTransactionAmount - memberTransactionAmount);
                                            pendingContributionAmount = (decimal)(pendingContributionAmount - memberTransactionAmount);
                                        }
                                    }

                                    if (memberTransactionAmount > 0 && pendingContributionTransactionFee != 0)
                                    {
                                        if (pendingContributionTransactionFee != 0)
                                        {
                                            if (memberTransactionAmount >= pendingContributionTransactionFee)
                                            {
                                                await UpdateMemberAccountBalances("Update Current Balance", "Decrease", memberGroupId, memberNumber, memberUserId, pendingContributionTransactionFee, null, null, null, null, null, null, pendingContributionTransactionFee, null, null, null, null);
                                                await UpdateMemberAccountBalances("Update Prepaid Balance", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, pendingContributionTransactionFee, null, null, null, null, null, null, null, null, null);
                                                await UpdateMemberAccountBalances("Update Today Transaction Fee", "Increase", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, null, null, null, null, null, pendingContributionTransactionFee, null); ;
                                                await UpdateMemberAccountBalances("Update Today Pending Transaction Fee", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, null, null, null, null, null, null, pendingContributionTransactionFee);
                                                await UpdateGroupBalances("Update Current Balance", "Increase", memberGroupId, null, pendingContributionTransactionFee, null);
                                                await UpdateGroupBalances("Update Pygg Balance", "Increase", memberGroupId, pendingContributionTransactionFee, null, null);
                                                memberTransactionAmount = (decimal)(memberTransactionAmount - pendingContributionTransactionFee);
                                                pendingContributionTransactionFee = (decimal)(pendingContributionTransactionFee - pendingContributionTransactionFee);
                                            }
                                            else
                                            {
                                                await UpdateMemberAccountBalances("Update Current Balance", "Decrease", memberGroupId, memberNumber, memberUserId, memberTransactionAmount, null, null, null, null, null, null, memberTransactionAmount, null, null, null, null);
                                                await UpdateMemberAccountBalances("Update Prepaid Balance", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, memberTransactionAmount, null, null, null, null, null, null, null, null, null);
                                                await UpdateMemberAccountBalances("Update Today Transaction Fee", "Increase", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, null, null, null, null, null, memberTransactionAmount, null); ;
                                                await UpdateMemberAccountBalances("Update Today Pending Transaction Fee", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, null, null, null, null, null, null, memberTransactionAmount);
                                                await UpdateGroupBalances("Update Current Balance", "Increase", memberGroupId, null, memberTransactionAmount, null);
                                                await UpdateGroupBalances("Update Pygg Balance", "Increase", memberGroupId, memberTransactionAmount, null, null);
                                                memberTransactionAmount = (decimal)(memberTransactionAmount - memberTransactionAmount);
                                                pendingContributionTransactionFee = (decimal)(pendingContributionTransactionFee - memberTransactionAmount);
                                            }
                                        }
                                    }

                                    if (pendingContributionAmount == 0 && pendingContributionTransactionFee == 0)
                                    {
                                        var installmentUpdate = await _context.GroupDepositInstallments.FindAsync(installmentNo);
                                        if (installmentUpdate != null)
                                        {
                                            installmentUpdate.IsInstallmentPaid = true;
                                            await _context.SaveChangesAsync();
                                        }
                                    }
                                }
                            }
                            else if (memberDueDate == Convert.ToDateTime(eastAfricaTime.ToString("yyyy-MM-dd")))
                            {

                                var payingMemberDailyContribitions = await _context.MemberAccounts
                                   .FirstOrDefaultAsync(a => a.AccountGroupId == memberGroupId &&
                                              a.AccountMemberId == memberNumber &&
                                              a.AccountUserId == memberUserId);
                                if (payingMemberDailyContribitions != null)
                                {
                                    var contributedAmount = payingMemberDailyContribitions.AccountTodayDeposit;
                                    var pendingContributionTransactionFee = payingMemberDailyContribitions.AccountTodayPendingTransactionFee;
                                    var contributedTransactionFee = payingMemberDailyContribitions.AccountTodayTransactionFee;
                                    var amountToDeduct = pendingContributionTransactionFee;

                                    if (memberTransactionAmount > 0 && pendingContributionTransactionFee != 0)
                                    {
                                        if (pendingContributionTransactionFee != 0)
                                        {
                                            if (memberTransactionAmount >= pendingContributionTransactionFee)
                                            {
                                                await UpdateMemberAccountBalances("Update Current Balance", "Decrease", memberGroupId, memberNumber, memberUserId, pendingContributionTransactionFee, null, null, null, null, null, null, pendingContributionTransactionFee, null, null, null, null);
                                                await UpdateMemberAccountBalances("Update Prepaid Balance", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, pendingContributionTransactionFee, null, null, null, null, null, null, null, null, null);
                                                await UpdateMemberAccountBalances("Update Today Transaction Fee", "Increase", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, null, null, null, null, null, pendingContributionTransactionFee, null); ;
                                                await UpdateMemberAccountBalances("Update Today Pending Transaction Fee", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, null, null, null, null, null, null, pendingContributionTransactionFee);
                                                await UpdateGroupBalances("Update Current Balance", "Increase", memberGroupId, null, pendingContributionTransactionFee, null);
                                                await UpdateGroupBalances("Update Pygg Balance", "Increase", memberGroupId, pendingContributionTransactionFee, null, null);
                                                memberTransactionAmount = (decimal)(memberTransactionAmount - pendingContributionTransactionFee);
                                                pendingContributionTransactionFee = (decimal)(pendingContributionTransactionFee - pendingContributionTransactionFee);
                                            }
                                            else
                                            {
                                                await UpdateMemberAccountBalances("Update Current Balance", "Decrease", memberGroupId, memberNumber, memberUserId, memberTransactionAmount, null, null, null, null, null, null, memberTransactionAmount, null, null, null, null);
                                                await UpdateMemberAccountBalances("Update Prepaid Balance", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, memberTransactionAmount, null, null, null, null, null, null, null, null, null);
                                                await UpdateMemberAccountBalances("Update Today Transaction Fee", "Increase", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, null, null, null, null, null, memberTransactionAmount, null); ;
                                                await UpdateMemberAccountBalances("Update Today Pending Transaction Fee", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, null, null, null, null, null, null, memberTransactionAmount);
                                                await UpdateGroupBalances("Update Current Balance", "Increase", memberGroupId, null, pendingContributionTransactionFee, null);
                                                await UpdateGroupBalances("Update Pygg Balance", "Increase", memberGroupId, pendingContributionTransactionFee, null, null);
                                                memberTransactionAmount = (decimal)(memberTransactionAmount - memberTransactionAmount);
                                                pendingContributionTransactionFee = (decimal)(pendingContributionTransactionFee - memberTransactionAmount);
                                            }
                                        }
                                    }

                                    if (pendingContributionTransactionFee == 0)
                                    {
                                        var installmentUpdate = await _context.GroupDepositInstallments.FindAsync(installmentNo);
                                        if (installmentUpdate != null)
                                        {
                                            installmentUpdate.IsInstallmentPaid = true;
                                            await _context.SaveChangesAsync();
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            }
            return true;
        }

        public async Task UpdateMemberPendingContributionsFromInstallments(int groupId)
        {
            DateTime serverTimeUtc = DateTime.UtcNow;
            TimeZoneInfo eastAfricaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. Africa Standard Time");
            DateTime eastAfricaTime = TimeZoneInfo.ConvertTimeFromUtc(serverTimeUtc, eastAfricaTimeZone);

            var maxInstallmentSeries = await GetGroupCurrentRobinSeries(groupId);
            if (maxInstallmentSeries == 0) return;

            var query = from installment in _context.GroupDepositInstallments
                        where installment.InstallmentGroupId == groupId &&
                              installment.InstallmentDate == Convert.ToDateTime(eastAfricaTime.ToString("yyyy-MM-dd")) &&
                              installment.IsInstallmentPaid == false &&
                              installment.InstallmentSeries == maxInstallmentSeries
                        select new
                        {
                            installment.InstallmentGroupId,
                            installment.InstallmentMemberId,
                            installment.InstallmentUserId,
                            installment.InstallmentAmount,
                            installment.InstallmentCharge,
                            installment.MemberDueDate,
                            installment.InstallmentDate,
                            installment.IsInstallmentPaid,
                            installment.MemberRobinNumber,
                            installment.InstallmentSeries,
                            installment.InstallmentNo,
                            installment.InstallmentContributionType

                        };

            var accountUpdates = await query.ToListAsync();

            var installmentDate = accountUpdates.FirstOrDefault().InstallmentDate;

            var nonPaying = accountUpdates.Where(a => a.MemberDueDate == installmentDate).FirstOrDefault();
            var nonPayingMemberId = nonPaying.InstallmentMemberId;
            var nonPayingMemberGroupId = nonPaying.InstallmentGroupId;
            var nonPayingMemberUserId = nonPaying.InstallmentUserId;


            if (accountUpdates.Count > 0)
            {

                foreach (var update in accountUpdates)
                {
                    var account = await _context.MemberAccounts
                        .FirstOrDefaultAsync(a => a.AccountGroupId == update.InstallmentGroupId &&
                                                   a.AccountMemberId == update.InstallmentMemberId &&
                                                     a.AccountIsActive == true);

                    if (account != null)
                    {
                        var memberGroupId = account.AccountGroupId;
                        var memberNumber = account.AccountMemberId;
                        var memberAccountNumber = account.AccountNumber;
                        var memberUserId = account.AccountUserId;
                        var pendingContributionAmount = account.AccountTodayPendingDeposit;
                        var pendingContributionTransactionFee = account.AccountTodayPendingTransactionFee;
                        var totalContribution = pendingContributionAmount + pendingContributionTransactionFee;

                        ///Fines should have be posted by now and fine amount captured in DefautDetails table
                        ///Next is to update the pending defaulted amount and transaaction fees owed to the member receiving

                        var fineDefaults = await _context.DefaultDetails
                      .FirstOrDefaultAsync(a => a.PenaltyGroupId == update.InstallmentGroupId &&
                                                 a.PenaltyMemberNumber == update.InstallmentMemberId &&
                                                  a.PenaltyUserId == update.InstallmentUserId &&
                                                  a.ContributionTypeId == update.InstallmentContributionType &&
                                                   a.PenaltlyDate == Convert.ToDateTime(Convert.ToDateTime(installmentDate).ToString("yyyy-MM-dd")) &&
                                                   a.IsPending == true);

                        if (fineDefaults != null)
                        {
                            fineDefaults.PenaltyAmount += pendingContributionAmount;
                            fineDefaults.PenaltyTransactionFee += pendingContributionTransactionFee;
                            await UpdateMemberAccountBalances("Update Default Amount To Pay", "Increase", memberGroupId, memberNumber, memberUserId, null, null, null, totalContribution, null, null, null, null, null, null, null, null);
                            await UpdateMemberAccountBalances("Update Default Amount To Receive", "Increase", nonPayingMemberGroupId, nonPayingMemberId, nonPayingMemberUserId, null, null, null, null, pendingContributionAmount, null, null, null, null, null, null, null);
                            await UpdateMemberAccountBalances("Update Available Balance", "Increase", nonPayingMemberGroupId, nonPayingMemberId, nonPayingMemberUserId, null, pendingContributionAmount, null, null, null, null, null, null, null, null, null, null);
                            await UpdateGroupBalances("Update Available Balance", "Increase", nonPayingMemberGroupId, null, null, pendingContributionTransactionFee);

                        }


                        if (update.MemberDueDate == update.InstallmentDate)
                        {
                            account.AccountTodayPendingDeposit = 0;
                            account.AccountTodayPendingTransactionFee = update.InstallmentCharge;
                        }
                        else
                        {
                            account.AccountTodayPendingDeposit = update.InstallmentAmount;
                            account.AccountTodayPendingTransactionFee = update.InstallmentCharge;
                        }
                        account.AccountTodayDeposit = 0;
                        account.AccountTodayTransactionFee = 0;
                    }
                }
            }
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Picks the latest group robin parameters and updates members accounts with robin amounts
        /// required for contributions.
        /// </summary>
        /// <param name="a">groupId.</param>
        /// <returns></returns>

        public async Task<bool> UpdateMemberAccountsFromInstallments()
        {
            DateTime serverTimeUtc = DateTime.UtcNow;
            TimeZoneInfo eastAfricaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. Africa Standard Time");
            DateTime eastAfricaTime = TimeZoneInfo.ConvertTimeFromUtc(serverTimeUtc, eastAfricaTimeZone);

            DateTime midNight = eastAfricaTime.Date.AddDays(1).AddSeconds(-1);

            //TimeSpan timeOfDay = eastAfricaTime.TimeOfDay;
            //DateTime dateOfDay = eastAfricaTime.Date;
            //TimeSpan endOfDay = new(01, 40, 0);
            if (eastAfricaTime < midNight) return false;

            if (eastAfricaTime >= midNight)
            {
                var installmentGroups = await _context.GroupDepositInstallments
                .Where(installment => installment.InstallmentDate == Convert.ToDateTime(eastAfricaTime.ToString("yyyy-MM-dd")))
                .Select(installment => installment.InstallmentGroupId)
                .Distinct()
                .ToListAsync();

                if (installmentGroups.Count == 0) return false;

                foreach (int groupId in installmentGroups)
                {

                    await UpdateMemberPendingContributionsFromInstallments(groupId);

                    var maxInstallmentSeries = await GetGroupCurrentRobinSeries(groupId);
                    if (maxInstallmentSeries == 0) return false;

                    var query = from installment in _context.GroupDepositInstallments
                                where installment.InstallmentGroupId == groupId &&
                                      installment.InstallmentDate == Convert.ToDateTime(eastAfricaTime.ToString("yyyy-MM-dd")) &&
                                      installment.IsInstallmentPaid == false &&
                                      installment.InstallmentSeries == maxInstallmentSeries
                                select new
                                {
                                    installment.InstallmentGroupId,
                                    installment.InstallmentMemberId,
                                    installment.InstallmentAmount,
                                    installment.InstallmentCharge,
                                    installment.MemberDueDate,
                                    installment.InstallmentDate,
                                    installment.IsInstallmentPaid,
                                    installment.MemberRobinNumber,
                                    installment.InstallmentSeries,
                                    installment.InstallmentNo

                                };

                    var accountUpdates = await query.ToListAsync();

                    if (accountUpdates.Count > 0)
                    {
                        foreach (var update in accountUpdates)
                        {
                            var account = await _context.MemberAccounts
                                .FirstOrDefaultAsync(a => a.AccountGroupId == update.InstallmentGroupId &&
                                                          a.AccountMemberId == update.InstallmentMemberId &&
                                                             a.AccountIsActive == true);

                            if (account != null)
                            {
                                var robinSeries = update.InstallmentSeries;
                                var prepaidAmount = account.AccountPrepaidBalance;
                                var memberGroupId = account.AccountGroupId;
                                var memberNumber = account.AccountMemberId;
                                var memberAccountNumber = account.AccountNumber;
                                var memberUserId = account.AccountUserId;
                                var pendingContributionAmount = account.AccountTodayPendingDeposit;
                                var contributedAmount = account.AccountTodayDeposit;
                                var pendingContributionTransactionFee = account.AccountTodayPendingTransactionFee;
                                var contributedTransactionFee = account.AccountTodayTransactionFee;
                                var totalContribution = pendingContributionAmount + pendingContributionTransactionFee;

                                //Make deposits from prepayments
                                if (prepaidAmount > 0)
                                {
                                    var payingMemberDefaults = await GetPayingMemberDefaultDetails(memberGroupId, memberNumber, memberUserId);

                                    if (payingMemberDefaults.Count > 0)
                                    {
                                        foreach (var payingMemberDefault in payingMemberDefaults)
                                        {
                                            int serialNumber = await GetNextTransactionSerialNo();
                                            if (prepaidAmount > 0)
                                            {
                                                var penaltyId = payingMemberDefault.PenaltyId;
                                                var penaltyAmount = payingMemberDefault.PenaltyAmount;
                                                var penaltyTransactionFee = payingMemberDefault.PenaltyTransactionFee;
                                                var isPending = payingMemberDefault.IsPending;
                                                var amountToDeduct = penaltyAmount + penaltyTransactionFee;
                                                int penaltyOwedToMemberId = (int)payingMemberDefault.PenaltyOwedToMemberId;
                                                int penaltyOwedToMemberGroupId = (int)payingMemberDefault.PenaltyOwedToGroupId;
                                                int penaltyOwedToUserId = (int)payingMemberDefault.PenaltyOwedToUserId;
                                                var penaltyOwedContributionId = payingMemberDefault.PenaltyOwedToContributionId;
                                                var penaltyOwedToFineId = payingMemberDefault.PenaltyOwedToFineId;

                                                if (prepaidAmount < amountToDeduct)
                                                {
                                                    amountToDeduct = prepaidAmount;
                                                }

                                                MemberTransaction defaultTransaction = new()
                                                {
                                                    TransactionSerialId = serialNumber,
                                                    TransactionAccountNumber = memberAccountNumber,
                                                    TransactionMemberId = memberNumber,
                                                    TransactionGroupId = memberGroupId,
                                                    TransactionUserId = memberUserId,
                                                    ContributionId = penaltyOwedContributionId,
                                                    FineId = 0,
                                                    PaymentTypeId = "PYT01",
                                                    TransactionCode = null,
                                                    TransactionTypeId = "TR01",
                                                    TransactionAmount = (decimal?)Convert.ToDouble(amountToDeduct),
                                                    TransactionDate = Convert.ToDateTime(Convert.ToDateTime(DateTime.Now.Date)),
                                                    TransactionValueDate = Convert.ToDateTime(Convert.ToDateTime(DateTime.Now.Date)),
                                                    TransactionGroupFineAccount = null,
                                                    TransactionContributionAccount = "DEPSGRA031",
                                                    TransactionNarration = " Amount deducted automatically by system for default recovery from Prepayment for default on member number " + penaltyOwedToMemberId.ToString() + " for robin series " + robinSeries.ToString(),
                                                    TransactionIsConfirmed = true,
                                                    TransactionIsDefaultRecovery = true,
                                                    TransactionDefaultRecoveryMemberId = penaltyOwedToMemberId,
                                                    TransactionDefaultRecoveryGroupId = penaltyOwedToMemberGroupId,
                                                    TransactionDefaultRecoveryUserId = penaltyOwedToUserId,
                                                    TransactionDefaultRecoveryContributionId = penaltyOwedContributionId,
                                                    TransactionFineRecoveryFineId = penaltyOwedToFineId,
                                                    TransactionCreatedBy = 1, //remember to crate a system account for such transactions
                                                    TransactionCreatedOn = DateTime.Now,

                                                    SafInvoiceId = "N/A",
                                                    SafTransState = "N/A",
                                                    SafCharges = 0,
                                                    SafNetAmount = 0,
                                                    SafCurrency = "KES",
                                                    SafValue = 0,
                                                    SafAccount = null,
                                                    SafApiRef = null,
                                                    SafMpesaRef = null,
                                                    SafHost = null,
                                                    SafRetry = null,
                                                    SafFailedReason = null,
                                                    SafFailedCode = null,
                                                    SafCreatedDate = null,
                                                    SafCreatedTime = null,
                                                    SafCustomerPhoneNumber = null,
                                                    SafCustomerCountry = null,
                                                    SafCustomerEmail = null,
                                                    SafCustomerLastname = null

                                                };

                                                _context.MemberTransactions.Add(defaultTransaction);
                                                await _context.SaveChangesAsync();

                                                if (penaltyAmount != 0)
                                                {
                                                    if (prepaidAmount >= penaltyAmount)
                                                    {
                                                        await UpdateMemberAccountBalances("Update Prepaid Balance", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, penaltyAmount, null, null, null, null, null, null, null, null, null);
                                                        await UpdateMemberAccountBalances("Update Default Amount To Pay", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, null, penaltyAmount, null, null, null, penaltyAmount, null, null, null, null);
                                                        await UpdateMemberAccountBalances("Update Default Amount To Receive", "Decrease", penaltyOwedToMemberGroupId, penaltyOwedToMemberId, penaltyOwedToUserId, null, null, null, null, penaltyAmount, null, null, null, null, null, null, null);
                                                        await UpdateMemberAccountBalances("Update Prepaid Balance", "Increase", penaltyOwedToMemberGroupId, penaltyOwedToMemberId, penaltyOwedToUserId, null, null, penaltyAmount, null, null, null, null, null, null, null, null, null);
                                                        await UpdateMemberAccountBalances("Update Current Balance", "Increase", penaltyOwedToMemberGroupId, penaltyOwedToMemberId, penaltyOwedToUserId, penaltyAmount, null, null, null, null, null, null, penaltyAmount, null, null, null, null);
                                                        await UpdateMemberAccountBalances("Update Available Balance", "Decrease", penaltyOwedToMemberGroupId, penaltyOwedToMemberId, penaltyOwedToUserId, null, penaltyAmount, null, null, null, null, null, null, null, null, null, null);

                                                        prepaidAmount = (decimal)(prepaidAmount - penaltyAmount);
                                                        penaltyAmount = (decimal)(penaltyAmount - penaltyAmount);
                                                    }
                                                    else
                                                    {
                                                        await UpdateMemberAccountBalances("Update Prepaid Balance", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, prepaidAmount, null, null, null, null, null, null, null, null, null);
                                                        await UpdateMemberAccountBalances("Update Default Amount To Pay", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, null, prepaidAmount, null, null, null, prepaidAmount, null, null, null, null);
                                                        await UpdateMemberAccountBalances("Update Default Amount To Receive", "Decrease", penaltyOwedToMemberGroupId, penaltyOwedToMemberId, penaltyOwedToUserId, null, null, null, null, prepaidAmount, null, null, null, null, null, null, null);
                                                        await UpdateMemberAccountBalances("Update Prepaid Balance", "Increase", penaltyOwedToMemberGroupId, penaltyOwedToMemberId, penaltyOwedToUserId, null, null, prepaidAmount, null, null, null, null, null, null, null, null, null);
                                                        await UpdateMemberAccountBalances("Update Current Balance", "Increase", penaltyOwedToMemberGroupId, penaltyOwedToMemberId, penaltyOwedToUserId, prepaidAmount, null, null, null, null, null, null, prepaidAmount, null, null, null, null);
                                                        await UpdateMemberAccountBalances("Update Available Balance", "Decrease", penaltyOwedToMemberGroupId, penaltyOwedToMemberId, penaltyOwedToUserId, null, prepaidAmount, null, null, null, null, null, null, null, null, null, null);

                                                        prepaidAmount = (decimal)(prepaidAmount - prepaidAmount);
                                                        penaltyAmount = (decimal)(penaltyAmount - prepaidAmount);

                                                    }
                                                }

                                                if (prepaidAmount > 0 && penaltyTransactionFee != 0)
                                                {
                                                    if (penaltyTransactionFee != 0)
                                                    {
                                                        if (prepaidAmount >= penaltyTransactionFee)
                                                        {
                                                            await UpdateMemberAccountBalances("Update Prepaid Balance", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, penaltyTransactionFee, null, null, null, null, null, null, null, null, null);
                                                            await UpdateMemberAccountBalances("Update Default Amount To Pay", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, null, penaltyTransactionFee, null, null, null, penaltyTransactionFee, null, null, null, null);
                                                            await UpdateGroupBalances("Update Current Balance", "Increase", memberGroupId, null, penaltyTransactionFee, null);
                                                            await UpdateGroupBalances("Update Pygg Balance", "Increase", memberGroupId, penaltyTransactionFee, null, null);
                                                            await UpdateGroupBalances("Update Available Balance", "Decrease", memberGroupId, null, null, penaltyTransactionFee);
                                                            prepaidAmount = (decimal)(prepaidAmount - penaltyTransactionFee);
                                                            penaltyTransactionFee = (decimal)(penaltyTransactionFee - penaltyTransactionFee);
                                                        }
                                                        else
                                                        {
                                                            await UpdateMemberAccountBalances("Update Prepaid Balance", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, prepaidAmount, null, null, null, null, null, null, null, null, null);
                                                            await UpdateMemberAccountBalances("Update Default Amount To Pay", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, null, prepaidAmount, null, null, null, prepaidAmount, null, null, null, null);
                                                            await UpdateGroupBalances("Update Current Balance", "Increase", memberGroupId, null, prepaidAmount, null);
                                                            await UpdateGroupBalances("Update Pygg Balance", "Increase", memberGroupId, prepaidAmount, null, null);
                                                            await UpdateGroupBalances("Update Available Balance", "Decrease", memberGroupId, null, null, prepaidAmount);
                                                            prepaidAmount = (decimal)(prepaidAmount - prepaidAmount);
                                                            penaltyTransactionFee = (decimal)(penaltyTransactionFee - prepaidAmount);
                                                        }
                                                    }
                                                }

                                                if (penaltyAmount == 0 && penaltyTransactionFee == 0)
                                                {
                                                    var penalty = await _context.DefaultDetails.FindAsync(penaltyId);
                                                    if (penalty != null)
                                                    {
                                                        penalty.IsPending = false;
                                                        await _context.SaveChangesAsync();

                                                    }
                                                }
                                            }
                                        }


                                        //end of for each loop
                                        // update today pending depo for member with defaults
                                        if (prepaidAmount > 0)
                                        {
                                            var payingMemberDailyContribitions = await _context.MemberAccounts
                                         .FirstOrDefaultAsync(a => a.AccountGroupId == memberGroupId &&
                                                          a.AccountMemberId == memberNumber &&
                                                          a.AccountUserId == memberUserId);


                                            if (payingMemberDailyContribitions != null)
                                            {

                                                if (update.MemberDueDate == Convert.ToDateTime(eastAfricaTime.ToString("yyyy-MM-dd")))//Member due does not pay contribution but pays transaction fee
                                                {
                                                    int serialNumber = await GetNextTransactionSerialNo();
                                                    MemberTransaction payingMember = new()
                                                    {
                                                        TransactionSerialId = serialNumber,
                                                        TransactionAccountNumber = memberAccountNumber,
                                                        TransactionMemberId = memberNumber,
                                                        TransactionGroupId = memberGroupId,
                                                        TransactionUserId = memberUserId,
                                                        ContributionId = 0,
                                                        ContributionTypeId = "CRT01",
                                                        FineId = 0,
                                                        PaymentTypeId = "PYT01",
                                                        TransactionCode = null,
                                                        TransactionTypeId = "TR01",
                                                        TransactionAmount = prepaidAmount,
                                                        TransactionDate = Convert.ToDateTime(Convert.ToDateTime(DateTime.Now.Date)),
                                                        TransactionValueDate = Convert.ToDateTime(Convert.ToDateTime(DateTime.Now.Date)),
                                                        TransactionGroupFineAccount = null,
                                                        TransactionContributionAccount = "DEPSGRA031",
                                                        TransactionNarration = " Transaction fee deducted for member due",
                                                        TransactionIsConfirmed = true,
                                                        TransactionIsDefaultRecovery = false,
                                                        TransactionDefaultRecoveryMemberId = 0,
                                                        TransactionDefaultRecoveryGroupId = 0,
                                                        TransactionDefaultRecoveryUserId = 0,
                                                        TransactionDefaultRecoveryContributionId = 0,
                                                        TransactionFineRecoveryFineId = 0,
                                                        TransactionCreatedBy = 1,//Remember to create a system account to handle this transactions
                                                        TransactionCreatedOn = DateTime.Now,

                                                        SafInvoiceId = "N/A",
                                                        SafTransState = "N/A",
                                                        SafCharges = 0,
                                                        SafNetAmount = 0,
                                                        SafCurrency = "KES",
                                                        SafValue = 0,
                                                        SafAccount = null,
                                                        SafApiRef = null,
                                                        SafMpesaRef = null,
                                                        SafHost = null,
                                                        SafRetry = null,
                                                        SafFailedReason = null,
                                                        SafFailedCode = null,
                                                        SafCreatedDate = null,
                                                        SafCreatedTime = null,
                                                        SafCustomerPhoneNumber = null,
                                                        SafCustomerCountry = null,
                                                        SafCustomerEmail = null,
                                                        SafCustomerLastname = null
                                                    };

                                                    _context.MemberTransactions.Add(payingMember);
                                                    await _context.SaveChangesAsync();

                                                    if (prepaidAmount > 0 && pendingContributionTransactionFee != 0)
                                                    {
                                                        if (pendingContributionTransactionFee != 0)
                                                        {
                                                            if (prepaidAmount >= pendingContributionTransactionFee)
                                                            {
                                                                await UpdateMemberAccountBalances("Update Prepaid Balance", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, pendingContributionTransactionFee, null, null, null, null, null, null, null, null, null);
                                                                await UpdateMemberAccountBalances("Update Today Transaction Fee", "Increase", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, null, null, null, null, null, pendingContributionTransactionFee, null); ;
                                                                await UpdateMemberAccountBalances("Update Today Pending Transaction Fee", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, null, null, null, null, null, null, pendingContributionTransactionFee);
                                                                await UpdateGroupBalances("Update Current Balance", "Increase", memberGroupId, null, pendingContributionTransactionFee, null);
                                                                await UpdateGroupBalances("Update Pygg Balance", "Increase", memberGroupId, pendingContributionTransactionFee, null, null);
                                                                prepaidAmount = (decimal)(prepaidAmount - pendingContributionTransactionFee);
                                                                pendingContributionTransactionFee = (decimal)(pendingContributionTransactionFee - pendingContributionTransactionFee);
                                                            }
                                                            else
                                                            {
                                                                await UpdateMemberAccountBalances("Update Prepaid Balance", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, prepaidAmount, null, null, null, null, null, null, null, null, null);
                                                                await UpdateMemberAccountBalances("Update Today Transaction Fee", "Increase", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, null, null, null, null, null, prepaidAmount, null); ;
                                                                await UpdateMemberAccountBalances("Update Today Pending Transaction Fee", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, null, null, null, null, null, null, prepaidAmount);
                                                                await UpdateGroupBalances("Update Current Balance", "Increase", memberGroupId, null, prepaidAmount, null);
                                                                await UpdateGroupBalances("Update Pygg Balance", "Increase", memberGroupId, prepaidAmount, null, null);
                                                                prepaidAmount = (decimal)(prepaidAmount - prepaidAmount);
                                                                pendingContributionTransactionFee = (decimal)(pendingContributionTransactionFee - prepaidAmount);
                                                            }
                                                        }
                                                    }
                                                    if (pendingContributionTransactionFee == 0)
                                                    {
                                                        var installmentUpdate = await _context.GroupDepositInstallments.FindAsync(update.InstallmentNo);
                                                        if (installmentUpdate != null)
                                                        {
                                                            installmentUpdate.IsInstallmentPaid = true;
                                                            await _context.SaveChangesAsync();
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    int serialNumber = await GetNextTransactionSerialNo();

                                                    MemberTransaction payingMember = new()
                                                    {
                                                        TransactionSerialId = serialNumber,
                                                        TransactionAccountNumber = memberAccountNumber,
                                                        TransactionMemberId = memberNumber,
                                                        TransactionGroupId = memberGroupId,
                                                        TransactionUserId = memberUserId,
                                                        ContributionId = 0,
                                                        ContributionTypeId = "CRT01",
                                                        FineId = 0,
                                                        PaymentTypeId = "PYT01",
                                                        TransactionCode = null,
                                                        TransactionTypeId = "TR01",
                                                        TransactionAmount = prepaidAmount,
                                                        TransactionDate = Convert.ToDateTime(Convert.ToDateTime(DateTime.Now.Date)),
                                                        TransactionValueDate = Convert.ToDateTime(Convert.ToDateTime(DateTime.Now.Date)),
                                                        TransactionGroupFineAccount = null,
                                                        TransactionContributionAccount = "DEPSGRA031",
                                                        TransactionNarration = " Amount deducted automatically by system for contribution from Prepayment",
                                                        TransactionIsConfirmed = true,
                                                        TransactionIsDefaultRecovery = false,
                                                        TransactionDefaultRecoveryMemberId = 0,
                                                        TransactionDefaultRecoveryGroupId = 0,
                                                        TransactionDefaultRecoveryUserId = 0,
                                                        TransactionDefaultRecoveryContributionId = 0,
                                                        TransactionFineRecoveryFineId = 0,
                                                        TransactionCreatedBy = 1,//Remember to create a system account to handle this transactions
                                                        TransactionCreatedOn = DateTime.Now,

                                                        SafInvoiceId = "N/A",
                                                        SafTransState = "N/A",
                                                        SafCharges = 0,
                                                        SafNetAmount = 0,
                                                        SafCurrency = "KES",
                                                        SafValue = 0,
                                                        SafAccount = null,
                                                        SafApiRef = null,
                                                        SafMpesaRef = null,
                                                        SafHost = null,
                                                        SafRetry = null,
                                                        SafFailedReason = null,
                                                        SafFailedCode = null,
                                                        SafCreatedDate = null,
                                                        SafCreatedTime = null,
                                                        SafCustomerPhoneNumber = null,
                                                        SafCustomerCountry = null,
                                                        SafCustomerEmail = null,
                                                        SafCustomerLastname = null
                                                    };

                                                    _context.MemberTransactions.Add(payingMember);
                                                    await _context.SaveChangesAsync();

                                                    if (pendingContributionAmount != 0)
                                                    {
                                                        if (prepaidAmount >= pendingContributionAmount)
                                                        {
                                                            await UpdateMemberAccountBalances("Update Prepaid Balance", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, pendingContributionAmount, null, null, null, null, null, null, null, null, null);
                                                            await UpdateMemberAccountBalances("Update Today Deposit", "Increase", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, null, null, null, pendingContributionAmount, null, null, null);
                                                            await UpdateMemberAccountBalances("Update Today Pending Deposit", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, null, null, null, null, pendingContributionAmount, null, null);
                                                            prepaidAmount = (decimal)(prepaidAmount - pendingContributionAmount);
                                                            pendingContributionAmount = (decimal)(pendingContributionAmount - pendingContributionAmount);
                                                        }
                                                        else
                                                        {
                                                            await UpdateMemberAccountBalances("Update Prepaid Balance", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, prepaidAmount, null, null, null, null, null, null, null, null, null);
                                                            await UpdateMemberAccountBalances("Update Today Deposit", "Increase", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, null, null, null, prepaidAmount, null, null, null);
                                                            await UpdateMemberAccountBalances("Update Today Pending Deposit", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, null, null, null, null, prepaidAmount, null, null);
                                                            prepaidAmount = (decimal)(prepaidAmount - prepaidAmount);
                                                            pendingContributionAmount = (decimal)(pendingContributionAmount - prepaidAmount);
                                                        }
                                                    }



                                                    if (prepaidAmount > 0 && pendingContributionTransactionFee != 0)
                                                    {
                                                        if (pendingContributionTransactionFee != 0)
                                                        {
                                                            if (prepaidAmount >= pendingContributionTransactionFee)
                                                            {
                                                                await UpdateMemberAccountBalances("Update Prepaid Balance", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, pendingContributionTransactionFee, null, null, null, null, null, null, null, null, null);
                                                                await UpdateMemberAccountBalances("Update Today Transaction Fee", "Increase", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, null, null, null, null, null, pendingContributionTransactionFee, null); ;
                                                                await UpdateMemberAccountBalances("Update Today Pending Transaction Fee", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, null, null, null, null, null, null, pendingContributionTransactionFee);
                                                                await UpdateGroupBalances("Update Current Balance", "Increase", memberGroupId, null, pendingContributionTransactionFee, null);
                                                                await UpdateGroupBalances("Update Pygg Balance", "Increase", memberGroupId, pendingContributionTransactionFee, null, null);
                                                                prepaidAmount = (decimal)(prepaidAmount - pendingContributionTransactionFee);
                                                                pendingContributionTransactionFee = (decimal)(pendingContributionTransactionFee - pendingContributionTransactionFee);
                                                            }
                                                            else
                                                            {
                                                                await UpdateMemberAccountBalances("Update Prepaid Balance", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, prepaidAmount, null, null, null, null, null, null, null, null, null);
                                                                await UpdateMemberAccountBalances("Update Today Transaction Fee", "Increase", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, null, null, null, null, null, prepaidAmount, null); ;
                                                                await UpdateMemberAccountBalances("Update Today Pending Transaction Fee", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, null, null, null, null, null, null, prepaidAmount);
                                                                await UpdateGroupBalances("Update Current Balance", "Increase", memberGroupId, null, prepaidAmount, null);
                                                                await UpdateGroupBalances("Update Pygg Balance", "Increase", memberGroupId, prepaidAmount, null, null);
                                                                prepaidAmount = (decimal)(prepaidAmount - prepaidAmount);
                                                                pendingContributionTransactionFee = (decimal)(pendingContributionTransactionFee - prepaidAmount);
                                                            }
                                                        }
                                                    }

                                                }

                                                if (pendingContributionAmount == 0 && pendingContributionTransactionFee == 0)
                                                {
                                                    var installmentUpdate = await _context.GroupDepositInstallments.FindAsync(update.InstallmentNo);
                                                    if (installmentUpdate != null)
                                                    {
                                                        installmentUpdate.IsInstallmentPaid = true;
                                                        await _context.SaveChangesAsync();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else // Handle member who is paying without defaults*************************************************************
                                    {
                                        //end of for each loop
                                        // update today pending depo
                                        //update today pending fee

                                        if (prepaidAmount > 0)
                                        {
                                            var payingMemberDailyContribitions = await _context.MemberAccounts
                                         .FirstOrDefaultAsync(a => a.AccountGroupId == memberGroupId &&
                                                          a.AccountMemberId == memberNumber &&
                                                          a.AccountUserId == memberUserId);


                                            if (payingMemberDailyContribitions != null)
                                            {

                                                if (update.MemberDueDate == Convert.ToDateTime(eastAfricaTime.ToString("yyyy-MM-dd")))//Member due does not pay contribution
                                                {

                                                    int serialNumber = await GetNextTransactionSerialNo();
                                                    MemberTransaction payingMember = new()
                                                    {
                                                        TransactionSerialId = serialNumber,
                                                        TransactionAccountNumber = memberAccountNumber,
                                                        TransactionMemberId = memberNumber,
                                                        TransactionGroupId = memberGroupId,
                                                        TransactionUserId = memberUserId,
                                                        ContributionId = 0,
                                                        ContributionTypeId = "CRT01",
                                                        FineId = 0,
                                                        PaymentTypeId = "PYT01",
                                                        TransactionCode = null,
                                                        TransactionTypeId = "TR01",
                                                        TransactionAmount = prepaidAmount,
                                                        TransactionDate = Convert.ToDateTime(Convert.ToDateTime(DateTime.Now.Date)),
                                                        TransactionValueDate = Convert.ToDateTime(Convert.ToDateTime(DateTime.Now.Date)),
                                                        TransactionGroupFineAccount = null,
                                                        TransactionContributionAccount = "DEPSGRA031",
                                                        TransactionNarration = " Amount deducted automatically by system for contribution from Prepayment",
                                                        TransactionIsConfirmed = true,
                                                        TransactionIsDefaultRecovery = false,
                                                        TransactionDefaultRecoveryMemberId = 0,
                                                        TransactionDefaultRecoveryGroupId = 0,
                                                        TransactionDefaultRecoveryUserId = 0,
                                                        TransactionDefaultRecoveryContributionId = 0,
                                                        TransactionFineRecoveryFineId = 0,
                                                        TransactionCreatedBy = 1,//Remember to create a system account to handle this transactions
                                                        TransactionCreatedOn = DateTime.Now,

                                                        SafInvoiceId = "N/A",
                                                        SafTransState = "N/A",
                                                        SafCharges = 0,
                                                        SafNetAmount = 0,
                                                        SafCurrency = "KES",
                                                        SafValue = 0,
                                                        SafAccount = null,
                                                        SafApiRef = null,
                                                        SafMpesaRef = null,
                                                        SafHost = null,
                                                        SafRetry = null,
                                                        SafFailedReason = null,
                                                        SafFailedCode = null,
                                                        SafCreatedDate = null,
                                                        SafCreatedTime = null,
                                                        SafCustomerPhoneNumber = null,
                                                        SafCustomerCountry = null,
                                                        SafCustomerEmail = null,
                                                        SafCustomerLastname = null
                                                    };

                                                    _context.MemberTransactions.Add(payingMember);
                                                    await _context.SaveChangesAsync();

                                                    if (prepaidAmount > 0 && pendingContributionTransactionFee != 0)
                                                    {
                                                        if (pendingContributionTransactionFee != 0)
                                                        {
                                                            if (prepaidAmount >= pendingContributionTransactionFee)
                                                            {
                                                                await UpdateMemberAccountBalances("Update Prepaid Balance", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, pendingContributionTransactionFee, null, null, null, null, null, null, null, null, null);
                                                                await UpdateMemberAccountBalances("Update Today Transaction Fee", "Increase", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, null, null, null, null, null, pendingContributionTransactionFee, null); ;
                                                                await UpdateMemberAccountBalances("Update Today Pending Transaction Fee", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, null, null, null, null, null, null, pendingContributionTransactionFee);
                                                                await UpdateGroupBalances("Update Current Balance", "Increase", memberGroupId, null, pendingContributionTransactionFee, null);
                                                                await UpdateGroupBalances("Update Pygg Balance", "Increase", memberGroupId, pendingContributionTransactionFee, null, null);
                                                                prepaidAmount = (decimal)(prepaidAmount - pendingContributionTransactionFee);
                                                                pendingContributionTransactionFee = (decimal)(pendingContributionTransactionFee - pendingContributionTransactionFee);
                                                            }
                                                            else
                                                            {
                                                                await UpdateMemberAccountBalances("Update Prepaid Balance", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, prepaidAmount, null, null, null, null, null, null, null, null, null);
                                                                await UpdateMemberAccountBalances("Update Today Transaction Fee", "Increase", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, null, null, null, null, null, prepaidAmount, null); ;
                                                                await UpdateMemberAccountBalances("Update Today Pending Transaction Fee", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, null, null, null, null, null, null, prepaidAmount);
                                                                await UpdateGroupBalances("Update Current Balance", "Increase", memberGroupId, null, prepaidAmount, null);
                                                                await UpdateGroupBalances("Update Pygg Balance", "Increase", memberGroupId, prepaidAmount, null, null);
                                                                prepaidAmount = (decimal)(prepaidAmount - prepaidAmount);
                                                                pendingContributionTransactionFee = (decimal)(pendingContributionTransactionFee - prepaidAmount);
                                                            }
                                                        }
                                                    }

                                                    if (pendingContributionTransactionFee == 0)
                                                    {
                                                        var installmentUpdate = await _context.GroupDepositInstallments.FindAsync(update.InstallmentNo);
                                                        if (installmentUpdate != null)
                                                        {
                                                            installmentUpdate.IsInstallmentPaid = true;
                                                            await _context.SaveChangesAsync();
                                                        }

                                                    }

                                                    if (prepaidAmount > 0)
                                                    {
                                                        //check whether there might be bug for member who is not paying contribution
                                                        await UpdateMemberAccountBalances("Update Prepaid Balance", "Increase", memberGroupId, memberNumber, memberUserId, null, null, prepaidAmount, null, null, null, null, null, null, null, null, null);
                                                        //await UpdateMemberAccountBalances("Update Current Balance", "Increase", memberGroupId, memberNumber, memberUserId, prepaidAmount, null, null, null, null, null, null, prepaidAmount, null, null, null, null);
                                                    }

                                                }
                                                else
                                                {
                                                    int serialNumber = await GetNextTransactionSerialNo();

                                                    MemberTransaction payingMember = new()
                                                    {
                                                        TransactionSerialId = serialNumber,
                                                        TransactionAccountNumber = memberAccountNumber,
                                                        TransactionMemberId = memberNumber,
                                                        TransactionGroupId = memberGroupId,
                                                        TransactionUserId = memberUserId,
                                                        ContributionId = 0,
                                                        ContributionTypeId = "CRT01",
                                                        FineId = 0,
                                                        PaymentTypeId = "PYT01",
                                                        TransactionCode = null,
                                                        TransactionTypeId = "TR01",
                                                        TransactionAmount = prepaidAmount,
                                                        TransactionDate = Convert.ToDateTime(Convert.ToDateTime(DateTime.Now.Date)),
                                                        TransactionValueDate = Convert.ToDateTime(Convert.ToDateTime(DateTime.Now.Date)),
                                                        TransactionGroupFineAccount = null,
                                                        TransactionContributionAccount = "DEPSGRA031",
                                                        TransactionNarration = " Amount deducted automatically by system for contribution from Prepayment",
                                                        TransactionIsConfirmed = true,
                                                        TransactionIsDefaultRecovery = false,
                                                        TransactionDefaultRecoveryMemberId = 0,
                                                        TransactionDefaultRecoveryGroupId = 0,
                                                        TransactionDefaultRecoveryUserId = 0,
                                                        TransactionDefaultRecoveryContributionId = 0,
                                                        TransactionFineRecoveryFineId = 0,
                                                        TransactionCreatedBy = 1,//Remember to create a system account to handle this transactions
                                                        TransactionCreatedOn = DateTime.Now,

                                                        SafInvoiceId = "N/A",
                                                        SafTransState = "N/A",
                                                        SafCharges = 0,
                                                        SafNetAmount = 0,
                                                        SafCurrency = "KES",
                                                        SafValue = 0,
                                                        SafAccount = null,
                                                        SafApiRef = null,
                                                        SafMpesaRef = null,
                                                        SafHost = null,
                                                        SafRetry = null,
                                                        SafFailedReason = null,
                                                        SafFailedCode = null,
                                                        SafCreatedDate = null,
                                                        SafCreatedTime = null,
                                                        SafCustomerPhoneNumber = null,
                                                        SafCustomerCountry = null,
                                                        SafCustomerEmail = null,
                                                        SafCustomerLastname = null
                                                    };

                                                    _context.MemberTransactions.Add(payingMember);
                                                    await _context.SaveChangesAsync();

                                                    if (pendingContributionAmount != 0)
                                                    {
                                                        if (prepaidAmount >= pendingContributionAmount)
                                                        {
                                                            await UpdateMemberAccountBalances("Update Prepaid Balance", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, pendingContributionAmount, null, null, null, null, null, null, null, null, null);
                                                            //await UpdateMemberAccountBalances("Update Current Balance", "Decrease", memberGroupId, memberNumber, memberUserId, pendingContributionAmount, null, null, null, null, null, null, pendingContributionAmount, null, null, null, null);
                                                            await UpdateMemberAccountBalances("Update Today Deposit", "Increase", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, null, null, null, pendingContributionAmount, null, null, null);
                                                            await UpdateMemberAccountBalances("Update Today Pending Deposit", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, null, null, null, null, pendingContributionAmount, null, null);
                                                            prepaidAmount = (decimal)(prepaidAmount - pendingContributionAmount);
                                                            pendingContributionAmount = (decimal)(pendingContributionAmount - pendingContributionAmount);
                                                        }
                                                        else
                                                        {
                                                            await UpdateMemberAccountBalances("Update Prepaid Balance", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, prepaidAmount, null, null, null, null, null, null, null, null, null);
                                                            //await UpdateMemberAccountBalances("Update Current Balance", "Decrease", memberGroupId, memberNumber, memberUserId, prepaidAmount, null, null, null, null, null, null, prepaidAmount, null, null, null, null);
                                                            await UpdateMemberAccountBalances("Update Today Deposit", "Increase", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, null, null, null, prepaidAmount, null, null, null);
                                                            await UpdateMemberAccountBalances("Update Today Pending Deposit", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, null, null, null, null, prepaidAmount, null, null);
                                                            prepaidAmount = (decimal)(prepaidAmount - prepaidAmount);
                                                            pendingContributionAmount = (decimal)(pendingContributionAmount - prepaidAmount);
                                                        }
                                                    }



                                                    if (prepaidAmount > 0 && pendingContributionTransactionFee != 0)
                                                    {
                                                        if (pendingContributionTransactionFee != 0)
                                                        {
                                                            if (prepaidAmount >= pendingContributionTransactionFee)
                                                            {
                                                                await UpdateMemberAccountBalances("Update Prepaid Balance", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, pendingContributionTransactionFee, null, null, null, null, null, null, null, null, null);
                                                                //await UpdateMemberAccountBalances("Update Current Balance", "Decrease", memberGroupId, memberNumber, memberUserId, pendingContributionTransactionFee, null, null, null, null, null, null, pendingContributionTransactionFee, null, null, null, null);
                                                                await UpdateMemberAccountBalances("Update Today Transaction Fee", "Increase", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, null, null, null, null, null, pendingContributionTransactionFee, null);
                                                                await UpdateMemberAccountBalances("Update Today Pending Transaction Fee", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, null, null, null, null, null, null, pendingContributionTransactionFee);
                                                                await UpdateGroupBalances("Update Current Balance", "Increase", memberGroupId, null, pendingContributionTransactionFee, null);
                                                                await UpdateGroupBalances("Update Pygg Balance", "Increase", memberGroupId, pendingContributionTransactionFee, null, null);
                                                                prepaidAmount = (decimal)(prepaidAmount - pendingContributionTransactionFee);
                                                                pendingContributionTransactionFee = (decimal)(pendingContributionTransactionFee - pendingContributionTransactionFee);
                                                            }
                                                            else
                                                            {
                                                                await UpdateMemberAccountBalances("Update Prepaid Balance", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, prepaidAmount, null, null, null, null, null, null, null, null, null);
                                                                //await UpdateMemberAccountBalances("Update Current Balance", "Decrease", memberGroupId, memberNumber, memberUserId, prepaidAmount, null, null, null, null, null, null, prepaidAmount, null, null, null, null);
                                                                await UpdateMemberAccountBalances("Update Today Transaction Fee", "Increase", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, null, null, null, null, null, prepaidAmount, null); ;
                                                                await UpdateMemberAccountBalances("Update Today Pending Transaction Fee", "Decrease", memberGroupId, memberNumber, memberUserId, null, null, null, null, null, null, null, null, null, null, null, prepaidAmount);
                                                                await UpdateGroupBalances("Update Current Balance", "Increase", memberGroupId, null, prepaidAmount, null);
                                                                await UpdateGroupBalances("Update Pygg Balance", "Increase", memberGroupId, prepaidAmount, null, null);
                                                                prepaidAmount = (decimal)(prepaidAmount - prepaidAmount);
                                                                pendingContributionTransactionFee = (decimal)(pendingContributionTransactionFee - prepaidAmount);
                                                            }
                                                        }
                                                    }

                                                }

                                                if (pendingContributionAmount == 0 && pendingContributionTransactionFee == 0)
                                                {
                                                    var installmentUpdate = await _context.GroupDepositInstallments.FindAsync(update.InstallmentNo);
                                                    if (installmentUpdate != null)
                                                    {
                                                        installmentUpdate.IsInstallmentPaid = true;
                                                        await _context.SaveChangesAsync();
                                                    }

                                                }
                                                ///Consider adding balance as prepayment

                                                if (prepaidAmount > 0)
                                                {
                                                    await UpdateMemberAccountBalances("Update Prepaid Balance", "Increase", memberGroupId, memberNumber, memberUserId, null, null, prepaidAmount, null, null, null, null, null, null, null, null, null);
                                                    //await UpdateMemberAccountBalances("Update Current Balance", "Increase", memberGroupId, memberNumber, memberUserId, prepaidAmount, null, null, null, null, null, null, prepaidAmount, null, null, null, null);
                                                }

                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                await _context.SaveChangesAsync();
            }

            return true;
        }



        public async Task<bool> PostFines()
        {
            DateTime serverTimeUtc = DateTime.UtcNow;
            TimeZoneInfo eastAfricaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. Africa Standard Time");
            DateTime eastAfricaTime = TimeZoneInfo.ConvertTimeFromUtc(serverTimeUtc, eastAfricaTimeZone);

            var dailyRobins = await _groupBusiness.GetGroupsDailyRobins();
            if (dailyRobins == null || dailyRobins.Count == 0) return false;

            foreach (var robin in dailyRobins)
            {
                var groupId = robin.GroupId;
                var contributionType = robin.ContributionTypeId;
                var frequency = robin.ContributionFrequency;

                var groupFines = await _context.GroupFineTypes
                               .Where(f => f.GroupId == groupId && f.FineContributionTypeId == contributionType && f.Frequency == frequency && f.IsActive == true)
                               .ToListAsync();
                if (groupFines.Count == 0) return false;

                if (string.Equals(frequency, "FRQ01")) //Daily frequency
                {
                    if (groupFines.Count > 0)
                    {
                        foreach (var fine in groupFines)
                        {
                            var fineId = fine.FineId;
                            var fineName = fine.FineName;
                            var fineContributionType = fine.FineContributionTypeId;
                            var fineStartTime = fine.StartTime;
                            var fineEndTime = fine.EndTime;
                            var fineAmount = fine.FineAmount;
                            var isFineTimeBased = fine.IsTimeBased;

                            //get all members who havent paid their day installments in full apart from the one receiving today
                            int series = await GetGroupCurrentRobinSeries((int)groupId);
                            if (series == 0) return false;

                            var payingMembersInstallmentDetails = await _context.GroupDepositInstallments
                           .Where(a => a.InstallmentGroupId == groupId && a.IsInstallmentPaid == false && a.InstallmentSeries == series &&
                                    a.InstallmentDate == Convert.ToDateTime(eastAfricaTime.ToString("yyyy-MM-dd")))
                           .ToListAsync();

                            if (payingMembersInstallmentDetails.Count == 0) return false;

                            var installmentDate = payingMembersInstallmentDetails.FirstOrDefault().InstallmentDate;
                            var nonPaying = payingMembersInstallmentDetails.Where(a => a.MemberDueDate == installmentDate).FirstOrDefault();
                            var nonPayingMemberId = nonPaying.InstallmentMemberId;
                            var nonPayingMemberGroupId = nonPaying.InstallmentGroupId;
                            var nonPayingMemberUserId = nonPaying.InstallmentUserId;
                            var nonPayingMemberContributionId = nonPaying.InstallmentContributionType;
                            var nonPayingMemberFineId = fineId;
                            var nonPayingMemberPenaltyAmount = nonPaying.InstallmentAmount;
                            var nonPayingMemberTransactionFee = nonPaying.InstallmentCharge;
                            var totalNonPayingMemberDefaultedAmount = nonPayingMemberPenaltyAmount + nonPayingMemberTransactionFee;

                            if (payingMembersInstallmentDetails.Count > 0)
                            {
                                foreach (var payingMember in payingMembersInstallmentDetails)
                                {
                                    if (payingMember.InstallmentMemberId != nonPayingMemberId)
                                    {
                                        if (isFineTimeBased == true)
                                        {
                                            var latePayInstallmentNo = payingMember.InstallmentNo;
                                            var latePayIsInstallmentPaid = (bool)payingMember.IsInstallmentPaid;
                                            var latePayMemberGroupId = payingMember.InstallmentGroupId;
                                            var latePayMemberId = payingMember.InstallmentMemberId;
                                            var latePayUserId = payingMember.InstallmentUserId;
                                            var lateInstallmentAmount = payingMember.InstallmentAmount;
                                            var lateinstallmentCharge = payingMember.InstallmentCharge;
                                            var lateInstallmentContributionType = payingMember.InstallmentContributionType;


                                            TimeSpan timeOfDay = eastAfricaTime.TimeOfDay;
                                            DateTime dateOfDay = eastAfricaTime.Date;

                                            if (timeOfDay > fineStartTime)
                                            {
                                                //check whether the fine has been posted or not

                                                var isFinePosted = await _context.DefaultDetails
                                               .Where(a => a.PenaltyGroupId == latePayMemberGroupId && a.PenaltyMemberNumber == latePayMemberId && a.PenaltyUserId == latePayUserId && a.PenaltyFineId == fineId &&
                                                        a.PenaltlyDate == installmentDate)//Convert.ToDateTime(installmentDate.ToString("yyyy-MM-dd")))
                                               .ToListAsync();
                                                if (isFinePosted.Count == 0)
                                                {
                                                    var defaultDetails = new DefaultDetail
                                                    {
                                                        PenaltyGroupId = latePayMemberGroupId,
                                                        PenaltyMemberNumber = latePayMemberId,
                                                        PenaltyUserId = latePayUserId,
                                                        PenaltyContributionId = 0,
                                                        PenaltyFineId = fineId,
                                                        ContributionTypeId = lateInstallmentContributionType,
                                                        PenaltyOwedToMemberId = nonPayingMemberId,
                                                        PenaltyOwedToGroupId = nonPayingMemberGroupId,
                                                        PenaltyOwedToUserId = nonPayingMemberUserId,
                                                        PenaltyOwedToContributionId = 0,
                                                        PenaltyOwedToFineId = nonPayingMemberFineId,
                                                        PenaltyAmount = 0,
                                                        PenaltyTransactionFee = 0,
                                                        FineAmount = fineAmount,
                                                        PenaltyValueDate = eastAfricaTime,
                                                        PenaltlyDate = installmentDate,
                                                        JoinFeeAmount = 0,
                                                        IsPending = true,
                                                        DateCreated = DateTime.UtcNow,
                                                        CreatedBy = 1
                                                    };

                                                    _context.DefaultDetails.Add(defaultDetails);
                                                    await _context.SaveChangesAsync();

                                                    await UpdateMemberAccountBalances("Update Total Penalty", "Increase", latePayMemberGroupId, latePayMemberId, latePayUserId, null, null, null, null, null, fineAmount, null, null, null, null, null, null);
                                                    await UpdateGroupBalances("Update Available Balance", "Increase", nonPayingMemberGroupId, null, null, fineAmount);
                                                    //await UpdateMemberAccountBalances("Update Default Amount To Pay", "Increase", latePayMemberGroupId, latePayMemberId, latePayUserId, null, null, null, totalNonPayingMemberDefaultedAmount, null, null, null, null, null, null, null, null);
                                                    //await UpdateMemberAccountBalances("Update Default Amount To Receive", "Increase", nonPayingMemberGroupId, nonPayingMemberId, nonPayingMemberUserId, null, null, null, null, nonPayingMemberPenaltyAmount, null, null, null, null, null, null, null);
                                                    //await UpdateMemberAccountBalances("Update Available Balance", "Increase", nonPayingMemberGroupId, nonPayingMemberId, nonPayingMemberUserId, null, nonPayingMemberPenaltyAmount, null, null, null, null, null, null, null, null, null, null);
                                                    //await UpdateGroupBalances("Update Available Balance", "Increase", nonPayingMemberGroupId, null, null, nonPayingMemberTransactionFee);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return true;
        }

        public async Task<List<Octopus>> RunOctopusDeployment(DateTime startDate, int groupId, string contributionTypeId)
        {
            var depositAmount = 0;
            var totalContribution = 0;
            var chargeAmount = 0;
            DateTime dateTimeUK = DateTime.Parse(Convert.ToDateTime(startDate).ToString("MM/dd/yyyy"), new CultureInfo("en-US"));
            string britishDate = Convert.ToDateTime(startDate).ToString("MM/dd/yyyy");


            int installmentSerie = await GetNextSerialNumberAsync(groupId);

            var groupMembers = await _context.GroupMembers
                    .Where(m => m.GroupId == groupId && m.IsActive == true)
                    .ToListAsync();

            if (groupId == 0) return null;
            var random = new Random();
            var shuffledMembers = groupMembers.OrderBy(x => random.Next()).ToList();

            //var endDate = Convert.ToDateTime(britishDate).AddDays(8);

            var endDate = Convert.ToDateTime(britishDate).AddDays(groupMembers.Count);

            var dateDifference = (int)(endDate - dateTimeUK).TotalDays;

            var contribution = await _context.GroupContributions
                               .Where(c => c.ContributionTypeId == contributionTypeId && c.IsContributionActive == true && c.GroupId == groupId && c.IsRobin == true)
                               .ToListAsync();

            if (contribution.Count == 0) return null;
            var contId = contribution.FirstOrDefault().ContributionId;
            var contributionType = contribution.FirstOrDefault().ContributionTypeId;
            depositAmount = (int)contribution.FirstOrDefault().ContributionAmount;
            contribution.FirstOrDefault().ContStartDate = Convert.ToDateTime(britishDate);
            contribution.FirstOrDefault().ContEndDate = endDate.AddDays(-1);
            totalContribution = depositAmount * groupMembers.Count;



            var charge = await _context.Charges
                         .Where(row => totalContribution >= row.ChargeMinAmount && totalContribution <= row.ChargeMaxAmount)
                         .FirstOrDefaultAsync();

            if (charge == null) return null;
            chargeAmount = (int)charge.ToMpesaUsers;
            chargeAmount = (int)Math.Ceiling(chargeAmount / (decimal)groupMembers.Count);

            var robinNumber = 0;
            var dueDate = Convert.ToDateTime(startDate).AddDays(-1);

            foreach (var member in shuffledMembers)
            {
                robinNumber++;
                var memberDueDate = dueDate.AddDays(1);
                for (int i = 0; i < dateDifference; i++)
                {

                    var installment = new GroupDepositInstallment
                    {
                        InstallmentSeries = installmentSerie,
                        InstallmentGroupId = member.GroupId,
                        InstallmentMemberId = member.GroupMemberId,
                        InstallmentUserId = member.GroupMemberUserId,
                        InstallmentDate = Convert.ToDateTime(startDate).AddDays(i),
                        MemberRobinNumber = robinNumber,
                        MemberDueDate = memberDueDate,
                        InstallmentAmount = depositAmount,
                        InstallmentCharge = chargeAmount,
                        IsInstallmentPaid = false,
                        InstallmentContributionType = contributionTypeId,
                        InstallmentCreatedBy = 1,
                        InstallmentCreatedOn = DateTime.Now,
                    };

                    _context.GroupDepositInstallments.Add(installment);

                }
                dueDate = memberDueDate;
            }

            await _context.SaveChangesAsync();

            var schedule = await (from a in _context.GroupDepositInstallments
                                  join b in _context.Users on a.InstallmentUserId equals b.UserId
                                  where a.InstallmentSeries == _context.GroupDepositInstallments.Max(x => x.InstallmentSeries)
                                  group new { b.Name, a.MemberRobinNumber, a.MemberDueDate, a.InstallmentSeries } by new { b.Name, a.MemberRobinNumber, a.MemberDueDate, a.InstallmentSeries } into g
                                  orderby g.Key.MemberRobinNumber ascending
                                  select new Octopus
                                  {
                                      Name = g.Key.Name,
                                      MemberRobinNumber = g.Key.MemberRobinNumber,
                                      InstallmentSeries = g.Key.InstallmentSeries,
                                      MemberDueDate = (DateTime)g.Key.MemberDueDate
                                  }).ToListAsync();


            return schedule;

        }


        public async Task<List<Octopus>> DisplayRoinSchedule()
        {
            var schedule = await (from a in _context.GroupDepositInstallments
                                  join b in _context.Users on a.InstallmentUserId equals b.UserId
                                  where a.InstallmentSeries == _context.GroupDepositInstallments.Max(x => x.InstallmentSeries)
                                  group new { b.Name, a.MemberRobinNumber, a.MemberDueDate, a.InstallmentSeries, a.InstallmentGroupId,a.InstallmentMemberId,a.InstallmentUserId} by new { b.Name, a.MemberRobinNumber, a.MemberDueDate, a.InstallmentSeries, a.InstallmentGroupId, a.InstallmentMemberId, a.InstallmentUserId } into g
                                  orderby g.Key.MemberRobinNumber ascending
                                  select new Octopus
                                  {
                                      InstallmentGroupId = g.Key.InstallmentGroupId,
                                      InstallmentMemberId = g.Key.InstallmentMemberId,
                                      InstallmentUserId = g.Key.InstallmentUserId,
                                      Name = g.Key.Name,
                                      MemberRobinNumber = g.Key.MemberRobinNumber,
                                      InstallmentSeries = g.Key.InstallmentSeries,
                                      MemberDueDate = (DateTime)g.Key.MemberDueDate
                                  })
                                  .ToListAsync();

            return schedule;
        }

        public async Task<int> GetNextSerialNumberAsync(int groupId)
        {
            var query = await _context.GroupDepositInstallments
                .Where(installment => installment.InstallmentGroupId == groupId)
                .Select(installment => installment.InstallmentSeries)
                .OrderByDescending(series => series)
                .FirstOrDefaultAsync();

            int nextSerialNumber = query + 1;

            return nextSerialNumber;
        }


        public async Task<int> GetGroupCurrentRobinSeries(int groupId)
        {
            var maxInstallmentSeries = await _context.GroupDepositInstallments
                .Where(i => i.InstallmentGroupId == groupId)
                .MaxAsync(i => (int?)i.InstallmentSeries);

            int robinSeries = maxInstallmentSeries ?? 0;
            return robinSeries;
        }



        public async Task<int> GetNextTransactionSerialNo()
        {
            var query = await _context.MemberTransactions
               .Select(serial => serial.TransactionSerialId)
                .OrderByDescending(series => series)
                .FirstOrDefaultAsync();

            int nextSerialNumber = query + 1;

            return nextSerialNumber;
        }

        public async Task<int> GetGroupMemberRoles(int groupId, int memberNumber, int groupMemberUserId)
        {
            var roleId = await _context.GroupMembers
                .Where(i => i.GroupId == groupId && i.GroupMemberId == memberNumber && i.GroupMemberUserId == groupMemberUserId)
                .Select(i => i.GroupMemberRoleId)
                .FirstOrDefaultAsync();

            int memberRoleId = roleId ?? 0;
            return memberRoleId;
        }


    }
}
