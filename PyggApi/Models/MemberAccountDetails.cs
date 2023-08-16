namespace PyggApi.Models
{
    public class MemberAccountDetails
    {
        public int MemberId { get; set; }
         public string MemberFirstName { get; set; }
        public string MemberMiddleName { get; set; }
        public string MemberLastName { get; set; }
        public string MemberFullName { get; set; }
        public string Email { get; set; }
        public string MemberPhoneNumber { get; set; }
        public string PhotoUrl { get; set; }
        public decimal AccountTodayPendingDeposit { get; set; }
        public decimal AccountTodayDeposit { get; set; }
        public decimal DepositBalanceToPay { get; set; }
        public decimal AccountTodayPendingTransactionFee { get; set; }
        public decimal AccountTodayTransactionFee { get; set; }
        public decimal FeeBalanceToPay { get; set; }
        public decimal AccountPrepaidBalance { get; set; }
        public decimal AccountDefaultAmountToPay { get; set; }
        public decimal AccountDefaultAmountToReceive { get; set; }
        public DateTime? DefaultDueDate { get; set; }
        public decimal AccountTodayTotalPenalty { get; set; }
        public decimal AccountLoanBalance { get; set; }
        public DateTime? LoanDueDate { get; set; }
        public decimal AccountAvailableBalance { get; set; }
        public decimal AccountCurrentBalance { get; set; }
        //*****
        public bool AccountIsActive { get; set; }
        public decimal LastDebitTrxAmount { get; set; }
        public decimal LastCreditTrxAmount { get; set; }
        public DateTime? LastDebitTrxDate { get; set; }
        public DateTime? LastCreditTrxDate { get; set; }
        public string GroupName { get; set; }
        public int AccountMemberId { get; set; }
        public int AccountNumber { get; set; }
        public int AccountUserId { get; set; }
        public int AccountGroupId { get; set; }
    }
}
