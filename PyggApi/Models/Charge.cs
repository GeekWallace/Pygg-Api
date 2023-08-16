using System.ComponentModel.DataAnnotations;

namespace PyggApi.Models
{
    public class Charge
    {
        [Required]
        public string ChargeId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "The field ChargeMinAmount must be a non-negative number.")]
        public decimal? ChargeMinAmount { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "The field ChargeMaxAmount must be a non-negative number.")]
        public decimal? ChargeMaxAmount { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "The field ToMpesaUsers must be a non-negative number.")]
        public decimal? ToMpesaUsers { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "The field ToUnregisteredUsers must be a non-negative number.")]
        public decimal? ToUnregisteredUsers { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "The field ToTill must be a non-negative number.")]
        public decimal? ToTill { get; set; }

        public string ChargeDescription { get; set; }
    }
}
