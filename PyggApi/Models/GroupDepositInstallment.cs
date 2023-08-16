using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PyggApi.Models
{
    public class GroupDepositInstallment
    {
        [Key]
        public int InstallmentNo { get; set; }

        [Required]
        public int InstallmentSeries { get; set; }

        [Required]
        public int InstallmentGroupId { get; set; }

        [Required]
        public int InstallmentMemberId { get; set; }

        [Required]
        public int InstallmentUserId { get; set; }

        public int MemberRobinNumber  { get; set; }

    
        public string InstallmentContributionType { get; set; }
        public DateTime? InstallmentDate { get; set; }

        public DateTime? MemberDueDate { get; set; }

        public decimal? InstallmentAmount { get; set; }

        public decimal? InstallmentCharge { get; set; }

        public bool? IsInstallmentPaid { get; set; }

        public int? InstallmentCreatedBy { get; set; }

        public DateTime? InstallmentCreatedOn { get; set; }
    }
}
