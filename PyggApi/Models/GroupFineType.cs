using System.ComponentModel.DataAnnotations;

namespace PyggApi.Models
{
    public class GroupFineType
    {
        [Key]
        public int FineId { get; set; }

        [Required]
        [StringLength(100)]
        public string FineName { get; set; }

        [Required]
        [StringLength(10)]
        public string FineContributionTypeId { get; set; }

        [Required]
        public int GroupId { get; set; }

        [StringLength(50)]
        public string FineGroupAccount { get; set; }

        public bool? IsTimeBased { get; set; }

        public TimeSpan? StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public decimal? FineAmount { get; set; }

        public bool? IsActive { get; set; }

        [StringLength(10)]
        public string Frequency { get; set; }

        [StringLength(10)]
        public string DayPayable { get; set; }

        public string FineDescription { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }
    
    }
}
