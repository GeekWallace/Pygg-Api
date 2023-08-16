using System.ComponentModel.DataAnnotations;

namespace PyggApi.Models
{
    public class PaymentType
    {
        [Key]
        [StringLength(10)]
        public string PaymentTypeId { get; set; }

        [StringLength(50)]
        public string PaymentName { get; set; }

        public string PaymentDescription { get; set; }
    }
}
