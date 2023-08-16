using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//#nullable enable
namespace PyggApi.Models
{

    public class MemberTransaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionId { get; set; }

        [Required]
        public int TransactionSerialId { get; set; }

        [Required]
        public int TransactionMemberId { get; set; }

        [Required]
        public int TransactionAccountNumber { get; set; }

        [Required]
        public int TransactionGroupId { get; set; }

        [Required]
        public int TransactionUserId { get; set; }

        public int? ContributionId { get; set; }

       
        public string ContributionTypeId { get; set; }

        public int? FineId { get; set; }

        [Required]
        [StringLength(10)]
        public string PaymentTypeId { get; set; }

        [StringLength(50)]
        public string TransactionCode { get; set; }

        [StringLength(10)]
        public string TransactionTypeId { get; set; }

        public decimal? TransactionAmount { get; set; }

        public DateTime? TransactionDate { get; set; }

        public DateTime? TransactionValueDate { get; set; }

        [StringLength(50)]
        public string TransactionGroupFineAccount { get; set; }

        [StringLength(50)]
        public string TransactionContributionAccount { get; set; }

        public string TransactionNarration { get; set; }

        public bool? TransactionIsConfirmed { get; set; }

        public bool? TransactionIsDefaultRecovery { get; set; }
        public int? TransactionDefaultRecoveryMemberId { get; set; }
        public int? TransactionDefaultRecoveryGroupId { get; set; }
        public int? TransactionDefaultRecoveryUserId { get; set; }
        public int? TransactionDefaultRecoveryContributionId { get; set; }
        public int? TransactionFineRecoveryFineId { get; set; }

      
        public int TransactionCreatedBy { get; set; }

        public DateTime? TransactionCreatedOn { get; set; }

        //Safaricom fields
        public string SafInvoiceId { get; set; }
        public string SafTransState { get; set; }
        public decimal? SafCharges { get; set; }
        public decimal? SafNetAmount { get; set; }
        public string SafCurrency { get; set; }
        public decimal? SafValue { get; set; }
        public string SafAccount { get; set; }
        public string SafApiRef { get; set; }
        public string SafMpesaRef { get; set; }
        public string SafHost { get; set; }
        public int? SafRetry { get; set; }
        public string SafFailedReason { get; set; }
        public string SafFailedCode { get; set; }
        public string SafFailedCodeLink { get; set; }
        public string SafCreatedDate { get; set; }
        public string SafCreatedTime { get; set; }
        public string SafCustomerPhoneNumber { get; set; }
        public string SafCustomerCountry { get; set; }
        public string SafCustomerEmail { get; set; }
        public string SafCustomerLastname { get; set; }
       
    }

}
