using System.ComponentModel.DataAnnotations;

namespace PyggApi.Models
{
    public class TransactionType
    {
        [Required]
        public string TransactionTypeId { get; set; }

        [MaxLength(100)]
        public string TransactionTypeName { get; set; }

        public string TransactionTypeDescription { get; set; }
    }
}
