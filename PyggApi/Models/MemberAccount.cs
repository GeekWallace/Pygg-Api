using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PyggApi.Models
{
    public class MemberAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountNumber { get; set; }

        [Required]
        public int AccountGroupId { get; set; }

        [Required]
        public int AccountMemberId { get; set; }

        [Required]
        public int AccountUserId { get; set; }

        public DateTime? AccountOpenDate { get; set; }

        public DateTime? AccountCloseDate { get; set; }

        public decimal? AccountTodayDeposit { get; set; }

        public decimal? AccountTodayPendingDeposit { get; set; }

        public decimal? AccountTodayTransactionFee { get; set; }

        public decimal? AccountTodayPendingTransactionFee { get; set; }

        public decimal? AccountCurrentBalance { get; set; }

        public decimal? AccountAvailableBalance { get; set; }

        public decimal? AccountPrepaidBalance { get; set; }

        public decimal? AccountDefaultAmountToPay { get; set; }

        public decimal? AccountDefaultAmountToReceive { get; set; }

        public DateTime? DefaultDueDate { get; set; }

        public decimal? AccountTodayTotalPenalty { get; set; }

        public decimal? AccountLoanBalance { get; set; }

        public DateTime? LoanDueDate { get; set; }

        public decimal? LastCreditAmount { get; set; }

        public decimal? LastDebitAmount { get; set; }

        public DateTime? LastDebitTrxDate { get; set; }

        public DateTime? LastCreditTrxDate { get; set; }

        public bool? AccountIsActive { get; set; }

        public DateTime? AccountCreatedOn { get; set; }

        [StringLength(10)]
        public string AccountCreatedBy { get; set; }
    }
}
