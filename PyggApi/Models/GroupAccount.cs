using System.ComponentModel.DataAnnotations;

namespace PyggApi.Models
{
    public class GroupAccount
    {
        [Key]
        public int GroupAccountId { get; set; }

        public int GroupId { get; set; }

        [Required]
        [StringLength(50)]
        public string GroupAccountType { get; set; }

        [StringLength(50)]
        public string GroupAccountNumber { get; set; }

        [StringLength(50)]
        public string GroupAccountName { get; set; }

        [StringLength(100)]
        public string GroupAccounServiceProvider { get; set; }

        public decimal? GroupAccountOpeningBalance { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CratedOn { get; set; }
    }
}
