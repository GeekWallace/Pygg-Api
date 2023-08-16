using System.ComponentModel.DataAnnotations;

namespace PyggApi.Models
{
    public class GroupContribution
    {
        public int ContributionId { get; set; }
        public int? GroupId { get; set; }
        [Key]
        public string ContributionTypeId { get; set; }
        public string ContributionName { get; set; }
        public decimal? ContributionAmount { get; set; }
        public string ContributionFrequency { get; set; }
        public string ContributionMpesaCharge { get; set; }
        public string ContributionGroupAccount { get; set; }
        public string ContributionCurrency { get; set; }
        public string ContributionCountryCode { get; set; }
        public bool? IsContributionActive { get; set; }
        public bool IsRobin { get; set; }
        public int? ContributionCreatedBy { get; set; }
        public DateTime? ContributionCreatedOn { get; set; }

        public DateTime? ContEndDate { get; set; }
        public DateTime? ContStartDate { get; set; }
        public int? ContributionGoalId { get; set; }
    }
}
