using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PyggApi.Models
{
    public class DefaultDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PenaltyId { get; set; }
        public int? PenaltyGroupId { get; set; }
        public string ContributionTypeId { get; set; }
        public int? PenaltyUserId { get; set; }
        public int PenaltyMemberNumber { get; set; }
        public int? PenaltyContributionId { get; set; }
        public int? PenaltyFineId { get; set; }
        public int? PenaltyOwedToMemberId { get; set; }
        public int? PenaltyOwedToFineId { get; set; }
        public int? PenaltyOwedToGroupId { get; set; }
        public int? PenaltyOwedToUserId { get; set; }
        public int? PenaltyOwedToContributionId { get; set; }
        public decimal? PenaltyAmount { get; set; }
        public decimal? FineAmount { get; set; }
        public decimal? JoinFeeAmount { get; set; }
        public decimal? PenaltyTransactionFee { get; set; }
        public DateTime? PenaltyValueDate { get; set; }
        public DateTime? PenaltlyDate { get; set; }
        public bool? IsPending { get; set; }
        public DateTime? DateCreated { get; set; }
        public int? CreatedBy { get; set; }
    }
}
